using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickZone : MonoBehaviour
{
	public float range;
	public float circumferance;
	public float knockBackModifier;
	public GameObject myOwner;
	public float canKnockbackDuration;
	private bool canKnockback = true;
	// Start is called before the first frame update
	void Start()
	{
		Destroy(gameObject, canKnockbackDuration + 0.2f);
		StartCoroutine(CanKnockTimer());
	}

	// Update is called once per frame
	void Update()
	{
		if (!canKnockback)
		{
			SelfKnockBack();
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("PlayerCharacter"))
		{
			TargetKnockBack(other.gameObject);
		}
	}

	void SelfKnockBack()
	{
		print("Knock me back");
		myOwner.GetComponent<Rigidbody2D>().AddForce(myOwner.GetComponent<IsometricPlayerMovementController>().lastDir * -knockBackModifier, ForceMode2D.Force);
	}

	void TargetKnockBack(GameObject opponent)
	{
		opponent.GetComponent<Rigidbody2D>().AddForce(myOwner.GetComponent<IsometricPlayerMovementController>().lastDir * knockBackModifier);

	}

	IEnumerator CanKnockTimer()
	{
		yield return new WaitForSeconds(canKnockbackDuration);
		canKnockback = false;
	}
}
