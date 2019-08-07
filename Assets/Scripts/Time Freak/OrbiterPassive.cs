using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbiterPassive : MonoBehaviour
{
	public GameObject myOwner;
	public float orbitSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		//transform.LookAt(myOwner.transform.position);
		transform.RotateAround(myOwner.transform.position, Vector3.forward, Time.deltaTime * orbitSpeed);
    }

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponent<ThrowSpear>())
		{
			Rigidbody2D otherRB = other.GetComponent<ThrowSpear>().rbody;
			otherRB.velocity *= 2;
			print("hit the orbit");
		}
	}
}
