using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VictoryScreenManager : MonoBehaviour
{

	public Image pNumImage;
	public Image charNumImage;

	public Sprite[] numSprites;
	public Sprite[] charSprites;

	float loadNextTimer = 8;
	private bool loaded = false;

	private AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
		int winNum = PlayerPrefs.GetInt("WinnerNum");
		int winChar = PlayerPrefs.GetInt("WinnerChar");

		pNumImage.sprite = numSprites[winNum];
		charNumImage.sprite = charSprites[winChar];

		audio = GetComponent<AudioSource>();
	}

    // Update is called once per frame
    void Update()
    {
		//loadNextTimer -= Time.deltaTime;
		if (!loaded && !audio.isPlaying)
		{
			loaded = true;
			Destroy(FindObjectOfType<MatchManager>().gameObject);
			SceneManager.LoadScene("Menu");
		}
    }
}
