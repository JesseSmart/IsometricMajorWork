﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAxe : MonoBehaviour
{
	public float minDamage;
	public float maxDamage;
	public Rigidbody2D rbody;
    public int initialForce;

    public GameObject myOwner;

    public float maxFlightDuration;
    private float flightTimer;

    public float pullSpeed;
    private Vector2 direction;

    private bool stuckToTarget;
    private bool stuckToTerrain;
    private GameObject myTarget;

    private float pullFloat;
	public float endPullDist;

    private LineRenderer lineR;

	//AUDIO
	private AudioSource audio;
	public AudioClip acHitTerrain;
	//public AudioClip acHitTarget;
	public AudioClip acChainYank;


    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        flightTimer = maxFlightDuration;
        lineR = GetComponent<LineRenderer>();
		audio = GetComponent<AudioSource>();

		audio.clip = acChainYank;
		audio.loop = true;
		audio.Play();
	}

    // Update is called once per frame
    void Update()
    {
        lineR.SetPosition(0, transform.position);
        lineR.SetPosition(1, myOwner.transform.position);

		//only run if not hit
        flightTimer -= Time.deltaTime;
        if (flightTimer <= 0 && (!stuckToTarget && !stuckToTerrain)) //add a check to make sure the player is not currently flying ( && axeHasHit == false) or the existing stuckToXYZ
        {
            Destroy(gameObject);
        }

        if (stuckToTarget)
        {
			if (myTarget != null)
			{
				transform.position = myTarget.transform.position;
				TargetToViking(myTarget);

			}
			else
			{
				myOwner.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
				myOwner.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
				Destroy(gameObject);

			}
		}
		else if (stuckToTerrain)
        {
			VikingToTerrain();
        }
    }

    void FixedUpdate()
    {

    }


    public void Throw(Vector2 dir)
    {
        direction = dir;

		rbody.AddForce(dir.normalized * initialForce);


        //look into force modes like acceleration
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!stuckToTarget && !stuckToTerrain)
        {
		
			if (other.gameObject.CompareTag("PlayerCharacter") && other.gameObject != myOwner) 
            {
                //deal damage here
                other.gameObject.GetComponent<CharacterCommon>().TakeDamage(minDamage, maxDamage, myOwner);
				FindObjectOfType<CameraController>().FrameFreeze();
				myTarget = other.gameObject;
                stuckToTarget = true;
				rbody.constraints = RigidbodyConstraints2D.FreezeAll;
                //Freeze player movement on hit
                myOwner.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
				myOwner.layer = 13; //layer stuff

				//audio.PlayOneShot(acHitTarget);
				//audio.clip = acChainYank;
				//audio.PlayDelayed(acHitTarget.length);
				//audio.loop = true;
			}
			else if (other.gameObject != myOwner)
            {

				//terrain
				stuckToTerrain = true;
				rbody.constraints = RigidbodyConstraints2D.FreezeAll;

				//Freeze player movement on hit
				myOwner.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
				myOwner.layer = 13; //layer stuff

				audio.PlayOneShot(acHitTerrain);
				audio.clip = acChainYank;
				audio.PlayDelayed(acHitTerrain.length);
				audio.loop = true;

			}


        }
        
    }


    public void TargetToViking(GameObject target)
    {
        pullFloat += Time.deltaTime;
        target.transform.position = Vector2.Lerp(target.transform.position, myOwner.transform.position, pullFloat * pullSpeed);
		CheckDist();
	}

    public void VikingToTerrain()
    {
        pullFloat += Time.deltaTime;

        myOwner.transform.position = Vector2.Lerp(myOwner.transform.position, transform.position, pullFloat * pullSpeed);
		CheckDist();
    }

	void CheckDist()
	{
		float dist = Vector2.Distance(transform.position, myOwner.transform.position);

		if (dist <= endPullDist)
		{
			//Add force outwards for bounce off effect
			myOwner.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
			myOwner.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
			myOwner.layer =9; //layer stuff
			Destroy(gameObject);
		}
		else
		{
		
		}
	}

}
