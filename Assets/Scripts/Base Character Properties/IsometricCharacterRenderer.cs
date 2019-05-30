using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricCharacterRenderer : MonoBehaviour
{

    public static readonly string[] idleDirections = { "Idle N", "Idle NE", "Idle E", "Idle SE", "Idle S", "Idle SW", "Idle W", "Idle NW" };
    public static readonly string[] runDirections = { "Run N", "Run NE", "Run E", "Run SE", "Run S", "Run SW", "Run W", "Run NW" };

    public GameObject characterImageObject;
    Animator anim;
    int lastDirection;

    private void Awake()
    {
        anim = characterImageObject.GetComponent<Animator>();
    }


    public void SetDirection(Vector2 direction)
    {
        string[] directionArray = null;

        if (direction.magnitude < .01f)
        {
            directionArray = idleDirections;
        }
        else
        {
            directionArray = runDirections;
            lastDirection = DirectionToIndex(direction, 8);
        }
        anim.Play(directionArray[lastDirection]);
    }

    public static int DirectionToIndex(Vector2 dir, int sliceCount)
    {
        Vector2 normDir = dir.normalized;
        float step = 360f / sliceCount;
        float halfstep = step / 2;
        float angle = Vector2.SignedAngle(Vector2.up, normDir);
        angle += halfstep;

        if (angle < 0)
        {
            angle += 360;
        }

        float stepCount = angle / step;

        return Mathf.FloorToInt(stepCount);
        
    }
    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
