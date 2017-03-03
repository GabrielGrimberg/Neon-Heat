using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;

public class Leaderboard : MonoBehaviour {
	TcpClient client;

	// Use this for initialization
	void Start () {
        try {
            client = new TcpClient("192.168.1.14", 9999);
        } catch (SocketException e) {
            Debug.Log("Connection to leaderboard server couldn't be made.");
        }

        //SubmitScore ("Evgeniy", 132);
        //SubmitScore ("Gabz", -5);
        //SubmitScore("Daniel", 2344);
        //SubmitScore("Sportsmen", 999999);
        //SubmitScore("Evgeniy", 132);
        //SubmitScore("Gabz", -5);
        //SubmitScore("Abdul", 123123);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SubmitScore(string name, int score) {
        if (client == null) return;

		NetworkStream stream = client.GetStream();
		byte[] data = System.Text.Encoding.ASCII.GetBytes(name + "#" + score.ToString());
		stream.Write(data, 0, data.Length);
	}

	public string ReceiveScore() {
        if (client == null) return string.Empty;

        NetworkStream stream = client.GetStream();
		byte[] data = System.Text.Encoding.ASCII.GetBytes("RESULTS"); 
		stream.Write(data, 0, data.Length);

		byte[] receive = new byte[10000];
		int bytes = stream.Read(receive, 0, receive.Length);
		string responseData = System.Text.Encoding.UTF8.GetString(receive, 0, bytes);

		return responseData;
	}

    public List<PlayerScore> GetPlayerScore() {
        List<PlayerScore> playerScores = new List<PlayerScore>();

        string data = ReceiveScore();
        string[] commaSplit = data.Split(',');

        foreach (string namescore in commaSplit) {
            if (namescore == "###") {
                continue;
            }

            playerScores.Add(new PlayerScore(namescore.Split('#')[0], int.Parse(namescore.Split('#')[1])));
        }

        return playerScores;
    } 

    public class PlayerScore {
        public string player;
        public int score;

        public PlayerScore(string player, int score) {
            this.player = player;
            this.score = score;
        }
    }
}
