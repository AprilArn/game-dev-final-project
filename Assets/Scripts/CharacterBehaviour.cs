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
    public Transform DumpTruck;
    private List<Transform> TrashList = new List<Transform>();

    private bool IsTrashInHands = false;
    private bool IsTrashFlying = false;
    private bool isOpen = false;
    private bool canInteractWithTrash = true; // Variable to control interaction with trash
    private float T = 0;
    private float doorDurationOpen = 0.7f;
    private float doorDurationClose = 1f;

    private int trashHitCount = 0; // Counter for trash hitting the target point
    private Vector3 originalDumpTruckPosition;

    private void Start()
    {

        originalDumpTruckPosition = DumpTruck.position; // Store the original position of the DumpTruck

    }

    // Update is called once per frame
    void Update() 
    {

        // walking
        Vector3 direction = new Vector3( Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical") );
        transform.position += direction * MoveSpeed * Time.deltaTime;
        if ( direction != Vector3.zero )
            transform.LookAt( transform.position + direction );

        // trash in hands
        if ( IsTrashInHands )
        {

            // hold over head
            if ( Input.GetKey(KeyCode.Space) )
            {

                TrashList[ TrashList.Count - 1 ].position = PosOverHead.position;
                Arms.localEulerAngles = Vector3.right * 180;

                // look at the target point of TrashCan
                transform.LookAt( Target );

            }

            // dribbling
            else
            {

                TrashList[ TrashList.Count - 1 ].position = PosDribble.position + Vector3.up * Mathf.Abs( Mathf.Sin(Time.time * 5) );
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

            TrashList[ TrashList.Count - 1 ].position = pos + arc;

            // moment when trash arrives at the target point
            if ( t01 >= 1 )
            {

                IsTrashFlying = false;
                TrashList[ TrashList.Count - 1 ].GetComponent<Rigidbody>().isKinematic = false;

                // Increment the counter when the trash hits the target point
                trashHitCount++;

                // Check if trash has hit the target point 5 times
                if ( trashHitCount >= 5 )
                {

                    trashHitCount = 0; // Reset the counter
                    canInteractWithTrash = false; // Prevent interaction with trash

                    // close the door
                    if ( isOpen )
                    {

                        LeanTween.rotateX( Door.gameObject, 0f, doorDurationClose ).setEase( LeanTweenType.easeInOutQuad ).setOnComplete(() =>
                        {

                            MoveDumpTruck();

                        });
                        isOpen = false;

                    }

                }

                else
                {

                    // close the door if it's not yet closed and trashHitCount < 5
                    if ( isOpen )
                    {

                        LeanTween.rotateX( Door.gameObject, 0f, doorDurationClose ).setEase( LeanTweenType.easeInOutQuad );
                        isOpen = false;

                    }

                }

            }

        }

    }

    private void OnTriggerEnter( Collider other )
    {
        if ( other.CompareTag("Trash") && !IsTrashInHands && !IsTrashFlying && canInteractWithTrash )
        {

            IsTrashInHands = true;
            TrashList.Add( other.transform );
            other.GetComponent<Rigidbody>().isKinematic = true;

        }

    }

    // Method to move the DumpTruck forward and then back
    private void MoveDumpTruck()
    {

        Vector3 targetPosition = DumpTruck.position + Vector3.forward * 15;
        LeanTween.move( DumpTruck.gameObject, targetPosition, 4f ).setEase( LeanTweenType.easeInOutQuad ).setOnComplete(() =>
        {

            // Destroy trash after dump truck moves forward
            foreach ( Transform trash in TrashList )
            {

                Destroy(trash.gameObject);

            }
            TrashList.Clear();

            LeanTween.move( DumpTruck.gameObject, originalDumpTruckPosition, 4f ).setEase( LeanTweenType.easeInOutQuad ).setOnComplete(() =>
            {

                canInteractWithTrash = true; // Allow interaction with trash once the truck is back

            });

        });

    }

}
