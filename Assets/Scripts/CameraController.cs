using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{

	public List<Transform> targets;
	public Vector3 offset;

	private Vector3 velocity;
	public float smoothTime = 0.5f;
	public float zoomSpeed;

	public float minZoom = 40;
	public float maxZoom = 10;
	public float zoomLimiter = 50f;

	private float distAway;
	private Camera cam;

	//BrinkZoom
	private bool brinkOveriding;
	private float zoomAmount = 1.5f;
	private float brinkMoveSmoothMultiplier = 3;
	private float brinkZoomSmoothMultiplier = 3;
	private float brinkZoomDuration = 2;
    // Start is called before the first frame update
    void Start()
    {
		cam = GetComponent<Camera>();
		distAway = transform.position.z;
    }

	void LateUpdate()
	{
		if (targets.Count == 0)
		{
			return;
		}

		if (!brinkOveriding)
		{
			Move();
			Zoom();


		}

	}

	void Move()
	{
		Vector3 centrePoint = GetCenterPoint();

		Vector3 newPosition = centrePoint + offset;

		//transform.position = newPosition;
		transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);

		transform.position = new Vector3(transform.position.x, transform.position.y, distAway);
	}

	void Zoom()
	{

		float newZoom = Mathf.Lerp(minZoom, maxZoom, GetGreatestDistance() / zoomLimiter);
		//cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
		cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, Time.deltaTime * zoomSpeed);
	}


	float GetGreatestDistance()
	{
		var bounds = new Bounds(targets[0].position, Vector3.zero);
		for (int i = 0; i < targets.Count; i++)
		{
			bounds.Encapsulate(targets[i].position);

		}

		return bounds.size.x;
	}

	Vector3 GetCenterPoint()
	{
		if (targets.Count == 1)
		{
			return targets[0].position;
		}

		var bounds = new Bounds(targets[0].position, Vector3.zero);
		for (int i = 0; i < targets.Count; i++)
		{
			bounds.Encapsulate(targets[i].position);
		}

		return bounds.center;
	}

	public void PlayersHaveSpawned()
	{

		CharacterCommon[] players = FindObjectsOfType<CharacterCommon>();
		for (int i = 0; i < players.Length; i++)
		{
			targets.Add(players[i].gameObject.transform);
		}
	}

	public void PlayerDeath(Transform deadPTrans)
	{
		targets.Remove(deadPTrans);

		//CharacterCommon[] players = FindObjectsOfType<CharacterCommon>();
		//for (int i = 0; i < players.Length; i++)
		//{
		//	targets.Add(players[i].gameObject.transform);
		//}
	}

	public void CamShake(float dur, float mag)
	{
		StartCoroutine(Shake(dur, mag));
	}

	IEnumerator Shake(float duration, float magnitude)
	{

		float elapsed = 0;

		while (elapsed < duration)
		{
			float x = transform.localPosition.x + Random.Range(-1f, 1f) * magnitude;
			float y = transform.localPosition.y + Random.Range(-1f, 1f) * magnitude;

			transform.localPosition = new Vector3(x, y, distAway); 
			elapsed += Time.deltaTime;

			yield return null;
		}
	}

	public void BrinkZoom(Transform interestPoint)
	{
		StartCoroutine(BrinkHitZoom(interestPoint, brinkZoomDuration));
	}

	IEnumerator BrinkHitZoom(Transform point, float duration)
	{
		brinkOveriding = true;
		//Time.timeScale = 0.05f;
		float elapsed = 0;

		while (elapsed < duration)
		{

			cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoomAmount, Time.deltaTime * brinkZoomSmoothMultiplier); //varieable it
			transform.position = Vector3.SmoothDamp(transform.position, point.position, ref velocity, smoothTime * brinkMoveSmoothMultiplier);
			transform.position = new Vector3(transform.position.x, transform.position.y, distAway);


			elapsed += Time.deltaTime;
			yield return null;
		}
		brinkOveriding = false;
		//Time.timeScale = 1;

	}

}
