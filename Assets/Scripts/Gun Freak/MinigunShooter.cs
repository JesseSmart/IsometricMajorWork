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
    // Start is called before the first frame update
    void Start()
    {
		canFire = true;
		StartCoroutine(FireBullet());
		StartCoroutine(DurationTimer());
    }

    // Update is called once per frame
    void Update()
    {
		transform.position = myOwner.transform.position;

		//Vector2 currentPos = transform.position;
		//float horizontalInput = Input.GetAxis(horizontalArray[myOwner.GetComponent<IsometricPlayerMovementController>().playerNumber]);
		//float verticalInput = Input.GetAxis(verticalArray[myOwner.GetComponent<IsometricPlayerMovementController>().playerNumber]);

		//Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
		////currentDir = inputVector;

		//if (inputVector != Vector2.zero)
		//{
		//	lastDir = inputVector;
		//}
		Vector2 tempVec = ((Vector2)transform.position + myOwner.GetComponent<IsometricPlayerMovementController>().lastDir.normalized);
		//Vector2 rotVec = new Vector3(tempVec.x, tempVec.y, 0);
		//transform.LookAt(rotVec.normalized);
		transform.up = tempVec - (Vector2)transform.position;


		//transform.LookAt((Vector2)transform.position + myOwner.GetComponent<IsometricPlayerMovementController>().lastDir.normalized);
	}

	IEnumerator FireBullet()
	{
		yield return new WaitForSeconds(fireRate);
		float rnd = Random.Range(-1.0f,1.0f);
		Vector3 newPos = new Vector3(transform.position.x + rnd, transform.position.y, 0);
		GameObject bullet = Instantiate(bulletObj, newPos, transform.rotation);
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
