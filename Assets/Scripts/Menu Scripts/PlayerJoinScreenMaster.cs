using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class PlayerJoinScreenMaster : MonoBehaviour
{
    private string[] aButtonArray = new string[4] { "P1AButton", "P2AButton", "P3AButton", "P4AButton" };
    private string[] startButtonArray = new string[4] { "P1StartButton", "P2StartButton", "P3StartButton", "P4StartButton" };

    public GameObject modifierPanel;

    public Button btnNext;
    public Button btnBack;

    public int totalPlayers;
	public int totatJoiningPlayers;

    public GameObject[] joinUIObjects;

	public GameObject imgPressStart;

    public GameObject eventSystemObj;
    public Button onBackSelectedButton;

	public GameObject[] charSelScreens;

	public int[] buildIndexArray;

	public bool[] totalPlayersAllowed = new bool[4];

	public bool legalGame;
	//private bool twoPAllowed;
	//private bool threePAllowed;
	//private bool fourPAllowed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (charSelScreens[1].GetComponent<CharSelectMaster>().myState == CharSelectMaster.SelectState.Selected)
		{
			totalPlayersAllowed[0] = true;
		}
		else
		{
			totalPlayersAllowed[0] = false;

		}

		if (charSelScreens[0].GetComponent<CharSelectMaster>().myState == CharSelectMaster.SelectState.Selected && charSelScreens[1].GetComponent<CharSelectMaster>().myState == CharSelectMaster.SelectState.Selected)
		{
			totalPlayersAllowed[1] = true;
		}
		else
		{
			totalPlayersAllowed[1] = false;

		}

		if (charSelScreens[0].GetComponent<CharSelectMaster>().myState == CharSelectMaster.SelectState.Selected && charSelScreens[1].GetComponent<CharSelectMaster>().myState == CharSelectMaster.SelectState.Selected && charSelScreens[2].GetComponent<CharSelectMaster>().myState == CharSelectMaster.SelectState.Selected)
		{
			totalPlayersAllowed[2] = true;
		}
		else
		{
			totalPlayersAllowed[2] = false;

		}

		if (charSelScreens[0].GetComponent<CharSelectMaster>().myState == CharSelectMaster.SelectState.Selected && charSelScreens[1].GetComponent<CharSelectMaster>().myState == CharSelectMaster.SelectState.Selected && charSelScreens[2].GetComponent<CharSelectMaster>().myState == CharSelectMaster.SelectState.Selected && charSelScreens[3].GetComponent<CharSelectMaster>().myState == CharSelectMaster.SelectState.Selected)
		{
			totalPlayersAllowed[3] = true;
		}
		else
		{
			totalPlayersAllowed[3] = false;

		}
		if (totalPlayers > 0)
		{
			if (totalPlayersAllowed[totalPlayers - 1] == true)
			{
				legalGame = true;
			}
			else
			{
				legalGame = false;
			}

		}
		//totalPlayers = GetComponentInChildren<CharSelectMaster>().totalPlayers;
		for (int i = 0; i < charSelScreens.Length; i++)
		{
			if (charSelScreens[i].GetComponent<CharSelectMaster>().myState == CharSelectMaster.SelectState.Selected)
			{
				totalPlayersAllowed[i] = true;
			}
			else
			{
				totalPlayersAllowed[i] = false;

			}
		}

        if (totalPlayers >= 2 && totatJoiningPlayers == totalPlayers && totalPlayersAllowed[totalPlayers - 1] && legalGame)
        {
			imgPressStart.SetActive(true);

            if (Input.GetButtonDown(startButtonArray[0]))
            {
                StartPressed();
            }
        }
        else
        {
			imgPressStart.SetActive(false);

		}
	}

    public void OnBackClick()
    {
        modifierPanel.SetActive(true);
        eventSystemObj.GetComponent<EventSystem>().SetSelectedGameObject(onBackSelectedButton.gameObject);
        gameObject.SetActive(false);

    }

    public void StartPressed()
    {
        if (totalPlayers >= 2 && totatJoiningPlayers == totalPlayers)
        {
            PlayerPrefs.SetInt("TotalPlayers", totalPlayers);
			FindObjectOfType<MatchManager>().totalPlayers = totalPlayers;
        }

		SceneManager.LoadScene(buildIndexArray[Random.Range(0, buildIndexArray.Length)]);
        //load scene 

    }
}
