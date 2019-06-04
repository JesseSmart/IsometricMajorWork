using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerJoinScreenMaster : MonoBehaviour
{
    private string[] basicAbilityArray = new string[4] { "P1AButton", "P2AButton", "P3AButton", "P4AButton" };

    public GameObject modifierPanel;

    public Button btnNext;

    public int totalPlayers;

    public GameObject[] joinUIObjects;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBackClick()
    {
        modifierPanel.SetActive(true);
        gameObject.SetActive(false);
        //reset character select values
    }

    public void OnNextPressed()
    {
        if (totalPlayers >= 2)
        {
            PlayerPrefs.SetInt("TotalPlayers", totalPlayers);

        }


        //load scene 

    }
}
