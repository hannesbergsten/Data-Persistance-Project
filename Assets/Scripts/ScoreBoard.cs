using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player
{
    public string Name { get; set; }
    public int Score { get; set; }
}

public class ScoreBoard : MonoBehaviour
{
    public static ScoreBoard Instance;

    private List<Player> _leaderBoard = new List<Player>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(Instance);
    }

    public void UpdateLeaderBoard(Player player)
    {
        var existingPlayer = _leaderBoard.Find(x => x.Name == player.Name);
        if (existingPlayer != null && existingPlayer.Score < player.Score)
        {
            existingPlayer.Score = player.Score;
            return;
        }

        _leaderBoard.Add(player);
        _leaderBoard = _leaderBoard.OrderByDescending(p => p.Score).Take(3).ToList();
    }

    public Player GetTopPlayer()
    {
        return _leaderBoard.OrderByDescending(l => l.Score).FirstOrDefault();
    }

    public List<Player> GetScoreBoard()
    {
        return _leaderBoard;
    }
}