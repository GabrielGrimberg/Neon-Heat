using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelMananger : MonoBehaviour 
{
	public void PlayGame(string newGame)
	{
		SceneManager.LoadScene(newGame);
	}

	public void EndGame()
	{
		print("Quit is pressed");
		Application.Quit();
	}
		
}