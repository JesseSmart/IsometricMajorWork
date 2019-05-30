using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricPlayerMovementController : MonoBehaviour
{

    public float baseMovementSpeed = 1f;

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

    }

    void FixedUpdate()
    {
        Vector2 currentPos = rbody.position;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector = Vector2.ClampMagnitude(inputVector, 1); //1 might have to be baseMovementSpeed instead. clamp prevents diagonal movement being faster
        Vector2 movement = inputVector * baseMovementSpeed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        //render
        rbody.MovePosition(newPos);
    }
}
