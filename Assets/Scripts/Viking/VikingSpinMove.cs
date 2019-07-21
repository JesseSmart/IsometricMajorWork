using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VikingSpinMove : MonoBehaviour
{
    public GameObject myOwner;
    private Rigidbody2D rbody;

	public float spinSpeed;

	private float rotationleft = 360;
	

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		transform.position = myOwner.transform.position;
		//transform.Rotate(0, 0, -1 * Time.deltaTime * spinSpeed, Space.World);


		float rotation = spinSpeed * Time.deltaTime;
		if (rotationleft > rotation)
		{
			rotationleft -= rotation;
		}
		else
		{
			rotation = rotationleft;
			rotationleft = 0;
			Destroy(gameObject);

		}
		transform.Rotate(0, 0, -rotation);


    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("PlayerCharacter") && collision.gameObject != myOwner)
		{
			//damage
			//knockback
		}
	}
}
