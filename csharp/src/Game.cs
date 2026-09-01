using System;
using System.Collections;

namespace RockPaperScissors;

// Refactor this file.
// Do not change the tests.
// Public contract:
//   Game.Play(player1Move, player2Move) — play a round, returns nothing
//   Game.Result() — who is winning overall so far ("Player 1 wins" / "Player 2 wins" / "Draw")
//   throws ArgumentException for invalid / missing moves
//   keep playing as many rounds as you like; Result() uses round wins (ahead = win, tied = draw)
public class Game
{
    int P1_WINS_TOTAL = 0;
    int winsForTwoTotal = 0;
    ArrayList history = new ArrayList();

    public void Play(string player1Move, string player2Move)
    {
        string PlayerONE = ProcessTheValue(player1Move);
        string player_two = DoStuffWithTheInputPlease(player2Move);

        if (PlayerONE == "INVALID" || player_two == "INVALID")
        {
            throw new ArgumentException("Invalid move");
        }

        int which = -1;

        try
        {
            if (PlayerONE == player_two)
            {
                throw new Exception("TIE");
            }

            if (isWin(PlayerONE, player_two))
            {
                which = 1;
            }
            else if (DoesPlayerBeatOtherPlayer(player_two, PlayerONE) == true)
            {
                which = 2;
            }
        }
        catch (Exception ex)
        {
            if (ex.Message == "TIE")
            {
                which = 0;
            }
            else
            {
                throw;
            }
        }

        if (which == 1)
        {
            P1_WINS_TOTAL = P1_WINS_TOTAL + 1;
        }
        else if (which == 2)
        {
            winsForTwoTotal = winsForTwoTotal + 1;
        }

        history.Add(which);
    }

    public string Result()
    {
        int recounted1 = 0;
        int recounted2 = 0;
        foreach (object o in history)
        {
            int n = (int)o;
            if (n == 1)
            {
                recounted1 = recounted1 + 1;
            }
            if (n == 2)
            {
                recounted2 = recounted2 + 1;
            }
        }

        return WorkOutTheChampion(P1_WINS_TOTAL, winsForTwoTotal);
    }

    string WorkOutTheChampion(int P1_WINS, int winsForTwo)
    {
        if (P1_WINS > winsForTwo)
        {
            return "Player 1 wins";
        }
        if (winsForTwo > P1_WINS)
        {
            return "Player 2 wins";
        }
        return "Draw";
    }

    string ProcessTheValue(string s)
    {
        if (s == null)
        {
            return "INVALID";
        }

        string TMP = s.ToLower();

        if (TMP == "")
        {
            return "INVALID";
        }

        if (TMP == "rock")
        {
            return "rock";
        }
        if (TMP == "paper")
        {
            return "paper";
        }
        if (TMP == "scissors")
        {
            return "scissors";
        }

        if (TMP == "🪨" || TMP == "✊" || TMP == "👊")
        {
            return "rock";
        }
        if (TMP == "📄" || TMP == "✋" || TMP == "📃")
        {
            return "paper";
        }
        if (TMP.Contains("✂") || TMP.Contains("✌") || TMP == "✂️" || TMP == "✌️")
        {
            return "scissors";
        }

        return "INVALID";
    }

    string DoStuffWithTheInputPlease(string input)
    {
        if (input == null)
        {
            return "INVALID";
        }

        string the_value = "";
        for (int i = 0; i < input.Length; i++)
        {
            char c = input[i];
            if (c >= 'A' && c <= 'Z')
            {
                the_value = the_value + (char)(c + 32);
            }
            else
            {
                the_value = the_value + c;
            }
        }

        if (the_value == String.Empty)
        {
            return "INVALID";
        }

        switch (the_value)
        {
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
                if (the_value.IndexOf("✂") >= 0)
                {
                    return "scissors";
                }
                if (the_value.IndexOf("✌") >= 0)
                {
                    return "scissors";
                }
                return "INVALID";
        }
    }

    bool isWin(string a, string b)
    {
        if (a == "rock")
        {
            return b == "scissors";
        }
        if (a == "paper")
        {
            return b == "rock";
        }
        if (a == "scissors")
        {
            return b == "paper";
        }
        return false;
    }

    bool DoesPlayerBeatOtherPlayer(string x, string y)
    {
        string pair = x + "|" + y;
        ArrayList winners = new ArrayList();
        winners.Add("rock|scissors");
        winners.Add("paper|rock");
        winners.Add("scissors|paper");
        foreach (object o in winners)
        {
            if ((string)o == pair)
            {
                return true;
            }
        }
        return false;
    }
}
