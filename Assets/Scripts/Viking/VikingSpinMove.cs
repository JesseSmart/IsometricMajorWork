using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VikingSpinMove : MonoBehaviour
{
    public GameObject myOwner;
	public float spinSpeed;
    public float damage;


    private Rigidbody2D rbody;
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

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("PlayerCharacter") && other.gameObject != myOwner)
		{
            print("Spin Hit");
            other.gameObject.GetComponent<CharacterCommon>().TakeDamage(damage);
			//damage
			//knockback
		}
	}
}
