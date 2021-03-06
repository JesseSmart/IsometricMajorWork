﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMinigun : MonoBehaviour
{
	public GameObject myOwner;
	public float speed;
	public float minDamage;
	public float maxDamage;
	public float duration;
    // Start is called before the first frame update
    void Start()
    {
		FindObjectOfType<CameraController>().CamShake(0.05f, 0.02f);
	}

    // Update is called once per frame
    void Update()
    {
		transform.Translate(Vector2.up * Time.deltaTime * speed);
		Destroy(gameObject, duration);
    }

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.GetComponent<CharacterCommon>() && other.gameObject != myOwner && !other.gameObject.GetComponent<BulletMinigun>())
		{
			other.GetComponent<CharacterCommon>().TakeDamage(minDamage, maxDamage, myOwner);
			FindObjectOfType<CameraController>().FrameFreeze();
			Destroy(gameObject);
		}
	}
}
