using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class RoundManager : MonoBehaviour
{
	public GameObject[] spawnPoints;
	public GameObject[] characterObjs;

	private MatchManager matchManager;

	private int roundDur;
	//private int winTarget;

	private int roundWinner;
	private bool hasEnded;

	//public TextMeshProUGUI countdownString;
	private int countInd;

	public GameObject[] InGameUis;

	public Image CountdownImage;
	public Image readyImage;
	public Image beginImage;
	public Sprite[] CountdownSprites;
	// Start is called before the first frame update
	void Start()
    {
		//winTarget = PlayerPrefs.GetInt("winPointTarget");
		roundDur = PlayerPrefs.GetInt("RoundDuration");


		matchManager = FindObjectOfType<MatchManager>();
		StartCoroutine(StartCountdown());

		matchManager.targetRounds = PlayerPrefs.GetInt("winPointTarget");
		matchManager.currentRound++; 
		//Spawn();
	}

    // Update is called once per frame
    void Update()
	{ 
		CharacterCommon[] charCom = FindObjectsOfType<CharacterCommon>();
		if (charCom.Length == 1 && !hasEnded)
		{
			roundWinner = FindObjectOfType<IsometricPlayerMovementController>().playerNumber;
			StartCoroutine(LoadNext());
			hasEnded = true;
		}
    }

	void Spawn()
	{
		int total = PlayerPrefs.GetInt("TotalPlayers");
		print("TOTAL ===================" + total);
		InGameUis[total - 2].SetActive(true);

		print("Spawn a total of " + total);
		for (int i = 0; i < total; i++) //could be totsl - 1
		{
			//character int PlayerPrefs.GetInt("CharacterPlayer" + i);
			
			GameObject character = Instantiate(characterObjs[PlayerPrefs.GetInt("CharacterPlayer" + i)], spawnPoints[i].transform.position, spawnPoints[i].transform.rotation);
			character.GetComponent<IsometricPlayerMovementController>().playerNumber = i;
			FindObjectOfType<MatchManager>().playerNumtoChars[i] = PlayerPrefs.GetInt("CharacterPlayer" + i);
			print("spawn");
		}

		FindObjectOfType<CameraController>().PlayersHaveSpawned();
	}

	IEnumerator LoadNext()
	{
		yield return new WaitForSeconds(2f);
		matchManager.playerScores[roundWinner]++;
		if (matchManager.playerScores[roundWinner] > matchManager.targetRounds)
		{
			//SceneManager.LoadScene("Menu");
			print("LOADERRORRR");
			print("scores" + matchManager.playerScores[roundWinner]);
			print("targetrounds" + matchManager.targetRounds);
		}
		else
		{
			if (FindObjectOfType<VikingController>())
			{
				FindObjectOfType<MatchManager>().lastWinnerCharNum = 0;
				FindObjectOfType<MatchManager>().lastWinnerPlayerNum = FindObjectOfType<VikingController>().pNum;
			}
			else if (FindObjectOfType<TimeFreakController>())
			{
				FindObjectOfType<MatchManager>().lastWinnerCharNum = 1;
				FindObjectOfType<MatchManager>().lastWinnerPlayerNum = FindObjectOfType<TimeFreakController>().pNum;

			}
			else if (FindObjectOfType<GunFreakController>())
			{
				FindObjectOfType<MatchManager>().lastWinnerCharNum = 2;
				FindObjectOfType<MatchManager>().lastWinnerPlayerNum = FindObjectOfType<GunFreakController>().pNum;

			}
			else if (FindObjectOfType<ZenFreakController>())
			{
				FindObjectOfType<MatchManager>().lastWinnerCharNum = 3;
				FindObjectOfType<MatchManager>().lastWinnerPlayerNum = FindObjectOfType<ZenFreakController>().pNum;

			}
			else
			{
				print("VIC ERROR");
			}
			SceneManager.LoadScene("MidScoresScreen");

		}
	}


	IEnumerator StartCountdown()
	{
		countInd++;
		yield return new WaitForSecondsRealtime(1f);
		if (countInd < 4)
		{
			readyImage.enabled = false;
			CountdownImage.enabled = true;

			CountdownImage.sprite = CountdownSprites[3 - countInd];
			StartCoroutine(StartCountdown());
		}
		else if (countInd == 4)
		{
			beginImage.enabled = true;
			CountdownImage.enabled = false;

			StartCoroutine(StartCountdown());
		}
		else
		{
			CountdownImage.enabled = false;
			beginImage.enabled = false;
			Spawn();
		}
	}



}
