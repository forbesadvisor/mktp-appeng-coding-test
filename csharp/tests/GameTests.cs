using RockPaperScissors;

namespace RockPaperScissors.Tests;

public class GameTests
{
    private readonly Game _game = new();

    public class PlayerOneWins : GameTests
    {
        [Theory]
        [InlineData("rock", "scissors")]
        [InlineData("paper", "rock")]
        [InlineData("scissors", "paper")]
        public void TextMoves_PlayerOneWins(string player1Move, string player2Move)
        {
            _game.Play(player1Move, player2Move);
            Assert.Equal("Player 1 wins", _game.Result());
        }

        [Theory]
        [InlineData("ROCK", "scissors")]
        [InlineData("Paper", "ROCK")]
        [InlineData("Scissors", "pApEr")]
        [InlineData("rOcK", "SCISSORS")]
        public void MixedCasing_PlayerOneWins(string player1Move, string player2Move)
        {
            _game.Play(player1Move, player2Move);
            Assert.Equal("Player 1 wins", _game.Result());
        }

        [Theory]
        [InlineData("🪨", "✂️")]
        [InlineData("✊", "✌️")]
        [InlineData("👊", "✂")]
        [InlineData("📄", "🪨")]
        [InlineData("✋", "✊")]
        [InlineData("📃", "👊")]
        [InlineData("✂️", "📄")]
        [InlineData("✌️", "✋")]
        [InlineData("✂", "📃")]
        public void EmojiMoves_PlayerOneWins(string player1Move, string player2Move)
        {
            _game.Play(player1Move, player2Move);
            Assert.Equal("Player 1 wins", _game.Result());
        }

        [Theory]
        [InlineData("rock", "✂️")]
        [InlineData("📄", "rock")]
        [InlineData("scissors", "✋")]
        [InlineData("✊", "scissors")]
        [InlineData("Paper", "🪨")]
        [InlineData("✌️", "paper")]
        public void MixedTextAndEmoji_PlayerOneWins(string player1Move, string player2Move)
        {
            _game.Play(player1Move, player2Move);
            Assert.Equal("Player 1 wins", _game.Result());
        }
    }

    public class PlayerTwoWins : GameTests
    {
        [Theory]
        [InlineData("scissors", "rock")]
        [InlineData("rock", "paper")]
        [InlineData("paper", "scissors")]
        public void TextMoves_PlayerTwoWins(string player1Move, string player2Move)
        {
            _game.Play(player1Move, player2Move);
            Assert.Equal("Player 2 wins", _game.Result());
        }

        [Theory]
        [InlineData("SCISSORS", "Rock")]
        [InlineData("rock", "PAPER")]
        [InlineData("PaPeR", "sCiSsOrS")]
        public void MixedCasing_PlayerTwoWins(string player1Move, string player2Move)
        {
            _game.Play(player1Move, player2Move);
            Assert.Equal("Player 2 wins", _game.Result());
        }

        [Theory]
        [InlineData("✂️", "🪨")]
        [InlineData("🪨", "📄")]
        [InlineData("📄", "✂️")]
        [InlineData("✌️", "✊")]
        [InlineData("👊", "✋")]
        public void EmojiMoves_PlayerTwoWins(string player1Move, string player2Move)
        {
            _game.Play(player1Move, player2Move);
            Assert.Equal("Player 2 wins", _game.Result());
        }

        [Theory]
        [InlineData("✂️", "rock")]
        [InlineData("rock", "📄")]
        [InlineData("✋", "scissors")]
        public void MixedTextAndEmoji_PlayerTwoWins(string player1Move, string player2Move)
        {
            _game.Play(player1Move, player2Move);
            Assert.Equal("Player 2 wins", _game.Result());
        }
    }

    public class Draws : GameTests
    {
        [Theory]
        [InlineData("rock", "rock")]
        [InlineData("paper", "paper")]
        [InlineData("scissors", "scissors")]
        [InlineData("ROCK", "rock")]
        [InlineData("Paper", "PAPER")]
        public void SameTextMove_IsDraw(string player1Move, string player2Move)
        {
            _game.Play(player1Move, player2Move);
            Assert.Equal("Draw", _game.Result());
        }

