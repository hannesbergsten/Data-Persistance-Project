using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using File = System.IO.File;

[Serializable]
public class Player
{
    public string name;
    public int score;
}

[Serializable]
public class ScoreBoard : MonoBehaviour
{
    public static ScoreBoard Instance;

    [SerializeField] private List<Player> leaderBoard = new List<Player>();

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
        var existingPlayer = leaderBoard.Find(x => x.name == player.name);
        if (existingPlayer != null && existingPlayer.score < player.score)
        {
            existingPlayer.score = player.score;
        }
        else
        {
            leaderBoard.Add(player);
            leaderBoard = leaderBoard.OrderByDescending(p => p.score).Take(3).ToList();
        }

        SaveTopPlayerSession();
    }

    private void SaveTopPlayerSession()
    {
        var json = JsonUtility.ToJson(leaderBoard.First());
        File.WriteAllText(Application.persistentDataPath + "/leaderboard.json", json);
    }

    public Player GetTopPlayer()
    {
        var path = Application.persistentDataPath + "/leaderboard.json";
        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);
            Debug.Log(json);

            var lb = JsonUtility.FromJson<Player>(json);
            return lb;
        }
        return null;
    }
}