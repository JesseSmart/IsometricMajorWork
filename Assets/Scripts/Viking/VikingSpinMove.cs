using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VikingSpinMove : MonoBehaviour
{
    public GameObject myOwner;
    private Rigidbody2D rbody;

	public float spinSpeed;

	public float spinTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		transform.Rotate(0, 0, -1 * Time.deltaTime * spinSpeed, Space.World);
		spinTimer -= Time.deltaTime;
		if (spinTimer <= 0)
		{
			Destroy(gameObject);
		}

    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject != myOwner)
		{
			//damage
			//knockback
		}
	}
}
