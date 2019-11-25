﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterCommon : MonoBehaviour
{
    public CharacterStats characterStats;
    public CharacterClass myClass = new CharacterClass();

    //public Slider sldHealth;
    //public Slider sldBrinkHealth;
    public Slider sldDeathChance;

    private bool canUpdateHealth = true;

	public TextMeshProUGUI pNumIndic;

	private float invinsDur = 1;
	private bool isInvincible;

	public SpriteRenderer mySpriteRend;
	public Color flashCol = Color.red;


	private Image health;
	private Image brinkHealth;
	private Image redHeart;
	private Image purpleHeart;


	//AUDIO
	private AudioSource audio;
	public AudioClip[] acHurtAudios;
    // Start is called before the first frame update
    void Start()
    {
		audio = GetComponent<AudioSource>();
		myClass.myHealth = characterStats.health;
		pNumIndic.text = "P" +  (gameObject.GetComponent<IsometricPlayerMovementController>().playerNumber + 1);
		//FindObjectOfType<InGameUIManager>().SetCommonUI(gameObject.GetComponent<IsometricPlayerMovementController>().playerNumber, dodgeCooldown, health, brinkHealth, redHeart, purpleHeart);

		PlayerHudManager phm = FindObjectOfType<InGameUIManager>().playerHudImages[gameObject.GetComponent<IsometricPlayerMovementController>().playerNumber].GetComponent<PlayerHudManager>();
		health = phm.health;
		brinkHealth = phm.brinkHealth;
		redHeart = phm.redHeart;
		purpleHeart = phm.purpleHeart;
	}

    // Update is called once per frame
    void Update()
    {
        UISetter();

		if (isInvincible)
		{
			FlashEffect(flashCol);
		}
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
                }

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


				int rndChance = Random.Range(0, 100);

				//sldDeathChance.gameObject.SetActive(true);
				sldDeathChance.value = rndChance / 100f;
				//FindObjectOfType<CameraController>().BrinkZoom(transform);
				StartCoroutine(disbableUIDelay(sldDeathChance.gameObject));
				if (rndChance >= Mathf.Abs(myClass.myHealth))
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

		Destroy(gameObject, 1f);
		FindObjectOfType<CameraController>().PlayerDeath(transform);
    }

    private IEnumerator disbableUIDelay(GameObject obj)
    {
        obj.SetActive(true);
        canUpdateHealth = false;
        yield return new WaitForSecondsRealtime(1f);
        canUpdateHealth = true;
        obj.SetActive(false);
        
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

}
