import { beforeEach, describe, expect, test } from "vitest";
import { ArgumentException, Game } from "../src/Game";

describe("GameTests", () => {
  let game: Game;

  beforeEach(() => {
    game = new Game();
  });

  describe("PlayerOneWins", () => {
    test.each([
      ["rock", "scissors"],
      ["paper", "rock"],
      ["scissors", "paper"],
    ])("TextMoves_PlayerOneWins", (player1Move, player2Move) => {
      game.Play(player1Move, player2Move);
      expect(game.Result()).toBe("Player 1 wins");
    });

    test.each([
      ["ROCK", "scissors"],
      ["Paper", "ROCK"],
      ["Scissors", "pApEr"],
      ["rOcK", "SCISSORS"],
    ])("MixedCasing_PlayerOneWins", (player1Move, player2Move) => {
      game.Play(player1Move, player2Move);
      expect(game.Result()).toBe("Player 1 wins");
    });

    test.each([
      ["🪨", "✂️"],
      ["✊", "✌️"],
      ["👊", "✂"],
      ["📄", "🪨"],
      ["✋", "✊"],
      ["📃", "👊"],
      ["✂️", "📄"],
      ["✌️", "✋"],
      ["✂", "📃"],
    ])("EmojiMoves_PlayerOneWins", (player1Move, player2Move) => {
      game.Play(player1Move, player2Move);
      expect(game.Result()).toBe("Player 1 wins");
    });

    test.each([
      ["rock", "✂️"],
      ["📄", "rock"],
      ["scissors", "✋"],
      ["✊", "scissors"],
      ["Paper", "🪨"],
      ["✌️", "paper"],
    ])("MixedTextAndEmoji_PlayerOneWins", (player1Move, player2Move) => {
      game.Play(player1Move, player2Move);
      expect(game.Result()).toBe("Player 1 wins");
    });
  });

  describe("PlayerTwoWins", () => {
    test.each([
      ["scissors", "rock"],
      ["rock", "paper"],
      ["paper", "scissors"],
    ])("TextMoves_PlayerTwoWins", (player1Move, player2Move) => {
      game.Play(player1Move, player2Move);
      expect(game.Result()).toBe("Player 2 wins");
    });

    test.each([
      ["SCISSORS", "Rock"],
      ["rock", "PAPER"],
      ["PaPeR", "sCiSsOrS"],
    ])("MixedCasing_PlayerTwoWins", (player1Move, player2Move) => {
      game.Play(player1Move, player2Move);
      expect(game.Result()).toBe("Player 2 wins");
    });

    test.each([
      ["✂️", "🪨"],
      ["🪨", "📄"],
      ["📄", "✂️"],
      ["✌️", "✊"],
      ["👊", "✋"],
    ])("EmojiMoves_PlayerTwoWins", (player1Move, player2Move) => {
      game.Play(player1Move, player2Move);
      expect(game.Result()).toBe("Player 2 wins");
    });

    test.each([
      ["✂️", "rock"],
      ["rock", "📄"],
      ["✋", "scissors"],
    ])("MixedTextAndEmoji_PlayerTwoWins", (player1Move, player2Move) => {
      game.Play(player1Move, player2Move);
      expect(game.Result()).toBe("Player 2 wins");
    });
  });

  describe("Draws", () => {
    test.each([
      ["rock", "rock"],
      ["paper", "paper"],
      ["scissors", "scissors"],
      ["ROCK", "rock"],
      ["Paper", "PAPER"],
    ])("SameTextMove_IsDraw", (player1Move, player2Move) => {
      game.Play(player1Move, player2Move);
      expect(game.Result()).toBe("Draw");
    });

    test.each([
      ["🪨", "🪨"],
      ["📄", "📄"],
      ["✂️", "✂️"],
      ["✊", "👊"],
      ["✋", "📃"],
      ["✌️", "✂"],
    ])("SameEmojiMove_IsDraw", (player1Move, player2Move) => {
      game.Play(player1Move, player2Move);
      expect(game.Result()).toBe("Draw");
    });

    test.each([
      ["rock", "🪨"],
      ["✊", "ROCK"],
      ["paper", "📄"],
      ["✋", "Paper"],
      ["scissors", "✂️"],
      ["✌️", "SCISSORS"],
    ])("EquivalentTextAndEmoji_IsDraw", (player1Move, player2Move) => {
      game.Play(player1Move, player2Move);
      expect(game.Result()).toBe("Draw");
    });
  });

  describe("InvalidMoves", () => {
    test.each([
      [null, "rock"],
      ["rock", null],
      [null, null],
    ])("NullMove_ThrowsArgumentException", (player1Move, player2Move) => {
      expect(() => game.Play(player1Move, player2Move)).toThrow(ArgumentException);
    });

    test.each([
      ["", "rock"],
      ["rock", ""],
    ])("EmptyMove_ThrowsArgumentException", (player1Move, player2Move) => {
      expect(() => game.Play(player1Move, player2Move)).toThrow(ArgumentException);
    });

    test.each([
      ["lizard", "rock"],
      ["rock", "spock"],
      ["banana", "paper"],
      ["fire", "water"],
      ["🪨🪨", "rock"],
      ["r", "paper"],
    ])("UnknownMove_ThrowsArgumentException", (player1Move, player2Move) => {
      expect(() => game.Play(player1Move, player2Move)).toThrow(ArgumentException);
    });
  });

  describe("MatchResult", () => {
    test("NoRoundsPlayed_IsDraw", () => {
      expect(game.Result()).toBe("Draw");
    });

    test("AfterFirstWin_ThatPlayerIsWinning", () => {
      game.Play("rock", "scissors");
      expect(game.Result()).toBe("Player 1 wins");
    });

    test("AfterFirstWin_SecondRoundDraw_StillWinning", () => {
      game.Play("rock", "scissors");
      game.Play("paper", "paper");
      expect(game.Result()).toBe("Player 1 wins");
    });

    test("AfterFirstWin_SecondRoundLoss_IsDraw", () => {
      game.Play("rock", "scissors");
      game.Play("rock", "paper");
      expect(game.Result()).toBe("Draw");
    });

    test("PlayerTwoCanLead", () => {
      game.Play("rock", "paper");
      expect(game.Result()).toBe("Player 2 wins");
      game.Play("paper", "scissors");
      expect(game.Result()).toBe("Player 2 wins");
    });

    test("KeepPlaying_ResultTracksWhoIsAhead", () => {
      game.Play("rock", "scissors");
      expect(game.Result()).toBe("Player 1 wins");

      game.Play("rock", "paper");
      expect(game.Result()).toBe("Draw");

      game.Play("scissors", "paper");
      expect(game.Result()).toBe("Player 1 wins");

      game.Play("📄", "🪨");
      expect(game.Result()).toBe("Player 1 wins");
    });

    test("InvalidMove_DoesNotChangeResult", () => {
      game.Play("rock", "scissors");
      expect(() => game.Play("lizard", "rock")).toThrow(ArgumentException);
      expect(game.Result()).toBe("Player 1 wins");
    });
  });
});
