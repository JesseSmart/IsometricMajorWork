using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class CharSelectMaster : MonoBehaviour
{
    public int playerNumber;
    private string[] aButtonArray = new string[4] { "P1AButton", "P2AButton", "P3AButton", "P4AButton" };
    private string[] horizontalArray = new string[4] { "P1Horizontal", "P2Horizontal", "P3Horizontal", "P4Horizontal" };
    private string[] verticalArray = new string[4] { "P1Vertical", "P2Vertical", "P3Vertical", "P4Vertical" };

    public GameObject panelCharSelect;
    public GameObject panelPressToJoin;

    public Button[] charSelButtons;
    public TextMeshProUGUI[] abilityDexcriptions;

    private bool SelectingChar;

    private GameObject eventSystemObj;
    public Button firstSelectedButton;

    public int totalChars;

    // Start is called before the first frame update
    void Start()
    {
        eventSystemObj = GameObject.FindGameObjectWithTag("EventSystem");

    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput(playerNumber);
    }

    private void PlayerInput(int pNum)
    {
        if (SelectingChar == false)
        {
            if (Input.GetButtonDown(aButtonArray[pNum]))
            {
                panelCharSelect.SetActive(true);
                eventSystemObj.GetComponent<EventSystem>().SetSelectedGameObject(firstSelectedButton.gameObject);
                panelPressToJoin.SetActive(false);

            }
        }

    }

    public void OnChar1Select()
    {
        CharSelect(playerNumber, 0);
    }

    public void OnChar2Select()
    {
        CharSelect(playerNumber, 1);

    }

    public void OnChar3Select()
    {
        CharSelect(playerNumber, 2);

    }

    public void OnChar4Select()
    {
        CharSelect(playerNumber, 3);

    }

    private void CharSelect(int pNum, int charNum)
    {
        PlayerPrefs.SetInt("CharacterPlayer" + pNum, charNum);
    }


}
