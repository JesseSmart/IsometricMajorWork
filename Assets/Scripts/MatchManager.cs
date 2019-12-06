using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MatchManager : MonoBehaviour
{


	public int[] playerScores = new int[4];
	public int totalPlayers;
	public int targetRounds;
	public int currentRound;
	public int lastWinnerPlayerNum;
	public int lastWinnerCharNum;
	public int lastFightScene;
	public int[] playerNumtoChars = new int[4];
    // Start is called before the first frame update
    void Start()
    {
		DontDestroyOnLoad(gameObject);

		Scene scene = SceneManager.GetActiveScene();
		if (scene.name == "Menu")
		{
			print("IS MENU");

			playerScores[0] = 0;
			playerScores[1] = 0;
			playerScores[2] = 0;
			playerScores[3] = 0;
			totalPlayers = 0;
			targetRounds = 0;
			currentRound = 0;
			lastWinnerPlayerNum = 0;
			lastWinnerCharNum = 0;

		}

	}

    // Update is called once per frame
    void Update()
    {
		


	}


}
