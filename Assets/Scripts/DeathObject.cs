using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathObject : MonoBehaviour
{

	public float dur = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
		Destroy(gameObject, dur);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
