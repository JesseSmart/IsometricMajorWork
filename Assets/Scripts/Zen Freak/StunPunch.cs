using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunPunch : MonoBehaviour
{
	public GameObject myOwner;
	public float minDamage;
	public float maxDamage;
	public float stunDuration;

	public float canStunDuration;
	private bool canStun;
    // Start is called before the first frame update
    void Start()
    {
		Destroy(gameObject, 2*stunDuration); //might need its own float
		canStun = true;
		StartCoroutine(CanStunTimer());
    }

    // Update is called once per frame
    void Update()
    {
		
    }

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.GetComponent<CharacterCommon>() && other.gameObject != myOwner && canStun)
		{
			StartCoroutine(ApplyStun(other.gameObject));
		}
	}

	IEnumerator ApplyStun(GameObject target)
	{
		target.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
		target.GetComponent<CharacterCommon>().TakeDamage(minDamage, maxDamage);
		yield return new WaitForSeconds(stunDuration);
		target.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
		target.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
		Destroy(gameObject);


	}

	IEnumerator CanStunTimer()
	{
		yield return new WaitForSeconds(canStunDuration);
		canStun = false;
	}

	//Incase zen freak dies and stun is still applied, use this to disable stuns somehow
	//private void OnDestroy()
	//{
		
	//}
}
