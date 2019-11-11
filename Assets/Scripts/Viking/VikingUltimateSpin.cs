using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VikingUltimateSpin : MonoBehaviour
{
	public float minAxeDamage;
	public float maxAxeDamage;
	public float minChainDamage;
	public float maxChainDamage;
	public GameObject myOwner;
	private Rigidbody2D rbody;
	private GameObject[] alreadyHitObjs = new GameObject[3]; //USE LIST INSTEAD
	public float duration;
	[Header("Stats")]
	public float spinSpeed;
	public int axeRange;

	private float rotationleft = 360;

	private LineRenderer lineR;


	// Start is called before the first frame update
	void Start()
    {
		transform.position = new Vector3(myOwner.transform.position.x, myOwner.transform.position.y + axeRange, myOwner.transform.position.z);
		//lineR = GetComponent<LineRenderer>();
		transform.SetParent(myOwner.transform, true);
		transform.RotateAround(myOwner.transform.position, Vector3.forward, 180);
		Destroy(gameObject, duration);
	}

	// Update is called once per frame
	void Update()
    {
		//transform.Rotate(0, 0, -1 * Time.deltaTime * spinSpeed, Space.World);
		float rotation = spinSpeed * Time.deltaTime;

		transform.RotateAround(myOwner.transform.position, Vector3.forward, -rotation);
		/**
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
		**/

		//lineR.SetPosition(0, transform.position);
		//lineR.SetPosition(1, myOwner.transform.position);

		ChainRayCollision();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{

		if (other.gameObject.GetComponent<CharacterCommon>())
		{
			if (other.gameObject != myOwner.gameObject)
			{
				print("Ult Axe Hit");
				other.GetComponent<CharacterCommon>().TakeDamage(minAxeDamage, maxAxeDamage, myOwner);
				FindObjectOfType<CameraController>().FrameFreeze();

				//damage
				//knockback
			}
		}
	}
	void ChainRayCollision() //only run once per object/character (like trigger entre)
	{
		RaycastHit2D hit = Physics2D.Raycast(transform.position, myOwner.transform.position);
		if (hit.collider != null) //error
		{
			GameObject hitObj = hit.collider.gameObject;

			if (hitObj.CompareTag("PlayerCharacter"))
			{
				foreach (GameObject obj in alreadyHitObjs)
				{
					if (hitObj == obj)
					{
						return;
					}
				}

				if (hitObj != myOwner.gameObject)
				{
					print("CHAIN HIT");
					for (int i = 0; i < alreadyHitObjs.Length; i++)
					{
						if (alreadyHitObjs[i] == null)
						{
							alreadyHitObjs[i] = hitObj;
							//damage
							//knockback
							print("chaindmg");

							hitObj.gameObject.GetComponent<CharacterCommon>().TakeDamage(minChainDamage, maxChainDamage, myOwner);

							return;
						}
					}
					
				}
			}
		}
	}


}