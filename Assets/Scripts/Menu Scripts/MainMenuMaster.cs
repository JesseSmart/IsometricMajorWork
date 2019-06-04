using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MainMenuMaster : MonoBehaviour
{

    public GameObject modifierPanel;
    public GameObject playerJoinPanel;

    public GameObject eventSystemObj;

    public Button firstSelectedButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPressPlay()
    {
        modifierPanel.SetActive(true);
        gameObject.SetActive(false);
        eventSystemObj.GetComponent<EventSystem>().SetSelectedGameObject(firstSelectedButton.gameObject);
    }

    public void OnQuit()
    {
        Application.Quit();
    }

}
