using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZenFreakController : MonoBehaviour
{
	public int pNum;
	public CharacterStats characterStats;
	private CharacterClass myClass = new CharacterClass();

	public GameObject stunPunchObj;
	public GameObject kickObj;
	public GameObject tempestObj;

	//UI
	public Slider sldBasicA;
	public Slider sldMovementA;
	public Slider sldUltA;


	private float stunOffset = 1;
	private float kickOffset = 1;

	Animator anim;

	// Start is called before the first frame update
	void Start()
	{
		pNum = gameObject.GetComponent<IsometricPlayerMovementController>().playerNumber;
		anim = GetComponentInChildren<Animator>();

		SetStats();
	}

	// Update is called once per frame
	void Update()
	{
		Cooldowns();
		Inputer();

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

			Vector3 myDir = new Vector3(gameObject.GetComponent<IsometricPlayerMovementController>().lastDir.x, gameObject.GetComponent<IsometricPlayerMovementController>().lastDir.y, 0);

			Vector3 fakePos = transform.position + (myDir.normalized * stunOffset);
			float myAngle = Mathf.Atan2(fakePos.y - transform.position.y, fakePos.x - transform.position.x) * 180 / Mathf.PI;
			Quaternion myRot = Quaternion.Euler(0, 0, myAngle - 90);

			GameObject stunZone = Instantiate(stunPunchObj, fakePos, myRot);
			stunZone.GetComponent<StunPunch>().myOwner = gameObject;
			GetComponent<IsometricPlayerMovementController>().DisableMove(stunZone.GetComponent<StunPunch>().canStunDuration);
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

			Vector3 fakePos = transform.position + (myDir.normalized * kickOffset);
			float myAngle = Mathf.Atan2(fakePos.y - transform.position.y, fakePos.x - transform.position.x) * 180 / Mathf.PI;
			Quaternion myRot = Quaternion.Euler(0, 0, myAngle - 90);

			GameObject kickZone = Instantiate(kickObj, fakePos, myRot);
			kickZone.GetComponent<KickZone>().myOwner = gameObject;
			GetComponent<IsometricPlayerMovementController>().DisableMove(kickZone.GetComponent<KickZone>().canKnockbackDuration);

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



			GameObject spinStick = Instantiate(tempestObj, transform.position, transform.rotation);
			spinStick.GetComponent<Tempest>().myOwner = gameObject;


			PlayClip("Ab Ult");
			float animDur = anim.GetCurrentAnimatorClipInfo(0).Length;
			int repeat = Mathf.RoundToInt(spinStick.GetComponent<Tempest>().zoneDuration / animDur);
			StartCoroutine(UltSpinRepeat(repeat, animDur, 0));
;

			myClass.ultATimer = myClass.ultACooldown;
		}
		else
		{
			//Recharging
		}
	}

	private void PassiveAbility()
	{

	}

	private void Inputer()
	{
		if (Input.GetKeyDown("joystick " + (pNum + 1) + " button " + 0) || Input.GetKeyDown(KeyCode.E))
		{
			BasicAbility();
		}

		if (Input.GetKeyDown("joystick " + (pNum + 1) + " button " + 2) || Input.GetKeyDown(KeyCode.F))
		{
			MovementAbility();
		}

		if (Input.GetKeyDown("joystick " + (pNum + 1) + " button " + 3) || Input.GetKeyDown(KeyCode.R))
		{
			UltimateAbility();
		}

	}

	void Cooldowns()
	{
		myClass.basicATimer -= Time.deltaTime;
		myClass.moveATimer -= Time.deltaTime;
		myClass.ultATimer -= Time.deltaTime;

		sldBasicA.value = 1 - (myClass.basicATimer / myClass.basicACooldown);
		sldMovementA.value = 1 - (myClass.moveATimer / myClass.moveACooldown);
		sldUltA.value = 1 - (myClass.ultATimer / myClass.ultACooldown);
	}

	void PlayClip(string clipName)
	{
		anim.Play(clipName);
		GetComponent<IsometricPlayerMovementController>().DisableAnims(anim.GetCurrentAnimatorClipInfo(0).Length);
	}

	IEnumerator UltSpinRepeat(int repAmount, float dur, int temp)
	{
		
		

		yield return new WaitForSeconds(dur);
		PlayClip("Ab Ult");
		if (temp < repAmount - 1)
		{
			temp++;
			StartCoroutine(UltSpinRepeat(repAmount, dur, temp));

		}
		else
		{

		}
	}
}
