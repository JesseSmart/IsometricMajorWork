using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MidPointManager : MonoBehaviour
{
	public GameObject[] scorePanels;
	private int playerCount;
	private MatchManager matchManager;

	public int[] sceneBuildNumArray;

    // Start is called before the first frame update
    void Start()
    {
		matchManager = FindObjectOfType<MatchManager>();
		playerCount = matchManager.totalPlayers;

		switch (playerCount)
		{
			case 2:
				scorePanels[0].SetActive(true);
				break;
			case 3:
				scorePanels[1].SetActive(true);

				break;
			case 4:
				scorePanels[2].SetActive(true);

				break;

		}
		StartCoroutine(LoadNextScene());
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	IEnumerator LoadNextScene()
	{
		yield return new WaitForSeconds(4f);
		bool loaded = false;
		for (int i = 0; i < matchManager.playerScores.Length; i++)
		{
			if (matchManager.playerScores[i] >= matchManager.targetRounds) //&& loaded = false;
			{
				loaded = true;
				PlayerPrefs.SetInt("WinnerNum", i);
				PlayerPrefs.SetInt("WinnerChar", i);
				SceneManager.LoadScene("Victory");
			}

		}

		if (!loaded)
		{
			SceneManager.LoadScene(sceneBuildNumArray[Random.Range(0, sceneBuildNumArray.Length)]);
			//could use corutine to generate number and check to make sure its not the same scene, then continue to generate. (use "while")
		}

	}
}
