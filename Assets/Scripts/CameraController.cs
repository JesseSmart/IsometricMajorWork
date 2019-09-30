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

		Move();
		Zoom();


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

}
