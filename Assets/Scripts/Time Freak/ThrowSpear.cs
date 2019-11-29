using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSpear : MonoBehaviour
{

	public float minDamage;
	public float maxDamage;

	public Rigidbody2D rbody;
	public int initialForce;

	public GameObject myOwner;
	public GameObject recievingSpearObj;
	public float maxFlightDuration;
	private float flightTimer;

	private Vector2 direction;

	private AudioSource audio;
	public AudioClip acHitTarget;
	public AudioClip acHitTerrain;
	public AudioClip acHitNothing;
	public AudioClip acThrow;
	public AudioClip acPowerUpSpear;

	public SpriteRenderer baseSpear; //implement this
	public SpriteRenderer powerSpear;

	// Start is called before the first frame update
	void Start()
    {
		audio = GetComponent<AudioSource>();
		rbody = GetComponent<Rigidbody2D>();
		flightTimer = maxFlightDuration;

		audio.PlayOneShot(acThrow);
	}

    // Update is called once per frame
    void Update()
    {
		flightTimer -= Time.deltaTime;
		if (flightTimer <= 0)
		{
			CreateReciever();
			//audio.PlayOneShot(acHitNothing);

			Destroy(gameObject);
		}
	}

	public void Throw(Vector2 dir)
	{
		//direction = dir;

		rbody.AddForce(dir.normalized * initialForce);


		//look into force modes like acceleration
	}

	private void OnTriggerEnter2D(Collider2D other)
	{

		if (other.gameObject.CompareTag("PlayerCharacter") && other.gameObject != myOwner)
		{
			//deal damage here
			print("Throw Damage");
			other.gameObject.GetComponent<CharacterCommon>().TakeDamage(minDamage, maxDamage, myOwner);
			FindObjectOfType<CameraController>().CamShake(0.1f, 0.1f);
			


			Destroy(gameObject);
			
		}
		else if (other.gameObject != myOwner && !other.gameObject.GetComponent<OrbiterPassive>() && !other.gameObject.GetComponent<ThrowSpear>() && !gameObject.GetComponent<RecieverSpear>())
		{
			//terrain
			print("TERRION");
			CreateReciever();

			Destroy(gameObject);
			
		}
	}

	void CreateReciever()
	{
		//Instantiate(recievingSpearObj, transform.position, transform.rotation);
		GameObject recieverObj = Instantiate(recievingSpearObj, transform.position, Quaternion.identity);
		recieverObj.GetComponent<RecieverSpear>().myOwner = myOwner;
	}

	public void Accelerated()
	{
		audio.PlayOneShot(acPowerUpSpear);
		//SWAP SPRITE
		baseSpear.enabled = false;
		powerSpear.enabled = true;
		rbody.velocity *= 2;
		print("hit the orbit");
	}
}
