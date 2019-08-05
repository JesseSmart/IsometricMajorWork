using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleDamageZone : MonoBehaviour
{
	public float damage;
	public float damageIntervals;
	public float zoneDuration;
	public GameObject myOwner;

	private bool damageHasRun;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter2D(Collider2D other)
	{

		if (other.gameObject.CompareTag("PlayerCharacter") && other.gameObject != myOwner)
		{
			if (!damageHasRun)
			{
				AreaDamage(other.gameObject);
				damageHasRun = true;
			}
		}
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("PlayerCharacter") && other.gameObject != myOwner)
		{
			if (!damageHasRun)
			{
				AreaDamage(other.gameObject);
				damageHasRun = true;
			}
		}
	}


	IEnumerator AreaDamage(GameObject targetObj)
	{

		targetObj.gameObject.GetComponent<CharacterCommon>().TakeDamage(damage);
		yield return new WaitForSeconds(1.0f);
		damageHasRun = false;
	}



}
