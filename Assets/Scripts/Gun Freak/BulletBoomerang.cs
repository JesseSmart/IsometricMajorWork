using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBoomerang : MonoBehaviour
{
	public GameObject myOwner;
	private float speed = 10;

	public float minDamage;
	public float maxDamage;
    // Start is called before the first frame update
    void Start()
    {
		FindObjectOfType<CameraController>().CamShake(0.1f, 0.1f);
	}

    // Update is called once per frame
    void Update()
    {
		transform.Translate(Vector2.up * Time.deltaTime * speed);
    }

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.GetComponent<CharacterCommon>() && other.gameObject != myOwner && !other.gameObject.GetComponent<BoomerangScript>())
		{
			other.GetComponent<CharacterCommon>().TakeDamage(minDamage, maxDamage, myOwner);
			FindObjectOfType<CameraController>().FrameFreeze();
			FindObjectOfType<CameraController>().CamShake(0.1f, 0.1f);
			Destroy(gameObject);
		}
	}
}
