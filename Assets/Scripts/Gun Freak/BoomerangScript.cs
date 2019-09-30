using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangScript : MonoBehaviour
{
    public Rigidbody2D rbody;

    public int initialForce;
    public int retForce;

	public float outwardFlightDuration;
	private bool isReturning;
    public GameObject myOwner;

	
    public float cantCatchDuration;
    private float cantCatchTimer;

	public float minDamage;
	public float maxDamage;
    // Start is called before the first frame update
    void Start()
    {
		cantCatchTimer = cantCatchDuration;
    }

    // Update is called once per frame
    void Update()
    {
		if (isReturning)
		{
			ReturnToOwner(myOwner);
		}
		else
		{
			outwardFlightDuration -= Time.deltaTime;
			if (outwardFlightDuration <= 0)
			{
				isReturning = true;
			}
		}
    }

    public void ReturnToOwner(GameObject player)
    {
		Vector2 dir = (Vector2)player.transform.position - (Vector2)transform.position;
		float dist = Vector2.Distance(transform.position, myOwner.transform.position);
		rbody.AddForce(dir.normalized * (retForce * (1 / Mathf.Sqrt(Mathf.Sqrt(dist)))));
		
		//rbody.velocity *= 0;
		//transform.position = Vector3.Slerp(transform.position, myOwner.transform.position, Time.deltaTime * 1/dist);
		CheckCatch(player);
    }

    public void Throw(Vector2 dir)
    {
		rbody.AddForce(dir.normalized * initialForce);
		//look into force modes like acceleration
	}

    private void CheckCatch(GameObject player)
    {
        cantCatchTimer -= Time.deltaTime;
        if (cantCatchTimer <= 0)
        {
            if (Vector2.Distance(player.transform.position, transform.position) < 1)
            {
                Destroy(gameObject);
            }
        }
    }

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("PlayerCharacter") && other.gameObject != myOwner)
		{
			//deal damage here
			print("Throw Damage");
			other.gameObject.GetComponent<CharacterCommon>().TakeDamage(minDamage, maxDamage);
			Destroy(gameObject);

		}
	}
}
