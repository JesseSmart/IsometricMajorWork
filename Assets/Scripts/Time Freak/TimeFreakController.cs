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

	private bool canCast = true;
	private float castPauseDur = 0.4f;

	private Rigidbody2D rbody;

	//UI
	public Slider sldBasicA;
	public Slider sldMovementA;
	public Slider sldUltA;

	private Image basicCooldown;
	private Image moveCooldown;
	private Image ultCooldown;

	Animator anim;

	// Start is called before the first frame update
	void Start()
	{
		pNum = gameObject.GetComponent<IsometricPlayerMovementController>().playerNumber;
		anim = GetComponentInChildren<Animator>();
		rbody = GetComponent<Rigidbody2D>();

		FindObjectOfType<InGameUIManager>().SetCharacterHud(pNum, 1); //number is freak type
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
			float myAngle = Mathf.Atan2(fakePos.y - transform.position.y, fakePos.x - transform.position.x) * 180 / Mathf.PI;
			Quaternion myRot = Quaternion.Euler(0, 0, myAngle - 90);

			// YOU CAN DO THIS (Vector3)gameObject.GetComponent<IsometricPlayerMovementController>().lastDir;
			GameObject thrownSpear = Instantiate(throwSpearObj, fakePos, myRot);
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
			Vector3 TelePosResult = transform.position;
			PlayClip("Tele In");
			Vector3 tempPos = transform.position;

			//gameObject.GetComponent<CharacterCommon>().flashCol = Color.black;
			//gameObject.GetComponent<CharacterCommon>().RunInvins(invinsDur);



			print("Dodge");
			Vector3 dodgeResult = Vector3.zero;
			Vector3 tempVec = new Vector3(gameObject.GetComponent<IsometricPlayerMovementController>().lastDir.x, gameObject.GetComponent<IsometricPlayerMovementController>().lastDir.y, 0);

			
			dodgeResult = transform.position + (tempVec.normalized * teleRange);

			StartCoroutine(TeleTo(0.55f, dodgeResult));

			//*///
			#region Teleportation Raycasting
			////Raycast
			//RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, gameObject.GetComponent<IsometricPlayerMovementController>().lastDir, teleRange);
			//bool teleComplete = false;
			//for (int i = 0; i < hits.Length; i++)
			//{
			//	if (!teleComplete)
			//	{
			//		print(hits[i].collider.gameObject.name);
			//		if (!hits[i].collider.gameObject.CompareTag("PlayerCharacter"))
			//		{
			//			print("Interupted tele");

			//			//transform.position = hits[i].transform.position;
			//			TelePosResult = hits[i].transform.position;
			//			teleComplete = true;
			//		}
			//	}

			//}

			////if (hits.Length == 0 || !teleComplete) //can just be !telecomplete
			//if (!teleComplete) //can just be !telecomplete
			//{
			//	print("No hit tele");

			//	Vector3 tempVec = new Vector3(gameObject.GetComponent<IsometricPlayerMovementController>().lastDir.x, gameObject.GetComponent<IsometricPlayerMovementController>().lastDir.y, 0);
			//	//transform.position = transform.position + (tempVec.normalized * teleRange); 
			//	TelePosResult = transform.position + (tempVec.normalized * teleRange); 
			//	teleComplete = true;
			//}

			//StartCoroutine(TeleDelay(0.55f, TelePosResult));
			#endregion
			/////*
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
			for (int i = 0; i < 8; i++)
			{
				Vector3 myDir = new Vector3(gameObject.GetComponent<IsometricPlayerMovementController>().lastDir.x, gameObject.GetComponent<IsometricPlayerMovementController>().lastDir.y, 0);

				//Vector3 fakePos = transform.position + (myDir.normalized * spearSpawnOffset);
				//float myAngle = Mathf.Atan2(fakePos.y - transform.position.y, fakePos.x - transform.position.x) * 180 / Mathf.PI;
				Quaternion myRot = Quaternion.Euler(0, 0, 45 * i);

				// YOU CAN DO THIS (Vector3)gameObject.GetComponent<IsometricPlayerMovementController>().lastDir;
				GameObject thrownSpear = Instantiate(throwSpearObj, transform.position, myRot);
				thrownSpear.GetComponent<ThrowSpear>().myOwner = gameObject;
				thrownSpear.GetComponent<ThrowSpear>().Throw(thrownSpear.transform.up);
			}



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
		if (canCast)
		{
			if (Input.GetKeyDown("joystick " + (pNum + 1) + " button " + 0) || Input.GetKeyDown(KeyCode.E))
			{
				BasicAbility();
				StartCoroutine(castDelay(castPauseDur));
			}

			if (Input.GetKeyDown("joystick " + (pNum + 1) + " button " + 2) || Input.GetKeyDown(KeyCode.F))
			{
				MovementAbility();
				StartCoroutine(castDelay(castPauseDur));

			}

			if (Input.GetKeyDown("joystick " + (pNum + 1) + " button " + 3) || Input.GetKeyDown(KeyCode.R))
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

		sldBasicA.value = 1 - (myClass.basicATimer / myClass.basicACooldown);
		sldMovementA.value = 1 - (myClass.moveATimer / myClass.moveACooldown);
		sldUltA.value = 1 - (myClass.ultATimer / myClass.ultACooldown);

		basicCooldown.fillAmount = 1 - (myClass.basicATimer / myClass.basicACooldown);
		moveCooldown.fillAmount = 1 - (myClass.moveATimer / myClass.moveACooldown);
		ultCooldown.fillAmount = 1 - (myClass.ultATimer / myClass.ultACooldown);
	}

	IEnumerator TeleDelay(float dur, Vector3 telePos)
	{
		yield return new WaitForSeconds(dur);
		transform.position = telePos;
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

	IEnumerator TeleTo(float dur, Vector3 pos)
	{
		rbody.constraints = RigidbodyConstraints2D.FreezeAll;
		yield return new WaitForSeconds(dur);
				
		rbody.constraints = RigidbodyConstraints2D.None;
		rbody.constraints = RigidbodyConstraints2D.FreezeRotation;
		float dist = Vector3.Distance(transform.position, pos);
		float timer = 0.2f;
		gameObject.GetComponent<CharacterCommon>().flashCol = Color.black;
		gameObject.GetComponent<CharacterCommon>().RunInvins(timer);
		while (dist > 0.5 && timer > 0)
		{
			timer -= Time.deltaTime;
			dist = Vector3.Distance(transform.position, pos);
			transform.position = Vector2.MoveTowards(transform.position, pos, Time.deltaTime * 100);
			yield return new WaitForEndOfFrame();
		}
		yield return null;

	}

}
