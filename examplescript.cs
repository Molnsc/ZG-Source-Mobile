using UnityEngine;
using System.Collections.Generic;

public class Matchmaker : MonoBehaviour
{
    public class Player
    {
        public int BaseLevel;
        public int XP;
        public int Trophies;
    }

    void Start()
    {
        Player currentPlayer = new Player { BaseLevel = 5, XP = 1200, Trophies = 300 };
        List<Player> allPlayers = GetOnlinePlayers();
        TryFindOpponent(currentPlayer, allPlayers);
    }

    List<Player> GetOnlinePlayers()
    {
        return new List<Player>
        {
            new Player { BaseLevel = 5, XP = 1150, Trophies = 310 },
            new Player { BaseLevel = 8, XP = 2000, Trophies = 800 }
        };
    }

    void TryFindOpponent(Player currentPlayer, List<Player> allPlayersOnServer)
    {
        Player matchedPlayer = null;

        foreach (Player other in allPlayersOnServer)
        {
            if (IsSimilar(currentPlayer, other))
            {
                matchedPlayer = other;
                break;
            }
        }

        if (matchedPlayer != null)
        {
            Attack(matchedPlayer);
        }
        else
        {
            Player bot = GenerateBot(currentPlayer);
            Attack(bot);
        }
    }

    bool IsSimilar(Player a, Player b)
    {
        return Mathf.Abs(a.BaseLevel - b.BaseLevel) <= 1 &&
               Mathf.Abs(a.XP - b.XP) <= 500 &&
               Mathf.Abs(a.Trophies - b.Trophies) <= 100;
    }

    Player GenerateBot(Player player)
    {
        return new Player
        {
            BaseLevel = player.BaseLevel,
            XP = player.XP + Random.Range(-200, 200),
            Trophies = player.Trophies + Random.Range(-50, 50)
        };
    }

    void Attack(Player target)
    {
        Debug.Log("Attacking target:");
        Debug.Log("Base Level: " + target.BaseLevel);
        Debug.Log("XP: " + target.XP);
        Debug.Log("Trophies: " + target.Trophies);

        bool win = Random.value > 0.5f;

        if (win)
        {
            Debug.Log("Result: Victory!");
        }
        else
        {
            Debug.Log("Result: Defeat.");
        }
    }
}
