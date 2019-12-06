using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunFreakController : MonoBehaviour
{
	public int pNum;
	public CharacterStats characterStats;
	private CharacterClass myClass = new CharacterClass();

	public GameObject boomerangObj;
	public GameObject shotgunObj;
	public GameObject minigunObj;

	private bool canCast = true;
	private float castPauseDur = 0.4f;


	private float shotgunOffset = 1;

	Animator anim;

	//AUDIO
	private AudioSource audio;
	// Ability Use
	//public AudioClip acAxeSpin;
	//public AudioClip acAxeThrow;
	//public AudioClip acAxeUlt;
	//Cooldown Ready
	public AudioClip acBasicReady;
	public AudioClip acMovementReady;
	public AudioClip acUltimateReady;
	public AudioClip acPassiveKill;
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

		FindObjectOfType<InGameUIManager>().SetCharacterHud(pNum, 2); //number is freak type
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
		//print(gameObject.GetComponent<IsometricPlayerMovementController>().lastDir);
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
			GameObject boomerang = Instantiate(boomerangObj, transform.position, transform.rotation);
			boomerang.GetComponent<BoomerangScript>().myOwner = gameObject;

			boomerang.GetComponent<BoomerangScript>().Throw(gameObject.GetComponent<IsometricPlayerMovementController>().lastDir);

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
			Vector3 myDir = new Vector3(gameObject.GetComponent<IsometricPlayerMovementController>().lastDir.x, gameObject.GetComponent<IsometricPlayerMovementController>().lastDir.y, 0);

			Vector3 fakePos = transform.position + (myDir.normalized * shotgunOffset);
			float myAngle = Mathf.Atan2(fakePos.y - transform.position.y, fakePos.x - transform.position.x) * 180 / Mathf.PI;
			Quaternion myRot = Quaternion.Euler(0, 0, myAngle - 90);

			GameObject shotBlast = Instantiate(shotgunObj, fakePos, myRot);
			shotBlast.GetComponent<ShotgunBlast>().myOwner = gameObject;

			if (myDir.x >= 0)
			{
				if (myDir.y >= 0.5)
				{
					print("UP");
					PlayClip("Move Ab N");
				}
				else if (myDir.y < -0.5)
				{
					print("Down");
					PlayClip("Move Ab S");

				}
				else
				{
					print("RIGHT");
					PlayClip("Move Ab E");

				}
			}
			else
			{
				if (myDir.y >= 0.5)
				{
					print("UP");
					PlayClip("Move Ab N");

				}
				else if (myDir.y < -0.5)
				{
					print("Down");
					PlayClip("Move Ab S");

				}
				else
				{
					print("LEFT");
					PlayClip("Move Ab W");

				}
			}

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
			GameObject minigun = Instantiate(minigunObj, transform.position, transform.rotation);
			minigun.GetComponent<MinigunShooter>().myOwner = gameObject;
			PlayClip("Ult Ab");
			ultimateAudPlayable = true;
			myClass.ultATimer = myClass.ultACooldown;
		}
		else
		{
			//Recharging
		}
	}

	public void PassiveAbility()
	{
		myClass.basicATimer = 0;
		myClass.moveATimer = 0;
		audio.PlayOneShot(acPassiveKill);
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
}
