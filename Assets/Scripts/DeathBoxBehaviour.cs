using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBoxBehaviour : MonoBehaviour
{
	public DamageZoneMaster master;
	private Vector3 startPos;
	private int dirMod;
    // Start is called before the first frame update
    void Start()
    {
		master = FindObjectOfType<DamageZoneMaster>();
		startPos = transform.position;

		//if (transform.position.x > 0)
		//{
		//	dirMod = 1;
		//	startPos = new Vector3(startPos.x + transform.localScale.x, startPos.y, startPos.z);
		//}
		//else if (transform.position.x < 0)
		//{
		//	dirMod = -1;
		//	startPos = new Vector3(startPos.x - transform.localScale.x, startPos.y, startPos.z);


		//}
		//else if (transform.position.y > 0)
		//{
		//	dirMod = 1;
		//	startPos = new Vector3(startPos.x, startPos.y + transform.localScale.y, startPos.z);

		//}
		//else if (transform.position.y < 0)
		//{
		//	dirMod = -1;
		//	startPos = new Vector3(startPos.x, startPos.y - transform.localScale.y, startPos.z);

		//}
	}

    // Update is called once per frame
    void Update()
    {


		if (master.roundTimer / master.duration > 0.5f)
		{
			transform.localPosition = Vector3.Slerp(master.gameObject.transform.position, startPos , master.roundTimer / master.duration);

		}
    }

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.GetComponent<CharacterCommon>())
		{
			Vector2 bounceDir = (Vector2)transform.position - (Vector2)other.transform.position;
			other.gameObject.GetComponent<Rigidbody2D>().AddForce(bounceDir * 5000); //this aint work
		}
	}
}
