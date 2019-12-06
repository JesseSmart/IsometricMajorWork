using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterCommon : MonoBehaviour
{
	public CharacterStats characterStats;
	public CharacterClass myClass = new CharacterClass();

	private int playerNum;
	//public Slider sldHealth;
	//public Slider sldBrinkHealth;

	private bool canUpdateHealth = true;


	private float invinsDur = 1;
	private bool isInvincible;

	public SpriteRenderer mySpriteRend;
	public Color flashCol = Color.red;
	public GameObject DeathObj;

	private Image health;
	private Image brinkHealth;
	private Image redHeart;
	private Image purpleHeart;
	private Image brinkImg;
	private Slider brinkSld;

	private float conSldFloat;

	public Image pNumIdentifierImg;
	public Sprite[] playerNumberSprites;

	//AUDIO
	private AudioSource audio;
	public AudioClip[] acHurtAudios;
	// Start is called before the first frame update
	void Start()
	{
		audio = GetComponent<AudioSource>();
		myClass.myHealth = characterStats.health;
		pNumIdentifierImg.sprite = playerNumberSprites[gameObject.GetComponent<IsometricPlayerMovementController>().playerNumber];
		//FindObjectOfType<InGameUIManager>().SetCommonUI(gameObject.GetComponent<IsometricPlayerMovementController>().playerNumber, dodgeCooldown, health, brinkHealth, redHeart, purpleHeart);
		playerNum = gameObject.GetComponent<IsometricPlayerMovementController>().playerNumber;
		PlayerHudManager phm = FindObjectOfType<InGameUIManager>().playerHudImages[gameObject.GetComponent<IsometricPlayerMovementController>().playerNumber].GetComponent<PlayerHudManager>();
		health = phm.health;
		brinkHealth = phm.brinkHealth;
		redHeart = phm.redHeart;
		purpleHeart = phm.purpleHeart;
		brinkImg = phm.brinkImg;
		brinkSld = phm.brinkSld;
	}

	// Update is called once per frame
	void Update()
	{
		UISetter();

		if (isInvincible)
		{
			FlashEffect(flashCol);
		}

		PausePressCheck();
	}

	void UISetter()
	{
		if (myClass.myHealth > 0)
		{
			//sldHealth.value = myClass.myHealth / characterStats.health;

			health.fillAmount = myClass.myHealth / characterStats.health;

			//if (!sldHealth.gameObject.active || sldBrinkHealth.gameObject.active)
			//         {
			//             sldHealth.gameObject.SetActive(true);
			//             sldBrinkHealth.gameObject.SetActive(false);
			//         }

			if (!health.gameObject.active || brinkHealth.gameObject.active)
			{
				health.gameObject.SetActive(true);
				redHeart.gameObject.SetActive(true);
				brinkHealth.gameObject.SetActive(false);
				purpleHeart.gameObject.SetActive(false);
			}
		}
		else
		{
			if (canUpdateHealth)
			{
				brinkHealth.fillAmount = Mathf.Abs(myClass.myHealth / characterStats.health);

				if (health.gameObject.active || !brinkHealth.gameObject.active)
				{
					health.gameObject.SetActive(false);
					redHeart.gameObject.SetActive(false);
					brinkHealth.gameObject.SetActive(true);
					purpleHeart.gameObject.SetActive(true);
					brinkSld.gameObject.SetActive(true);
					brinkImg.enabled = true;
				}


				conSldFloat += Time.deltaTime * 2f;
				brinkSld.value = Mathf.PingPong(conSldFloat, 1);

				brinkImg.fillAmount = Mathf.PingPong(conSldFloat, 1);

				//sldBrinkHealth.value = Mathf.Abs(myClass.myHealth / characterStats.health);

				//if (sldHealth.gameObject.active || !sldBrinkHealth.gameObject.active)
				//{
				//	sldHealth.gameObject.SetActive(false);
				//	sldBrinkHealth.gameObject.SetActive(true);
				//}

			}


		}
	}

	public void TakeDamage(float minDamage, float maxDamage, GameObject dealerOwner)
	{
		float damage = Random.Range(minDamage, maxDamage);
		if (!isInvincible)
		{
			if (myClass.myHealth >= 0)
			{
				myClass.myHealth -= damage;
				HurtAudio();

			}
			else if (myClass.myHealth < 0)
			{


				//int rndChance = Random.Range(0, 100);

				//sldDeathChance.gameObject.SetActive(true);
				//sldDeathChance.value = rndChance / 100f;
				//FindObjectOfType<CameraController>().BrinkZoom(transform);
				StartCoroutine(disbableUIDelay(brinkSld.gameObject));
				if (brinkSld.value * 100 >= Mathf.Abs(myClass.myHealth))
				{
					//alive
					print("Alive");
					myClass.myHealth -= damage;
					HurtAudio();
				}
				else
				{
					//dead
					Death(dealerOwner);
					//Death Audio
				}

			}
			flashCol = Color.red;

			RunInvins(invinsDur);

		}



	}

	void Death(GameObject killer)
	{
		if (killer.GetComponent<GunFreakController>())
		{
			killer.GetComponent<GunFreakController>().PassiveAbility();
		}

		Instantiate(DeathObj, transform.position, transform.rotation);
		gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
		Destroy(gameObject, 0.1f);
		FindObjectOfType<CameraController>().PlayerDeath(transform);
	}

	private IEnumerator disbableUIDelay(GameObject obj)
	{
		//obj.SetActive(true);
		canUpdateHealth = false;
		yield return new WaitForSecondsRealtime(1f);
		canUpdateHealth = true;
		//obj.SetActive(false);

	}

	public void RunInvins(float dur)
	{
		StartCoroutine(InvinsibilityFrames(dur));

	}

	private IEnumerator InvinsibilityFrames(float dur)
	{
		float nowTime = Time.deltaTime;
		isInvincible = true;
		gameObject.layer = 8;
		yield return new WaitForSeconds(dur);
		isInvincible = false;
		mySpriteRend.color = Color.white;
		gameObject.layer = 9;
	}

	private void FlashEffect(Color flashColor)
	{
		//print("FLASH... ahahhhhhhhh");
		//print(Mathf.Abs(Mathf.Cos(Time.time * 3 * Mathf.PI)));
		mySpriteRend.color = Color.Lerp(Color.white, flashColor, Mathf.Abs(Mathf.Cos(Time.time * 5 * Mathf.PI)));
		//print(mySpriteRend);
	}

	void HurtAudio()
	{
		audio.PlayOneShot(acHurtAudios[Random.Range(0, acHurtAudios.Length)]);
	}

	public void PausePressCheck()
	{
		if (Input.GetKeyDown("joystick " + (playerNum + 1) + " button " + 7))
		{
			FindObjectOfType<Pauser>().Paused();
		}
	}

}
