using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour
{


	public int[] playerScores = new int[4];
	public int totalPlayers;
	public int targetRounds;
	public int currentRound;
	public int lastWinnerPlayerNum;
	public int lastWinnerCharNum;

	public int[] playerNumtoChars = new int[4];
    // Start is called before the first frame update
    void Start()
    {
		DontDestroyOnLoad(gameObject);

	}

    // Update is called once per frame
    void Update()
    {
		


	}


}
