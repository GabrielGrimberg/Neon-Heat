using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class DrawLeaderBoard : MonoBehaviour {
    public GameObject start;
    Leaderboard board;
    GameObject leaderBoardTextPrefab;

	// Use this for initialization
	void Start () {
        board = gameObject.GetComponent<Leaderboard>();
        leaderBoardTextPrefab = Resources.Load("LeaderBoardPrefab") as GameObject;

        List<Leaderboard.PlayerScore> playerScores = board.GetPlayerScore();
        playerScores = playerScores.OrderByDescending(p => p.score).ToList();


        int i = 0;
        RectTransform pos = GameObject.FindGameObjectWithTag("LeaderboardStart").GetComponent<RectTransform>();
        foreach (Leaderboard.PlayerScore score in playerScores) {
            GameObject t = Instantiate(leaderBoardTextPrefab);
            t.GetComponent<RectTransform>().position = pos.position + (Vector3.down * i * 41);
            t.GetComponent<Text>().text = score.player;
            t.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);

            GameObject t1 = Instantiate(leaderBoardTextPrefab);
            t1.GetComponent<RectTransform>().position = pos.position + (Vector3.down * i * 41) + (Vector3.right * 290);
            t1.GetComponent<Text>().text = score.score.ToString();
            t1.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);
            i++;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
