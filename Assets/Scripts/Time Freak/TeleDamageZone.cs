﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleDamageZone : MonoBehaviour
{
	public float minDamage;
	public float maxDamage;
	public float damageIntervals;
	public float zoneDuration;
	public GameObject myOwner;

	private bool damageHasRun;


	private AudioSource audio;
	public AudioClip acZoneStart;
	public AudioClip acLoopZap;
	public AudioClip STILLNEEDIMPLEMENTATION;
    // Start is called before the first frame update
    void Start()
    {
		StartCoroutine(SelfDestroy());
    }

    // Update is called once per frame
    void Update()
    {
		print(damageHasRun);
    }

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("PlayerCharacter") && other.gameObject != myOwner)
		{

			if (!damageHasRun)
			{
				StartCoroutine(AreaDamage(other.gameObject));
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
				//not running?
				StartCoroutine(AreaDamage(other.gameObject));
				damageHasRun = true;
			}
		}
	}


	IEnumerator AreaDamage(GameObject targetObj)
	{
		print("damage pre");
		targetObj.gameObject.GetComponent<CharacterCommon>().TakeDamage(minDamage, maxDamage, myOwner);
		//FindObjectOfType<CameraController>().FrameFreeze();
		yield return new WaitForSeconds(damageIntervals);
		damageHasRun = false;
	}

	IEnumerator SelfDestroy()
	{
		yield return new WaitForSeconds(zoneDuration);
		Destroy(gameObject);
	}



}
