﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RoundManager : MonoBehaviour
{
	public GameObject[] spawnPoints;
	public GameObject[] characterObjs;

	private MatchManager matchManager;

	private int roundDur;
	private int winTarget;

	private int roundWinner;
	private bool hasEnded;

	// Start is called before the first frame update
	void Start()
    {
		winTarget = PlayerPrefs.GetInt("winPointTarget");
		roundDur = PlayerPrefs.GetInt("RoundDuration");
		matchManager = FindObjectOfType<MatchManager>();
		Spawn();
    }

    // Update is called once per frame
    void Update()
	{ 
		CharacterCommon[] charCom = FindObjectsOfType<CharacterCommon>();
		if (charCom.Length <= 1 && !hasEnded)
		{
			roundWinner = FindObjectOfType<IsometricPlayerMovementController>().playerNumber;
			StartCoroutine(LoadNext());
			hasEnded = true;
		}
    }

	void Spawn()
	{
		int total = PlayerPrefs.GetInt("TotalPlayers");
		print("Spawn a total of " + total);
		for (int i = 0; i < total; i++) //could be totsl - 1
		{
			//character int PlayerPrefs.GetInt("CharacterPlayer" + i);
			
			GameObject character = Instantiate(characterObjs[PlayerPrefs.GetInt("CharacterPlayer" + i)], spawnPoints[i].transform.position, spawnPoints[i].transform.rotation);
			character.GetComponent<IsometricPlayerMovementController>().playerNumber = i;
			print("spawn");
		}
	}

	IEnumerator LoadNext()
	{
		yield return new WaitForSeconds(2f);
		matchManager.playerScores[roundWinner]++;
		if (matchManager.playerScores[roundWinner] >= winTarget)
		{
			SceneManager.LoadScene("Menu");
		}
		else
		{
			SceneManager.LoadScene("JesseTesting");

		}
	}
}