using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBlast : MonoBehaviour
{
	public float range;
	public float circumferance;
	public float knockBackModifier;
	public float duration;
	public GameObject myOwner;

    // Start is called before the first frame update
    void Start()
    {
		SelfKnockBack();
		Destroy(gameObject, duration);

    }

    // Update is called once per frame
    void Update()
    {
        
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
		myOwner.GetComponent<Rigidbody2D>().AddForce(myOwner.GetComponent<IsometricPlayerMovementController>().lastDir * -knockBackModifier,ForceMode2D.Force);
	}

	void TargetKnockBack(GameObject opponent)
	{
		opponent.GetComponent<Rigidbody2D>().AddForce(myOwner.GetComponent<IsometricPlayerMovementController>().lastDir * knockBackModifier);

	}

}
