using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VikingUltimateSpin : MonoBehaviour
{

	public GameObject myOwner;
	private Rigidbody2D rbody;

	[Header("Stats")]
	public float spinSpeed;
	public int axeRange;

	private float rotationleft = 360;

	private LineRenderer lineR;


	// Start is called before the first frame update
	void Start()
    {
		transform.position = new Vector3(myOwner.transform.position.x, myOwner.transform.position.y + axeRange, myOwner.transform.position.z);
		lineR = GetComponent<LineRenderer>();

	}

	// Update is called once per frame
	void Update()
    {

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

		transform.RotateAround(myOwner.transform.position, Vector3.forward, -rotation);

		lineR.SetPosition(0, transform.position);
		lineR.SetPosition(1, myOwner.transform.position);

		ChainRayCollision();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("PlayerCharacter"))
		{
			if (collision.gameObject != myOwner.gameObject)
			{
				print("Ult Axe Hit");
				//damage
				//knockback
			}
		}
	}
	void ChainRayCollision() //only run once per object/character (like trigger entre)
	{
		RaycastHit2D hit = Physics2D.Raycast(transform.position, myOwner.transform.position);
		if (hit.collider.gameObject != null) //error
		{
			GameObject hitObj = hit.collider.gameObject;

			if (hitObj.CompareTag("PlayerCharacter"))
			{
				if (hitObj != myOwner.gameObject)
				{
					print("CHAIN HIT");
					//damage
					//knockback
				}
			}
		}
	}
}