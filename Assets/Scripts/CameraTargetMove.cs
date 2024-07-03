// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CameraTargetMove : MonoBehaviour
// {

//     public float delta = 0.95f;  // Amount to move left and right from the start point
//     public float speed = 2.0f; 
//     private Vector3 startPos;

//     // Start is called before the first frame update
//     void Start()
//     {

//         startPos = transform.position;

//     }

//     // Update is called once per frame
//     void Update()
//     {

//         Vector3 v = startPos;
//         v.x += delta * Mathf.Sin( Time.time * speed );
//         transform.position = v;

//     }
    
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetMove : MonoBehaviour
{
    public float delta = 20f;  // Amount to move left and right from the start point
    public float speed = 0.75f;
    public float verticalDelta = 5.5f; // Amount to move up and down from the start point
    public float verticalSpeed = 0.5f; // Speed of vertical movement
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = startPos;
        v.x += delta * Mathf.Sin(Time.time * speed);
        v.y += verticalDelta * Mathf.Sin(Time.time * verticalSpeed);
        transform.position = v;
    }
}
