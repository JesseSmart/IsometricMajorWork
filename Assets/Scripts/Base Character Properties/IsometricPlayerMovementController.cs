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

    private int basicAbilityKey = 0;
    public float baseMovementSpeed = 1f;
    private float currentSpeed;

    Rigidbody2D rbody;

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
    }

    void FixedUpdate()
    {
        MovementFunction(playerNumber);
        BasicTest(playerNumber);
        MovementAbilityTest(playerNumber);
    }

    private void MovementFunction(int playerNum)
    {
        Vector2 currentPos = rbody.position;
        float horizontalInput = Input.GetAxis(horizontalArray[playerNum]);
        float verticalInput = Input.GetAxis(verticalArray[playerNum]);

        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector = Vector2.ClampMagnitude(inputVector, 1); //1 might have to be baseMovementSpeed instead. clamp prevents diagonal movement being faster
        Vector2 movement = inputVector * currentSpeed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        //render
        rbody.MovePosition(newPos);
    }

    private void BasicTest(int playerNum)
    {
        //if (Input.GetButtonDown("joystick " + (playerNum + 1) + " button " + basicAbilityKey))
        if (Input.GetButtonDown(basicAbilityArray[playerNum]))
        {
            print("P" + playerNum + ": Pressed Basic Ability = " + basicAbilityKey);
            currentSpeed += 5;
        }

    }

    private void MovementAbilityTest(int playerNum)
    {

        if (Input.GetButtonDown(movementAbilityArray[playerNum]))
        {
            print("P" + playerNum + ": Pressed Movement Ability");
            currentSpeed -= 5;
        }
    }
}
