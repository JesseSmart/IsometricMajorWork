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
    private string[] xButtonArray = new string[4] { "P1XButton", "P2XButton", "P3XButton", "P4XButton" };
    private string[] bButtonArray = new string[4] { "P1BButton", "P2BButton", "P3BButton", "P4BButton" };
    private string[] horizontalArray = new string[4] { "P1Horizontal", "P2Horizontal", "P3Horizontal", "P4Horizontal" };
    private string[] verticalArray = new string[4] { "P1Vertical", "P2Vertical", "P3Vertical", "P4Vertical" };
    private string[] leftBumperArray = new string[4] { "P1LeftBumper", "P2LeftBumper", "P3LeftBumper", "P4LeftBumper" };
    private string[] rightBumperArray = new string[4] { "P1RightBumper", "P2RightBumper", "P3RightBumper", "P4RightBumper" };

    public enum SelectState
    {
        WaitForJoin,
        Selecting,
        Selected
    }

    public SelectState myState;

    public CharacterStats[] characterStats;

    public GameObject panelCharSelect;
    public GameObject panelPressToJoin;

    //public TextMeshProUGUI[] abilityDescObjs;
    public GameObject txtObjPlayerReady;



    public int totalChars = 1; //change as more added
    private int currentChar;
    //public Sprite[] charSelectSprites;

    public GameObject eventSystemObj;
    public Button focusSelectedButton;


    //UI Objects
    public TextMeshProUGUI nameTextObj;
    public Image displayImageObj;
    public TextMeshProUGUI basicAbilityDescriptionObj;
    public TextMeshProUGUI movementAbilityDescriptionObj;
    public TextMeshProUGUI ultimateAbilityDescriptionObj;
    public TextMeshProUGUI passiveAbilityDescriptionObj;

    // Start is called before the first frame update
    void Start()
    {
        focusSelectedButton = GetComponentInParent<PlayerJoinScreenMaster>().btnBack;
        eventSystemObj = GameObject.FindGameObjectWithTag("EventSystem");
        totalChars = characterStats.Length;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput(playerNumber);

        

    }

    private void PlayerInput(int pNum)
    {

        //myCharSelectImage.sprite = charSelectSprites[currentChar];
        SetUIFromStats(pNum, currentChar);

        switch (myState)
        {
            case SelectState.WaitForJoin:

                if (Input.GetButtonDown(xButtonArray[pNum]))
                {
                    panelCharSelect.SetActive(true);
                    panelPressToJoin.SetActive(false);
                    myState = SelectState.Selecting;
                }
                break;
            case SelectState.Selecting:
                eventSystemObj.GetComponent<EventSystem>().SetSelectedGameObject(null);

                if (Input.GetButtonDown(leftBumperArray[pNum]))
                {
                    if (currentChar <= 0)
                    {
                        currentChar = totalChars - 1;

                    }
                    else
                    {
                        currentChar--;
                    }
                }

                if (Input.GetButtonDown(rightBumperArray[pNum]))
                {
                    if (currentChar>= totalChars - 1)
                    {
                        currentChar = 0;
                    }
                    else
                    {
                        currentChar++;
                    }
                }

                if (Input.GetButtonDown(aButtonArray[pNum]))
                {

                    CharSelect(playerNumber, currentChar);
                    myState= SelectState.Selected;

                }

                if (Input.GetButtonDown(bButtonArray[pNum]))
                {
                    Unjoin(pNum);
                    myState= SelectState.WaitForJoin;

                }

                break;
            case SelectState.Selected:

                if (Input.GetButtonDown(bButtonArray[pNum]))
                {
                    DeselectProcesses(pNum);
                    myState= SelectState.Selecting;

                }
                break;

        }

        #region OLD CODE
        /*
        //YOU SHOULD USE A ENUM FOR THIS
        if (selectingChar[pNum] == false)
        {
            if (Input.GetButtonDown(xButtonArray[pNum]))
            {
                panelCharSelect.SetActive(true);
                panelPressToJoin.SetActive(false);
                selectingChar[pNum] = true;
            }

        }
        else if (charSelected[pNum] == false)
        {
            eventSystemObj.GetComponent<EventSystem>().SetSelectedGameObject(null);

            if (Input.GetButtonDown(leftBumperArray[pNum]))
            {
                if (currentChar[pNum] <= 0)
                {
                    currentChar[pNum] = totalChars;

                }
                else
                {
                    currentChar[pNum]--;
                }
                print("Player " + pNum + ": Pressed LB");
            }

            if (Input.GetButtonDown(rightBumperArray[pNum]))
            {
                if (currentChar[pNum] >= totalChars)
                {
                    currentChar[pNum] = 0;
                }
                else
                {
                    currentChar[pNum]++;
                }
                print("Player " + pNum + ": Pressed LB");
            }

            if (Input.GetButtonDown(aButtonArray[pNum]))
            {
                CharSelect(playerNumber, currentChar[pNum]);
            }

            if (Input.GetButtonDown(bButtonArray[pNum]))
            {
                print("P" + pNum + ": Pressed B to Unjoin");
                Unjoin(pNum);
            }

        }
        else if (charSelected[pNum])
        {
            if (Input.GetButtonDown(bButtonArray[pNum]))
            {
                print("P" + pNum + ": Pressed B to Deselect");

                DeselectProcesses(pNum);
            }

        }
        */
        #endregion
    }

    private void CharSelect(int pNum, int charNum)
    {
        PlayerPrefs.SetInt("CharacterPlayer" + pNum, charNum);
        txtObjPlayerReady.SetActive(true);
        GetComponentInParent<PlayerJoinScreenMaster>().totalPlayers++;
    }

    private void Unjoin(int pNum)
    {
        panelCharSelect.SetActive(false);
        panelPressToJoin.SetActive(true);

        if (pNum == 0)
        {
            eventSystemObj.GetComponent<EventSystem>().SetSelectedGameObject(focusSelectedButton.gameObject);
        }
    }

    private void DeselectProcesses(int playerNum)
    {
        txtObjPlayerReady.SetActive(false);
        GetComponentInParent<PlayerJoinScreenMaster>().totalPlayers--;
    }

    void SetUIFromStats(int pNum, int myChar)
    {
        nameTextObj.text = characterStats[myChar].myName;
        basicAbilityDescriptionObj.text = characterStats[myChar].basicAbilityDescription;
        movementAbilityDescriptionObj.text = characterStats[myChar].movementAbilityDescription;
        ultimateAbilityDescriptionObj.text = characterStats[myChar].ultimateAbilityDescription;
        passiveAbilityDescriptionObj.text = characterStats[myChar].passiveAbilityDescription;
        displayImageObj.sprite = characterStats[myChar].displayImage;
    }
}
