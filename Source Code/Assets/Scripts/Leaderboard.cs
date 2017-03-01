using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;

public class Leaderboard : MonoBehaviour {
	TcpClient client;

	// Use this for initialization
	void Start () {
        try {
            client = new TcpClient("localhost", 9999);
        } catch (SocketException e) {
            Debug.Log("Connection to leaderboard server couldn't be made.");
        }

        //SubmitScore ("Evgeniy", 132);
        //SubmitScore ("Gabz", -5);
        Debug.Log(ReceiveScore());
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
}
