using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigunShooter : MonoBehaviour
{

	private string[] horizontalArray = new string[4] { "Horizontal", "P2Horizontal", "P3Horizontal", "P4Horizontal" }; //CHANGE BACK TO P1 on start of first item in array to make work for controller ***
	private string[] verticalArray = new string[4] { "Vertical", "P2Vertical", "P3Vertical", "P4Vertical" };

	public GameObject myOwner;
	public GameObject bulletObj;
	public float fireRate;
	public float duration;

	private bool canFire;
	private Vector2 lastDir;

	private float minigunOffset = 1;

	private Vector2 vel;
    // Start is called before the first frame update
    void Start()
    {
		canFire = true;
		StartCoroutine(FireBullet());
		StartCoroutine(DurationTimer());
		//myOwner.GetComponent<IsometricPlayerMovementController>().DisableMove(duration);
    }

    // Update is called once per frame
    void Update()
    {
		transform.position = myOwner.transform.position;


		Vector2 tempVec = ((Vector2)transform.position + myOwner.GetComponent<IsometricPlayerMovementController>().lastDir.normalized);

		transform.up = tempVec - (Vector2)transform.position;

		//try make slow turn
		//Vector2 tempVec = Vector2.SmoothDamp((Vector2)transform.position, ((Vector2)transform.position + myOwner.GetComponent<IsometricPlayerMovementController>().lastDir.normalized), ref vel, 10f);
		//transform.up = Vector2.Lerp((Vector2)transform.up, tempVec - (Vector2)transform.position, Time.deltaTime * 0.2f);

	}

	IEnumerator FireBullet()
	{
		yield return new WaitForSeconds(fireRate);
		Vector3 myDir = new Vector3(myOwner.GetComponent<IsometricPlayerMovementController>().lastDir.x, myOwner.GetComponent<IsometricPlayerMovementController>().lastDir.y, 0);

		Vector3 fakePos = transform.position + (myDir.normalized * minigunOffset);
		float myAngle = Mathf.Atan2(fakePos.y - transform.position.y, fakePos.x - transform.position.x) * 180 / Mathf.PI;
		Quaternion myRot = Quaternion.Euler(0, 0, myAngle - 90 + Random.Range(-10, 10));

		GameObject bullet = Instantiate(bulletObj, fakePos, myRot);
		bullet.GetComponent<BulletMinigun>().myOwner = myOwner;
		if (canFire)
		{
			StartCoroutine(FireBullet());
		}
		else
		{
			Destroy(gameObject);
		}
	}

	IEnumerator DurationTimer()
	{
		myOwner.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
		yield return new WaitForSeconds(duration);
		canFire = false;
		myOwner.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
		myOwner.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

	}


}
