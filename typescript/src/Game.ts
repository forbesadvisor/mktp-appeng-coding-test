// Refactor this file.
// Do not change the tests.
// Public contract:
//   Game.Play(player1Move, player2Move) — play a round, returns nothing
//   Game.Result() — who is winning overall so far ("Player 1 wins" / "Player 2 wins" / "Draw")
//   throws ArgumentException for invalid / missing moves
//   keep playing as many rounds as you like; Result() uses round wins (ahead = win, tied = draw)

export class ArgumentException extends Error {
  constructor(message?: string) {
    super(message);
    this.name = "ArgumentException";
  }
}

export class Game {
  P1_WINS_TOTAL = 0;
  winsForTwoTotal = 0;
  history: any[] = [];

  Play(player1Move: any, player2Move: any) {
    const PlayerONE = this.ProcessTheValue(player1Move);
    const player_two = this.DoStuffWithTheInputPlease(player2Move);

    if (PlayerONE == "INVALID" || player_two == "INVALID") {
      throw new ArgumentException("Invalid move");
    }

    let which = -1;

    try {
      if (PlayerONE == player_two) {
        throw new Error("TIE");
      }

      if (this.isWin(PlayerONE, player_two)) {
        which = 1;
      } else if (this.DoesPlayerBeatOtherPlayer(player_two, PlayerONE) == true) {
        which = 2;
      }
    } catch (ex: any) {
      if (ex.message == "TIE") {
        which = 0;
      } else {
        throw ex;
      }
    }

    if (which == 1) {
      this.P1_WINS_TOTAL = this.P1_WINS_TOTAL + 1;
    } else if (which == 2) {
      this.winsForTwoTotal = this.winsForTwoTotal + 1;
    }

    this.history.push(which);
  }

  Result() {
    let recounted1 = 0;
    let recounted2 = 0;
    for (const o of this.history) {
      const n = o as number;
      if (n == 1) {
        recounted1 = recounted1 + 1;
      }
      if (n == 2) {
        recounted2 = recounted2 + 1;
      }
    }

    return this.WorkOutTheChampion(this.P1_WINS_TOTAL, this.winsForTwoTotal);
  }

  WorkOutTheChampion(P1_WINS: number, winsForTwo: number) {
    if (P1_WINS > winsForTwo) {
      return "Player 1 wins";
    }
    if (winsForTwo > P1_WINS) {
      return "Player 2 wins";
    }
    return "Draw";
  }

  ProcessTheValue(s: any) {
    if (s == null) {
      return "INVALID";
    }

    const TMP = s.toLowerCase();

    if (TMP == "") {
      return "INVALID";
    }

    if (TMP == "rock") {
      return "rock";
    }
    if (TMP == "paper") {
      return "paper";
    }
    if (TMP == "scissors") {
      return "scissors";
    }

    if (TMP == "🪨" || TMP == "✊" || TMP == "👊") {
      return "rock";
    }
    if (TMP == "📄" || TMP == "✋" || TMP == "📃") {
      return "paper";
    }
    if (TMP.includes("✂") || TMP.includes("✌") || TMP == "✂️" || TMP == "✌️") {
      return "scissors";
    }

    return "INVALID";
  }

  DoStuffWithTheInputPlease(input: any) {
    if (input == null) {
      return "INVALID";
    }

    let the_value = "";
    for (let i = 0; i < input.length; i++) {
      const c = input[i];
      if (c >= "A" && c <= "Z") {
        the_value = the_value + String.fromCharCode(c.charCodeAt(0) + 32);
      } else {
        the_value = the_value + c;
      }
    }

    if (the_value == "") {
      return "INVALID";
    }

    switch (the_value) {
      case "rock":
      case "🪨":
      case "✊":
      case "👊":
        return "rock";
      case "paper":
      case "📄":
      case "✋":
      case "📃":
        return "paper";
      case "scissors":
        return "scissors";
      default:
        if (the_value.indexOf("✂") >= 0) {
          return "scissors";
        }
        if (the_value.indexOf("✌") >= 0) {
          return "scissors";
        }
        return "INVALID";
    }
  }

  isWin(a: string, b: string) {
    if (a == "rock") {
      return b == "scissors";
    }
    if (a == "paper") {
      return b == "rock";
    }
    if (a == "scissors") {
      return b == "paper";
    }
    return false;
  }

  DoesPlayerBeatOtherPlayer(x: string, y: string) {
    const pair = x + "|" + y;
    const winners: any[] = [];
    winners.push("rock|scissors");
    winners.push("paper|rock");
    winners.push("scissors|paper");
    for (const o of winners) {
      if ((o as string) == pair) {
        return true;
      }
    }
    return false;
  }
}
