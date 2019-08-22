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

	//UI
	public Slider sldBasicA;
	public Slider sldMovementA;
	public Slider sldUltA;

	// Start is called before the first frame update
	void Start()
	{
		pNum = gameObject.GetComponent<IsometricPlayerMovementController>().playerNumber;

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
			GameObject boomerang = Instantiate(boomerangObj, transform.position, transform.rotation);
			boomerang.GetComponent<BoomerangScript>().myOwner = gameObject;

			boomerang.GetComponent<BoomerangScript>().Throw(gameObject.GetComponent<IsometricPlayerMovementController>().lastDir);

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
			GameObject shotBlast = Instantiate(shotgunObj, transform.position, transform.rotation);
			shotBlast.GetComponent<ShotgunBlast>().myOwner = gameObject;

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

}
