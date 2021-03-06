﻿using System.Collections;
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

		GetComponent<SpriteRenderer>().enabled = false;

		StartCoroutine(ReDisplay());
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


		if (master.roundTimer / master.duration > 0.7f)
		{
			transform.localPosition = Vector3.Slerp(master.gameObject.transform.position, startPos , master.roundTimer / master.duration);

		}
    }



	private void OnCollisionStay2D(Collision2D other) //make collision 2d
	{
		if (other.gameObject.GetComponent<CharacterCommon>())
		{
			print("is char");
			Vector2 bounceDir = ((Vector2)transform.position - (Vector2)other.transform.position) / Vector2.Distance((Vector2)transform.position, (Vector2)other.transform.position);
			other.gameObject.GetComponent<Rigidbody2D>().AddForce(bounceDir.normalized * -200); //this aint work
			StartCoroutine(PauseControl(other.gameObject, 0.2f)); //MIGHT CAUSE ERRORS
			other.gameObject.GetComponent<CharacterCommon>().TakeDamage(5, 10, null);
			
		}
	}

	IEnumerator PauseControl(GameObject effected, float dur)
	{
		effected.GetComponent<IsometricPlayerMovementController>().enabled = false;
		yield return new WaitForSeconds(dur);
		effected.GetComponent<IsometricPlayerMovementController>().enabled = true;

	}

	IEnumerator ReDisplay()
	{
		yield return new WaitForSeconds(5f);
		GetComponent<SpriteRenderer>().enabled = true;

	}



}
