using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class GameModifiersMaster : MonoBehaviour
{

    public int roundTotal;
    public int roundDuration;

    public GameObject mainMenuPanel;
    public GameObject playerJoinPanel;

    public TextMeshProUGUI txtRoundTotal;
    public TextMeshProUGUI txtRoundDuration;

    public Button btnNext;

    public GameObject eventSystemObj;
    public Button firstSelectedButton;
    public Button onBackSelectedButton;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (roundTotal >= 0 && roundDuration >= 0)
        {
            btnNext.interactable = true;
        }

        if (roundTotal <= 0)
        {
            roundTotal = 1;
            txtRoundTotal.text = roundTotal.ToString();

        }
        else if (roundTotal >= 10)
        {
            roundTotal = 9;
            txtRoundTotal.text = roundTotal.ToString();

        }

        if (roundDuration <= 0)
        {
            roundDuration = 10;
            txtRoundDuration.text = roundDuration.ToString();

        }
        else if (roundDuration > 60)
        {
            roundDuration = 60;
            txtRoundDuration.text = roundDuration.ToString();

        }
    }

    public void OnBackClick()
    {
        mainMenuPanel.SetActive(true);
        gameObject.SetActive(false);
        eventSystemObj.GetComponent<EventSystem>().SetSelectedGameObject(onBackSelectedButton.gameObject);

    }

    public void OnNextPressed()
    {
        playerJoinPanel.SetActive(true);
        eventSystemObj.GetComponent<EventSystem>().SetSelectedGameObject(firstSelectedButton.gameObject);

        gameObject.SetActive(false);

    }

    public void OnRoundInc()
    {
        //if isnt at maximum or minimum
        if (roundTotal > 0 && roundTotal < 10) //<-- make maximum
        {
            roundTotal += 2;
            PlayerPrefs.SetInt("RoundTotal", roundTotal);
            txtRoundTotal.text = roundTotal.ToString();
        }
    }

    public void OnRoundDec()
    {
        //if isnt at maximum or minimum
        if (roundTotal > 0 && roundTotal < 10) //<-- make maximum
        {
            roundTotal -= 2;
            PlayerPrefs.SetInt("RoundTotal", roundTotal);
            txtRoundTotal.text = roundTotal.ToString();
        }

    }

    public void OnTimerInc()
    {
        //if isnt at maximum or minimum
        if (roundDuration > 0 && roundDuration <= 60)
        {
            roundDuration += 10;
            PlayerPrefs.SetInt("RoundDuration", roundDuration);
            txtRoundDuration.text = roundDuration.ToString();
        }

    }

    public void OnTimerDec()
    {
        if (roundDuration > 0 && roundDuration <= 60)
        {
            roundDuration -= 10;
            PlayerPrefs.SetInt("RoundDuration", roundDuration);
            txtRoundDuration.text = roundDuration.ToString();
        }

    }
}
