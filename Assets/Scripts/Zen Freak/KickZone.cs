﻿using System.Collections;
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
	public float launchDuration;

	public float minDamage;
	public float maxDamage;

	private AudioSource audio;
	public AudioClip acKick;
	public AudioClip acSelfLaunch;
	public AudioClip acEnemyLaunch;
	// Start is called before the first frame update
	void Start()
	{
		//Destroy(gameObject, canKnockbackDuration + 0.2f);
		StartCoroutine(CanKnockTimer());
		//effected.GetComponent<IsometricPlayerMovementController>().canInput = false;
		audio = GetComponent<AudioSource>();
		audio.PlayOneShot(acKick);
	}

	// Update is called once per frame
	void Update()
	{
		if (!canKnockback)
		{
			SelfKnockBack();
			canKnockback = true;
		}
	}


	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("PlayerCharacter") && other.gameObject != myOwner)
		{
			TargetKnockBack(other.gameObject);
			other.gameObject.GetComponent<CharacterCommon>().TakeDamage(minDamage, maxDamage, myOwner);

		}
	}



	void SelfKnockBack()
	{
		print("Knock me back = " + myOwner.GetComponent<IsometricPlayerMovementController>().lastDir.normalized * -knockBackModifier);
		audio.PlayOneShot(acSelfLaunch);
		StartCoroutine(KnockBack(myOwner, -1));
	}

	void TargetKnockBack(GameObject opponent)
	{
		print("Knock enemy back = " + myOwner.GetComponent<IsometricPlayerMovementController>().lastDir.normalized * knockBackModifier);
		audio.PlayOneShot(acEnemyLaunch);
		StartCoroutine(KnockBack(opponent, 1));
		FindObjectOfType<CameraController>().FrameFreeze();

	}

	IEnumerator CanKnockTimer()
	{
		yield return new WaitForSeconds(canKnockbackDuration);
		canKnockback = false;
	}

	IEnumerator KnockBack(GameObject effected, int dir)
	{
		Rigidbody2D rb = effected.GetComponent<Rigidbody2D>();
		rb.AddForce(myOwner.GetComponent<IsometricPlayerMovementController>().lastDir.normalized * knockBackModifier * dir, ForceMode2D.Impulse);
		effected.GetComponent<IsometricPlayerMovementController>().canInput = false;

		yield return new WaitForSeconds(launchDuration);
		effected.GetComponent<IsometricPlayerMovementController>().canInput = true;

		effected.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		Destroy(gameObject, 0.2f);

	}
}
