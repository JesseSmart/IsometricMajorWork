﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VikingController : MonoBehaviour
{
    public int pNum;
    public CharacterStats characterStats;
    private CharacterClass myClass = new CharacterClass();

    public GameObject spinMoveObj;
    public GameObject throwAxeObject;
	public GameObject ultimateAxeObj;

	private bool passiveHasRun;

	private bool canCast = true;
	private float castPauseDur = 0.4f;

	public GameObject imgRageAura;


	Animator anim;

	//AUDIO
	private AudioSource audio;
	// Ability Use
	public AudioClip acAxeSpin;
	//public AudioClip acAxeThrow;
	//public AudioClip acAxeUlt;
		//Cooldown Ready
	public AudioClip acBasicReady;
	public AudioClip acMovementReady;
	public AudioClip acUltimateReady;
	public AudioClip acBrinkYell;
			//Cooldown Ready Audio Play Bools
	private bool basicAudPlayable = false;
	private bool movementAudPlayable = false;
	private bool ultimateAudPlayable = false;

	private Image basicCooldown;
	private Image moveCooldown;
	private Image ultCooldown;


	// Start is called before the first frame update
	void Start()
    {
		pNum = gameObject.GetComponent<IsometricPlayerMovementController>().playerNumber;
		anim = GetComponentInChildren<Animator>();
		audio = GetComponent<AudioSource>();

		//FindObjectOfType<InGameUIManager>().SetUI(pNum, 0, basicCooldown, moveCooldown, ultCooldown); //change that number to fit character
		FindObjectOfType<InGameUIManager>().SetCharacterHud(pNum, 0);
		PlayerHudManager phm = FindObjectOfType<InGameUIManager>().playerHudImages[pNum].GetComponent<PlayerHudManager>();
		basicCooldown = phm.basicCooldown;
		moveCooldown = phm.moveCooldown;
		ultCooldown = phm.ultCooldown;
		SetStats();
    }

    // Update is called once per frame
    void Update()
    {
        Cooldowns();
        Inputer();
		if (!passiveHasRun)
		{
			PassiveAbility();
		}
    }

    private void SetStats()
    {
        //myClass.myHealth = characterStats.health;
        myClass.basicACooldown = characterStats.basicAbilityCooldown;
        myClass.moveACooldown = characterStats.movementAbilityCooldown;
        myClass.ultACooldown = characterStats.ultimateAbilityCooldown;
        //myClass.dodgeCooldown = characterStats.dodgeCooldown;


        //myClass.basicATimer = characterStats.basicAbilityCooldown;
        //myClass.moveATimer = characterStats.movementAbilityCooldown;
        myClass.ultATimer = characterStats.ultimateAbilityCooldown;
        //myClass.dodgeTimer = characterStats.dodgeCooldown;
    }

    private void BasicAbility()
    {
        if (myClass.basicATimer <= 0)
        {
			PlayClip("Basic Ab");
			audio.PlayOneShot(acAxeSpin);
			print(pNum + " Player: DO BASIC");

            GameObject spinObj = Instantiate(spinMoveObj, transform.position, transform.rotation);
            spinObj.GetComponent<VikingSpinMove>().myOwner = gameObject;

			basicAudPlayable = true;
            myClass.basicATimer = myClass.basicACooldown;
        }
        else
        {
            print("Basic recharging");
        }
    }

    private void MovementAbility()
    {
        if (myClass.moveATimer <= 0)
        {
			//PlayClip("Move Ab");
			if (gameObject.GetComponent<IsometricPlayerMovementController>().lastDir.x > 0)
			{
				PlayClip("Move Ab W");

			}
			else
			{
				PlayClip("Move Ab E");

			}


			GameObject thrownAxe = Instantiate(throwAxeObject, transform.position, transform.rotation);
            thrownAxe.GetComponent<ThrowAxe>().myOwner = gameObject;
            
            thrownAxe.GetComponent<ThrowAxe>().Throw(gameObject.GetComponent<IsometricPlayerMovementController>().lastDir);
            print(gameObject.GetComponent<IsometricPlayerMovementController>().currentDir);

			movementAudPlayable = true;
            myClass.moveATimer = myClass.moveACooldown;
        }
        else
        {
            //Recharging
            print("Movement recharging");
        }
    }

    private void UltimateAbility()
    {
        if (myClass.ultATimer <= 0)
        {
			PlayClip("Ult Ab");


			GameObject ultAxe = Instantiate(ultimateAxeObj, transform.position, transform.rotation);
			ultAxe.GetComponent<VikingUltimateSpin>().myOwner = gameObject;

			ultimateAudPlayable = true;
			myClass.ultATimer = myClass.ultACooldown;
        }
        else
        {
            //Recharging
        }
    }

    private void PassiveAbility()
    {
		if (GetComponent<CharacterCommon>().myClass.myHealth < 0)
		{
			GetComponent<IsometricPlayerMovementController>().currentSpeed *= 2;
			audio.PlayOneShot(acBrinkYell);
			imgRageAura.SetActive(true);
			passiveHasRun = true;
		}
    }

    private void Inputer()
    {
		if (canCast)
		{
			if (Input.GetKeyDown("joystick " + (pNum + 1) + " button " + 0))
			{
				BasicAbility();
				StartCoroutine(castDelay(castPauseDur));
			}

			if (Input.GetKeyDown("joystick " + (pNum + 1) + " button " + 2))
			{
				MovementAbility();
				StartCoroutine(castDelay(castPauseDur));

			}

			if (Input.GetKeyDown("joystick " + (pNum + 1) + " button " + 3))
			{
				UltimateAbility();
				StartCoroutine(castDelay(castPauseDur));

			}
		}

    }

    void Cooldowns()
    {
        myClass.basicATimer -= Time.deltaTime;
        myClass.moveATimer -= Time.deltaTime;
        myClass.ultATimer -= Time.deltaTime;

        //sldBasicA.value = 1 - (myClass.basicATimer / myClass.basicACooldown);
        //sldMovementA.value = 1 -(myClass.moveATimer / myClass.moveACooldown);
        //sldUltA.value = 1 - (myClass.ultATimer / myClass.ultACooldown);

		basicCooldown.fillAmount = 1 - (myClass.basicATimer / myClass.basicACooldown);
		moveCooldown.fillAmount = 1 - (myClass.moveATimer / myClass.moveACooldown);
		ultCooldown.fillAmount = 1 - (myClass.ultATimer / myClass.ultACooldown);

		

		if (myClass.basicATimer <= 0 && basicAudPlayable)
		{
			audio.PlayOneShot(acBasicReady);
			basicAudPlayable = false;
		}

		if (myClass.moveATimer <= 0 && movementAudPlayable)
		{
			audio.PlayOneShot(acMovementReady);
			movementAudPlayable = false;
		}

		if (myClass.ultATimer <= 0 && ultimateAudPlayable)
		{
			audio.PlayOneShot(acUltimateReady);
			ultimateAudPlayable = false;
		}
	}



	void PlayClip(string clipName)
	{
		anim.Play(clipName);
		GetComponent<IsometricPlayerMovementController>().DisableAnims(anim.GetCurrentAnimatorClipInfo(0).Length);
	}

	IEnumerator castDelay(float dur)
	{
		canCast = false;
		yield return new WaitForSeconds(dur);
		canCast = true;
	}

	void UILinker()
	{
		
	}
}
