using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class GameModifiersMaster : MonoBehaviour
{

    public int winPointTarget;
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
        //set duration to what is on screen
    }

    // Update is called once per frame
    void Update()
    {
        if (winPointTarget >= 0 && roundDuration >= 0)
        {
            btnNext.interactable = true;
        }

        if (winPointTarget <= 0)
        {
            winPointTarget = 1;
            txtRoundTotal.text = winPointTarget.ToString();

        }
        else if (winPointTarget >= 10)
        {
            winPointTarget = 9;
            txtRoundTotal.text = winPointTarget.ToString();

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
        if (winPointTarget > 0 && winPointTarget < 10) //<-- make maximum
        {
            winPointTarget ++;
            PlayerPrefs.SetInt("winPointTarget", winPointTarget);
            txtRoundTotal.text = winPointTarget.ToString();
        }
    }

    public void OnRoundDec()
    {
        //if isnt at maximum or minimum
        if (winPointTarget > 0 && winPointTarget < 10) //<-- make maximum
        {
            winPointTarget--;
            PlayerPrefs.SetInt("winPointTarget", winPointTarget);
            txtRoundTotal.text = winPointTarget.ToString();
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
