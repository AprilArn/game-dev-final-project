// ----- Not Moving -----
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CharacterBehaviour : MonoBehaviour
// {
//     public float MoveSpeed = 10;

//     public Transform Arms;
//     public Transform PosOverHead;
//     public Transform PosDribble;
//     public Transform Target;
//     public Transform Door;
//     private Transform Trash;

//     private bool IsTrashInHands = false;
//     private bool IsTrashFlying = false;
//     private bool isOpen = false;
//     private float T = 0;
//     private float doorDurationOpen = 0.65f;
//     private float doorDurationClose = 0.9f;

//     // Update is called once per frame
//     void Update() 
//     {
//         // walking
//         Vector3 direction = new Vector3( Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical") );
//         transform.position += direction * MoveSpeed * Time.deltaTime;
//         if( direction != Vector3.zero )
//             transform.LookAt( transform.position + direction );

//         // trash in hands
//         if ( IsTrashInHands )
//         {
//             // hold over head
//             if ( Input.GetKey(KeyCode.Space) )
//             {
//                 Trash.position = PosOverHead.position;
//                 Arms.localEulerAngles = Vector3.right * 180;

//                 // look at the target point of TrashCan
//                 transform.LookAt( Target );
//             }
//             // dribbling
//             else
//             {
//                 Trash.position = PosDribble.position + Vector3.up * Mathf.Abs( Mathf.Sin(Time.time * 5) );
//                 Arms.localEulerAngles = Vector3.right * 0;
//             }

//             // throw trash
//             if ( Input.GetKeyUp(KeyCode.Space) )
//             {
//                 IsTrashInHands = false;
//                 IsTrashFlying = true;
//                 T = 0;
//             }

//             // open the door
//             if ( !isOpen )
//             {
//                 LeanTween.rotateX( Door.gameObject, -35f, doorDurationOpen ).setEase( LeanTweenType.easeInOutQuad );
//                 isOpen = true;
//             }
//         }

//         // trash in the air
//         if ( IsTrashFlying )
//         {
//             T += Time.deltaTime;
//             float duration = 0.5f;
//             float t01 = T / duration;

//             Vector3 A = PosOverHead.position;
//             Vector3 B = Target.position;
//             Vector3 pos = Vector3.Lerp( A, B, t01 );

//             // move in arc
//             Vector3 arc = Vector3.up * 5 * Mathf.Sin( t01 * 3.14f );

//             Trash.position = pos + arc;

//             // moment when trash arrives at the target point
//             if ( t01 >= 1 )
//             {
//                 IsTrashFlying = false;
//                 Trash.GetComponent<Rigidbody>().isKinematic = false;
//                 Trash = null;

//                 // close the door
//                 if ( isOpen )
//                 {
//                     LeanTween.rotateX( Door.gameObject, 0f, doorDurationClose).setEase(LeanTweenType.easeInOutQuad );
//                     isOpen = false;
//                 }
//             }
//         }
//     }

//     private void OnTriggerEnter( Collider other )
//     {
//         if ( other.CompareTag("Trash") && !IsTrashInHands && !IsTrashFlying )
//         {
//             IsTrashInHands = true;
//             Trash = other.transform;
//             Trash.GetComponent<Rigidbody>().isKinematic = true;
//         }
//     }
// }


