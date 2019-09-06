using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZoneMaster : MonoBehaviour
{
	public float speed;
	public float duration;
	public float roundTimer;
    // Start is called before the first frame update
    void Start()
    {
        duration = PlayerPrefs.GetInt("RoundDuration");
		roundTimer = duration;
	}

	// Update is called once per frame
	void Update()
    {
		if (roundTimer > 0)
		{
			roundTimer -= Time.deltaTime * 0.2f;

		}
    }
}