        [Theory]
        [InlineData("🪨", "🪨")]
        [InlineData("📄", "📄")]
        [InlineData("✂️", "✂️")]
        [InlineData("✊", "👊")]
        [InlineData("✋", "📃")]
        [InlineData("✌️", "✂")]
        public void SameEmojiMove_IsDraw(string player1Move, string player2Move)
        {
            _game.Play(player1Move, player2Move);
            Assert.Equal("Draw", _game.Result());
        }

        [Theory]
        [InlineData("rock", "🪨")]
        [InlineData("✊", "ROCK")]
        [InlineData("paper", "📄")]
        [InlineData("✋", "Paper")]
        [InlineData("scissors", "✂️")]
        [InlineData("✌️", "SCISSORS")]
        public void EquivalentTextAndEmoji_IsDraw(string player1Move, string player2Move)
        {
            _game.Play(player1Move, player2Move);
            Assert.Equal("Draw", _game.Result());
        }
    }

    public class InvalidMoves : GameTests
    {
        [Theory]
        [InlineData(null, "rock")]
        [InlineData("rock", null)]
        [InlineData(null, null)]
        public void NullMove_ThrowsArgumentException(string? player1Move, string? player2Move)
        {
            Assert.Throws<ArgumentException>(() => _game.Play(player1Move!, player2Move!));
        }

        [Theory]
        [InlineData("", "rock")]
        [InlineData("rock", "")]
        public void EmptyMove_ThrowsArgumentException(string player1Move, string player2Move)
        {
            Assert.Throws<ArgumentException>(() => _game.Play(player1Move, player2Move));
        }

        [Theory]
        [InlineData("lizard", "rock")]
        [InlineData("rock", "spock")]
        [InlineData("banana", "paper")]
        [InlineData("fire", "water")]
        [InlineData("🪨🪨", "rock")]
        [InlineData("r", "paper")]
        public void UnknownMove_ThrowsArgumentException(string player1Move, string player2Move)
        {
            Assert.Throws<ArgumentException>(() => _game.Play(player1Move, player2Move));
        }
    }

    public class MatchResult : GameTests
    {
        [Fact]
        public void NoRoundsPlayed_IsDraw()
        {
            Assert.Equal("Draw", _game.Result());
        }

        [Fact]
        public void AfterFirstWin_ThatPlayerIsWinning()
        {
            _game.Play("rock", "scissors");
            Assert.Equal("Player 1 wins", _game.Result());
        }

        [Fact]
        public void AfterFirstWin_SecondRoundDraw_StillWinning()
        {
            _game.Play("rock", "scissors");
            _game.Play("paper", "paper");
            Assert.Equal("Player 1 wins", _game.Result());
        }

        [Fact]
        public void AfterFirstWin_SecondRoundLoss_IsDraw()
        {
            _game.Play("rock", "scissors");
            _game.Play("rock", "paper");
            Assert.Equal("Draw", _game.Result());
        }

        [Fact]
        public void PlayerTwoCanLead()
        {
            _game.Play("rock", "paper");
            Assert.Equal("Player 2 wins", _game.Result());
            _game.Play("paper", "scissors");
            Assert.Equal("Player 2 wins", _game.Result());
        }

        [Fact]
        public void KeepPlaying_ResultTracksWhoIsAhead()
        {
            _game.Play("rock", "scissors");
            Assert.Equal("Player 1 wins", _game.Result());

            _game.Play("rock", "paper");
            Assert.Equal("Draw", _game.Result());

            _game.Play("scissors", "paper");
            Assert.Equal("Player 1 wins", _game.Result());

            _game.Play("📄", "🪨");
            Assert.Equal("Player 1 wins", _game.Result());
        }

        [Fact]
        public void InvalidMove_DoesNotChangeResult()
        {
            _game.Play("rock", "scissors");
            Assert.Throws<ArgumentException>(() => _game.Play("lizard", "rock"));
            Assert.Equal("Player 1 wins", _game.Result());
        }
    }
}
