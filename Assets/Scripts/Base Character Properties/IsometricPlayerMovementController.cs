﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricPlayerMovementController : MonoBehaviour
{
    public int playerNumber; //this will need to be accessed on other scripts, such as stats and what not

    //controlls
    private string[] horizontalArray = new string[4] { "P1Horizontal", "P2Horizontal", "P3Horizontal", "P4Horizontal" }; //CHANGE BACK TO P1 on start of first item in array to make work for controller ***
    private string[] verticalArray = new string[4] { "P1Vertical", "P2Vertical", "P3Vertical", "P4Vertical" };
    private string[] basicAbilityArray = new string[4] { "P1AButton", "P2AButton", "P3AButton", "P4AButton" }; //when making inheritance and stats and all, move these to them
    private string[] movementAbilityArray = new string[4] { "P1BButton", "P2BButton", "P3BButton", "P4BButton" };


    public float baseMovementSpeed = 1f;
    private float currentSpeed;

    public GameObject renderObj;
    IsometricCharacterRenderer isoRenderer;
    Rigidbody2D rbody;

    public Vector2 currentDir;
	public Vector2 lastDir;

	public bool canInput = true;
	private bool canAnimate = true;


	private float dodgeCooldown = 1;
	private float dodgeTimer;
	private float dodgeRange = 3;

	private float frictionMod = 5f;
	private Vector2 fricVel;


    // Start is called before the first frame update
    //void Start()
    //{
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //}

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        currentSpeed = baseMovementSpeed;
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
		
    }

    void FixedUpdate()
    {
		if (canInput)
		{

			MovementFunction(playerNumber);
			Dodge(playerNumber);
		}
		ForceFriction();

    }

    private void MovementFunction(int playerNum)
    {
        Vector2 currentPos = rbody.position;
        float horizontalInput = Input.GetAxis(horizontalArray[playerNum]);
        float verticalInput = Input.GetAxis(verticalArray[playerNum]);

        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        currentDir = inputVector;

		if (currentDir != Vector2.zero)
		{
			lastDir = currentDir;
		}


        inputVector = Vector2.ClampMagnitude(inputVector, 1); //1 might have to be baseMovementSpeed instead. clamp prevents diagonal movement being faster
        Vector2 movement = inputVector * currentSpeed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;

		if (canAnimate)
		{
			isoRenderer.SetDirection(movement);

		}
		if (newPos != currentPos)
		{
			rbody.MovePosition(newPos);
		}
    }

    public void Dodge(float pNum)
    {
		dodgeTimer -= Time.deltaTime;
		if (dodgeTimer <= 0)
		{
			if (Input.GetKeyDown("joystick " + (pNum + 1) + " button " + 1))
			{
				print("Dodge");
				//Raycast
				Vector3 dodgeResult = Vector3.zero;
				Vector3 tempVec = new Vector3(gameObject.GetComponent<IsometricPlayerMovementController>().lastDir.x, gameObject.GetComponent<IsometricPlayerMovementController>().lastDir.y, 0);
				RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, lastDir, dodgeRange);
				bool teleComplete = false;

				if (!teleComplete && hits.Length > 1)
				{
					print(hits[1].collider.gameObject.name);
					if (!hits[1].collider.gameObject.GetComponent<CharacterCommon>())
					{
						print("Interupted tele");

						dodgeResult = hits[1].transform.position;
						teleComplete = true;
					}
				}

				

				if (!teleComplete)
				{
					//print("No hit tele");

					dodgeResult = transform.position + (tempVec.normalized * dodgeRange);
					teleComplete = true;
				}
				//print(transform.position - dodgeResult);
				//transform.position = dodgeResult;
				StartCoroutine(DodgeTo(dodgeResult));

				dodgeTimer = dodgeCooldown;

			}
		}
    }

	void ForceFriction()
	{
		rbody.velocity = Vector2.SmoothDamp(rbody.velocity, Vector2.zero, ref fricVel, Time.deltaTime * frictionMod);
	}


	public void DisableAnims(float duration)
	{
		StartCoroutine(PauseAnims(duration));
	}

	IEnumerator PauseAnims(float dur)
	{
		canAnimate = false;
		yield return new WaitForSeconds(dur);
		canAnimate = true;
	}

	public void DisableMove(float duration)
	{
		StartCoroutine(PauseMove(duration));
	}

	IEnumerator PauseMove(float dur)
	{
		canInput = false;
		yield return new WaitForSeconds(dur);
		canInput = true;
	}

	IEnumerator DodgeTo(Vector3 pos)
	{
		float dist = Vector3.Distance(transform.position, pos);
		canInput = false;
		while (dist > 0.5)
		{
			dist = Vector3.Distance(transform.position, pos);
			transform.position = Vector2.MoveTowards(transform.position, pos, Time.deltaTime * 10);
			yield return new WaitForEndOfFrame();
		}
		canInput = true;
		yield return null;
		
	}
}
