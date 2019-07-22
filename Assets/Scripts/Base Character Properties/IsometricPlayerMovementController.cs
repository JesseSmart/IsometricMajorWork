using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricPlayerMovementController : MonoBehaviour
{
    public int playerNumber; //this will need to be accessed on other scripts, such as stats and what not

    //controlls
    private string[] horizontalArray = new string[4] { "P1Horizontal", "P2Horizontal", "P3Horizontal", "P4Horizontal" };
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

	public float dodgeCooldown = 2;
	private float dodgeTimer;

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
        MovementFunction(playerNumber);
		Dodge(playerNumber);

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
        isoRenderer.SetDirection(movement);
        rbody.MovePosition(newPos);
    }

    public void Dodge(float pNum)
    {
		dodgeTimer -= Time.deltaTime;
		if (dodgeTimer <= 0)
		{
			if (Input.GetKeyDown("joystick " + (pNum + 1) + " button " + 1))
			{
				print("Dodge");
				dodgeTimer = dodgeCooldown;

			}
		}
    }




}
