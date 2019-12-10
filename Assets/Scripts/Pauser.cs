using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Pauser : MonoBehaviour
{


	private bool paused = false;
	public GameObject PauseScreen;

	public Button firstSelected;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void Paused() //note unpausing causes ability use if pressed with A button
	{
		paused = !paused;

		if (paused)
		{
			Time.timeScale = 0f;
			PauseScreen.SetActive(true);
			FindObjectOfType<EventSystem>().SetSelectedGameObject(firstSelected.gameObject);
		}
		else
		{
			Time.timeScale = 1f;
			PauseScreen.SetActive(false);
		}
	}

	public void QuitToMenu()
	{
		Time.timeScale = 1f;
		PauseScreen.SetActive(false);
		SceneManager.LoadScene("Menu");
	}
}
