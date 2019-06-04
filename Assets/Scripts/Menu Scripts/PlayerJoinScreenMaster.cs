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

    public GameObject[] joinUIObjects;

    public GameObject txtObjPressStart;


    public GameObject eventSystemObj;
    public Button onBackSelectedButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //totalPlayers = GetComponentInChildren<CharSelectMaster>().totalPlayers;



        if (totalPlayers >= 2)
        {
            txtObjPressStart.SetActive(true);

            if (Input.GetButtonDown(startButtonArray[0]))
            {
                StartPressed();
            }
        }
        else
        {
            txtObjPressStart.SetActive(false);

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
        if (totalPlayers >= 2)
        {
            PlayerPrefs.SetInt("TotalPlayers", totalPlayers);
        }
        print("LOAD SCENE");
        //load scene 

    }
}
