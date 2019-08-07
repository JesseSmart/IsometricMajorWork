using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimeFreakController : MonoBehaviour
{
	public int pNum;
	public CharacterStats characterStats;
	private CharacterClass myClass = new CharacterClass();

	public GameObject throwSpearObj;
	public GameObject teleDamageZoneObj;

	private float spearSpawnOffset = 1;
	public float teleRange;



	//UI
	public Slider sldBasicA;
	public Slider sldMovementA;
	public Slider sldUltA;

	// Start is called before the first frame update
	void Start()
	{
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

			Vector3 fakePos = transform.position + (myDir.normalized * spearSpawnOffset);
			// YOU CAN DO THIS (Vector3)gameObject.GetComponent<IsometricPlayerMovementController>().lastDir;
			GameObject thrownSpear = Instantiate(throwSpearObj, fakePos, transform.rotation);
			thrownSpear.GetComponent<ThrowSpear>().myOwner = gameObject;

			thrownSpear.GetComponent<ThrowSpear>().Throw(gameObject.GetComponent<IsometricPlayerMovementController>().lastDir);
			print(gameObject.GetComponent<IsometricPlayerMovementController>().currentDir);

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
			//FOR FUTRUE: Check collisions at desired pos (trig, multi raycast, square cast?) as well as raycastall. If collision with terrain (i.e unable to tele, use furthest raycastall)

			Vector3 tempPos = transform.position;
			#region Teleportation Raycasting
			//Raycast
			RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, gameObject.GetComponent<IsometricPlayerMovementController>().lastDir, teleRange);
			bool teleComplete = false;
			for (int i = 0; i < hits.Length; i++)
			{
				if (!teleComplete)
				{
					print(hits[i].collider.gameObject.name);
					if (!hits[i].collider.gameObject.CompareTag("PlayerCharacter"))
					{
						print("Interupted tele");

						transform.position = hits[i].transform.position;
						teleComplete = true;
					}
				}

			}

			//if (hits.Length == 0 || !teleComplete) //can just be !telecomplete
			if (!teleComplete) //can just be !telecomplete
			{
				print("No hit tele");

				Vector3 tempVec = new Vector3(gameObject.GetComponent<IsometricPlayerMovementController>().lastDir.x, gameObject.GetComponent<IsometricPlayerMovementController>().lastDir.y, 0);
				transform.position = transform.position + (tempVec.normalized * teleRange); 
				teleComplete = true;
			}
			#endregion

			GameObject teleDmgZone = Instantiate(teleDamageZoneObj, tempPos, transform.rotation);
			teleDmgZone.GetComponent<TeleDamageZone>().myOwner = gameObject;

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

		if (Input.GetKeyDown("joystick " + (pNum + 1) + " button " + 2))
		{
			MovementAbility();
		}

		if (Input.GetKeyDown("joystick " + (pNum + 1) + " button " + 3))
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
