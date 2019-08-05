using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecieverSpear : MonoBehaviour
{

	private RecieverSpear[] spears;
	public float lightningDamage;
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

		RaycastHit2D hit = Physics2D.Raycast(transform.position, recieverScript.gameObject.transform.position);
		if (hit.collider != null) //error
		{
			

			GameObject hitObj = hit.collider.gameObject;
			print("hit esixtsts " + hitObj);
			if (hitObj.CompareTag("PlayerCharacter"))
			{
				print("Is player");

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
							hitObj.GetComponent<CharacterCommon>().TakeDamage(lightningDamage);
							print("Lightning damage");

							//damage
							//knockback

							return;
						}
					}

				}
			}
		}
	}
}