// ----- Moving Forward -----
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    public float MoveSpeed = 10;

    public Transform Arms;
    public Transform PosOverHead;
    public Transform PosDribble;
    public Transform Target;
    public Transform Door;
    public Transform DumpTruck; // Reference to the DumpTruck object
    private Transform Trash;

    private bool IsTrashInHands = false;
    private bool IsTrashFlying = false;
    private bool isOpen = false;
    private float T = 0;
    private float doorDurationOpen = 0.7f;
    private float doorDurationClose = 1f;

    private int trashHitCount = 0; // Counter for trash hitting the target point

    // Update is called once per frame
    void Update() 
    {
        // walking
        Vector3 direction = new Vector3( Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical") );
        transform.position += direction * MoveSpeed * Time.deltaTime;
        if( direction != Vector3.zero )
            transform.LookAt( transform.position + direction );

        // trash in hands
        if ( IsTrashInHands )
        {

            // hold over head
            if ( Input.GetKey(KeyCode.Space) )
            {

                Trash.position = PosOverHead.position;
                Arms.localEulerAngles = Vector3.right * 180;

                // look at the target point of TrashCan
                transform.LookAt( Target );

            }

            // dribbling
            else
            {

                Trash.position = PosDribble.position + Vector3.up * Mathf.Abs( Mathf.Sin(Time.time * 5) );
                Arms.localEulerAngles = Vector3.right * 0;

            }

            // throw trash
            if ( Input.GetKeyUp(KeyCode.Space) )
            {

                IsTrashInHands = false;
                IsTrashFlying = true;
                T = 0;

            }

            // open the door
            if ( !isOpen )
            {

                LeanTween.rotateX( Door.gameObject, -35f, doorDurationOpen ).setEase( LeanTweenType.easeInOutQuad );
                isOpen = true;

            }
        }

        // trash in the air
        if ( IsTrashFlying )
        {
            T += Time.deltaTime;
            float duration = 0.5f;
            float t01 = T / duration;

            Vector3 A = PosOverHead.position;
            Vector3 B = Target.position;
            Vector3 pos = Vector3.Lerp( A, B, t01 );

            // move in arc
            Vector3 arc = Vector3.up * 5 * Mathf.Sin( t01 * 3.14f );

            Trash.position = pos + arc;

            // moment when trash arrives at the target point
            if ( t01 >= 1 )
            {

                IsTrashFlying = false;
                Trash.GetComponent<Rigidbody>().isKinematic = false;
                Trash = null;

                // Increment the counter when the trash hits the target point
                trashHitCount++;

                // Check if trash has hit the target point 10 times
                if ( trashHitCount >= 5 )
                {

                    MoveDumpTruck();
                    trashHitCount = 0; // Reset the counter

                }

                // close the door
                if ( isOpen )
                {

                    LeanTween.rotateX( Door.gameObject, 0f, doorDurationClose).setEase(LeanTweenType.easeInOutQuad );
                    isOpen = false;

                }

            }
        }
    }

    private void OnTriggerEnter( Collider other )
    {
        if ( other.CompareTag("Trash") && !IsTrashInHands && !IsTrashFlying )
        {
            IsTrashInHands = true;
            Trash = other.transform;
            Trash.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    // Method to move the DumpTruck forward
    private void MoveDumpTruck()
    {
        Vector3 targetPosition = DumpTruck.position + Vector3.forward * 4;
        LeanTween.move( DumpTruck.gameObject, targetPosition, 2f ).setEase( LeanTweenType.easeInOutQuad );
    }
}


//  ----- Moving Forward and Rotate -----

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CharacterBehaviour : MonoBehaviour
// {
//     public float MoveSpeed = 10;

//     public Transform Arms;
//     public Transform PosOverHead;
//     public Transform PosDribble;
//     public Transform Target;
//     public Transform Door;
//     public Transform DumpTruck; // Reference to the DumpTruck object
//     private Transform Trash;

//     private bool IsTrashInHands = false;
//     private bool IsTrashFlying = false;
//     private bool isOpen = false;
//     private float T = 0;
//     private float doorDurationOpen = 0.65f;
//     private float doorDurationClose = 0.9f;

//     private int trashHitCount = 0; // Counter for trash hitting the target point

//     // Update is called once per frame
//     void Update() 
//     {
//         // walking
//         Vector3 direction = new Vector3( Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical") );
//         transform.position += direction * MoveSpeed * Time.deltaTime;
//         if( direction != Vector3.zero )
//             transform.LookAt( transform.position + direction );

//         // trash in hands
//         if ( IsTrashInHands )
//         {
//             // hold over head
//             if ( Input.GetKey(KeyCode.Space) )
//             {
//                 Trash.position = PosOverHead.position;
//                 Arms.localEulerAngles = Vector3.right * 180;

//                 // look at the target point of TrashCan
//                 transform.LookAt( Target );
//             }
//             // dribbling
//             else
//             {
//                 Trash.position = PosDribble.position + Vector3.up * Mathf.Abs( Mathf.Sin(Time.time * 5) );
//                 Arms.localEulerAngles = Vector3.right * 0;
//             }

//             // throw trash
//             if ( Input.GetKeyUp(KeyCode.Space) )
//             {
//                 IsTrashInHands = false;
//                 IsTrashFlying = true;
//                 T = 0;
//             }

//             // open the door
//             if ( !isOpen )
//             {
//                 LeanTween.rotateX( Door.gameObject, -35f, doorDurationOpen ).setEase( LeanTweenType.easeInOutQuad );
//                 isOpen = true;
//             }
//         }

//         // trash in the air
//         if ( IsTrashFlying )
//         {
//             T += Time.deltaTime;
//             float duration = 0.5f;
//             float t01 = T / duration;

//             Vector3 A = PosOverHead.position;
//             Vector3 B = Target.position;
//             Vector3 pos = Vector3.Lerp( A, B, t01 );

//             // move in arc
//             Vector3 arc = Vector3.up * 5 * Mathf.Sin( t01 * 3.14f );

//             Trash.position = pos + arc;

//             // moment when trash arrives at the target point
//             if ( t01 >= 1 )
//             {
//                 IsTrashFlying = false;
//                 Trash.GetComponent<Rigidbody>().isKinematic = false;
//                 Trash = null;

//                 // Increment the counter when the trash hits the target point
//                 trashHitCount++;

//                 // Check if trash has hit the target point 10 times
//                 if ( trashHitCount >= 5 )
//                 {
//                     MoveDumpTruck();
//                     trashHitCount = 0; // Reset the counter
//                 }

//                 // close the door
//                 if ( isOpen )
//                 {
//                     LeanTween.rotateX( Door.gameObject, 0f, doorDurationClose).setEase(LeanTweenType.easeInOutQuad );
//                     isOpen = false;
//                 }
//             }
//         }
//     }

//     private void OnTriggerEnter( Collider other )
//     {
//         if ( other.CompareTag("Trash") && !IsTrashInHands && !IsTrashFlying )
//         {
//             IsTrashInHands = true;
//             Trash = other.transform;
//             Trash.GetComponent<Rigidbody>().isKinematic = true;
//         }
//     }

//     // Method to move the DumpTruck forward and perform subsequent actions
//     private void MoveDumpTruck()
//     {
//         Vector3 forwardPosition = DumpTruck.position + Vector3.forward * 3;
//         LeanTween.move(DumpTruck.gameObject, forwardPosition, 2f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
//         {
//             RotateDumpTruck();
//         });
//     }

//     // Method to rotate the DumpTruck to the left
//     private void RotateDumpTruck()
//     {
//         LeanTween.rotateY(DumpTruck.gameObject, -90f, 2f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
//         {
//             MoveDumpTruckBackward();
//         });
//     }

//     // Method to move the DumpTruck backward on the X-axis
//     private void MoveDumpTruckBackward()
//     {
//         Vector3 backwardPosition = DumpTruck.position + Vector3.left * 15;
//         LeanTween.move(DumpTruck.gameObject, backwardPosition, 2f).setEase(LeanTweenType.easeInOutQuad);
//     }

// }