using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecieverSpear : MonoBehaviour
{

	private RecieverSpear[] spears;
	public float minLightningDamage;
	public float maxLightningDamage;
	public GameObject myOwner;
	public float lifeSoFar;

	private GameObject[] alreadyHitObjs = new GameObject[3];
	private LineRenderer lineR;

	private bool hasRunZap;

	// Start is called before the first frame update
	void Start()
    {
		lineR = GetComponent<LineRenderer>();

	}

	// Update is called once per frame
	void Update()
    {
		lifeSoFar += Time.deltaTime;
		CheckForOtherReciever();
    }

	void CheckForOtherReciever()
	{
		spears = FindObjectsOfType<RecieverSpear>();
		if (spears.Length > 1)
		{
			foreach (RecieverSpear reciever in spears)
			{
				if (reciever != this && reciever.myOwner == myOwner && lifeSoFar > reciever.lifeSoFar)
				{
					if (!hasRunZap)
					{
						StartCoroutine(ZapClock(reciever));
						hasRunZap = true;
					}
				}
			}
		}
	}



	IEnumerator ZapClock(RecieverSpear recieverScript)
	{
		lineR.enabled = true;
		lineR.SetPosition(0, transform.position);
		lineR.SetPosition(1, recieverScript.gameObject.transform.position);
		ZapRayCollision(recieverScript);
		yield return new WaitForSeconds(2f);
		lineR.enabled = false;
		Destroy(gameObject);
	}
	void Zap()
	{

	}

	void ZapRayCollision(RecieverSpear recieverScript) //only run once per object/character (like trigger entre)
	{
		print("Zap");

		RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, recieverScript.gameObject.transform.position, Vector3.Distance(transform.position, recieverScript.gameObject.transform.position));

		if (hits.Length > 0)
		{
			for (int i = 0; i < hits.Length; i++)
			{
				GameObject hitObj = hits[i].collider.gameObject;

				if (hitObj.GetComponent<CharacterCommon>())
				{
					hitObj.GetComponent<CharacterCommon>().TakeDamage(minLightningDamage, maxLightningDamage, myOwner);
					print("ZAP");
				}

			}
		}

		/*
		if (hit.collider != null) //error
		{
			

			GameObject hitObj = hit.collider.gameObject;
			print("hit esixtsts " + hitObj);
			if (hitObj.GetComponent<CharacterCommon>())
			{
				print("Is player");
				hitObj.GetComponent<CharacterCommon>().TakeDamage(minLightningDamage, maxLightningDamage, myOwner);

				foreach (GameObject obj in alreadyHitObjs)
				{
					if (hitObj == obj)
					{
						return;
					}
				}
				print("post return");

				if (hitObj != myOwner.gameObject)
				{
					for (int i = 0; i < alreadyHitObjs.Length; i++)
					{
						if (alreadyHitObjs[i] == null)
						{
							alreadyHitObjs[i] = hitObj;
							hitObj.GetComponent<CharacterCommon>().TakeDamage(minLightningDamage, maxLightningDamage, myOwner);
							print("Lightning damage");

							//damage
							//knockback

							return;
						}
					}

				}
			}
		}
		*/
	}
}
