using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InGameUIManager : MonoBehaviour
{
	public Image[] playerHudImages;
	public Sprite[] playerHudSprites;

	//make this script instantiate the individual huds first, then hide them (or they are disabled by default), then space them based of a centre point EGO and the screen width, then enable on player spawn

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void SetCharacterHud(int playerNum, int freakNum)
	{
		playerHudImages[playerNum].sprite = playerHudSprites[freakNum]; //make this line its own function

	}

	//public void SetUI(int playerNum, int freakNum, Image basic, Image move, Image ult)
	//{
	//	PlayerHudManager phm = playerHudImages[playerNum].GetComponent<PlayerHudManager>(); 
	//	playerHudImages[playerNum].sprite = playerHudSprites[freakNum]; //make this line its own function
	//	basic = phm.basicCooldown;
	//	move = phm.moveCooldown;
	//	ult = phm.ultCooldown;
		


	//}

	//public void SetCommonUI(int playerNum, Image dodge, Image health, Image brinkHealth, Image red, Image purple)
	//{
	//	PlayerHudManager phm = playerHudImages[playerNum].GetComponent<PlayerHudManager>();
	//	dodge = phm.dodgeCooldown;
	//	health = phm.health;
	//	brinkHealth = phm.brinkHealth;
	//	red = phm.redHeart;
	//	purple = phm.purpleHeart;
	//}



	//public void SetArray(Image[] array, int playerNum)
	//{

	//	array = playerHudImages[playerNum].GetComponent<PlayerHudManager>().barImages;

	//	switch (playerNum + 1)
	//	{
	//		case 1:
	//			array = playerHudImages[playerNum].GetComponent<PlayerHudManager>().barImages;
	//			break;
	//		case 2:
	//			array = playerHudImages[playerNum].GetComponent<PlayerHudManager>().barImages;
	//			break;
	//		case 3:
	//			array = playerHudImages[playerNum].GetComponent<PlayerHudManager>().barImages;
	//			break;
	//		case 4:
	//			array = playerHudImages[playerNum].GetComponent<PlayerHudManager>().barImages;
	//			break;
	//	}
	//}

}
