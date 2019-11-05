using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreSlider : MonoBehaviour
{

	private Slider slider;
	public int sliderNum;
	public Text myText;

	private MatchManager matchManager;
    // Start is called before the first frame update
    void Start()
    {
		matchManager = FindObjectOfType<MatchManager>();
		slider = GetComponent<Slider>();
		float tr = matchManager.targetRounds;
		float scr = matchManager.playerScores[sliderNum];
		float val = scr / tr;
		print("bruh"+ val);
		print("pscore"+ matchManager.playerScores[sliderNum]);
		print("tgrnd"+ matchManager.targetRounds);
		slider.value = val;
		myText.text = "P" + (sliderNum + 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
