using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    public float MoveSpeed = 10;

    public Transform Trash;
    public Transform Arms;
    public Transform PosOverHead;
    public Transform PosDribble;
    public Transform Target;

    private bool IsTrashInHands = false;
    private bool IsTrashFlying = false;
    private float T = 0;

    // Update is called once per frame
    void Update() 
    {

        // walking
        Vector3 direction = new Vector3( Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical") );
        transform.position += direction * MoveSpeed * Time.deltaTime;
        transform.LookAt( transform.position + direction );

        // trash in hands
        if ( IsTrashInHands ) {

            // hold over head
            if( Input.GetKey(KeyCode.Space) ) {

                Trash.position = PosOverHead.position;
                Arms.localEulerAngles = Vector3.right * 180;

                // look at the targetpoint of TrashCan
                transform.LookAt(Target);

            }

            // dribbling
            else {

                Trash.position = PosDribble.position + Vector3.up * Mathf.Abs( Mathf.Sin(Time.time * 5) );
                Arms.localEulerAngles = Vector3.right * 0;

            }

            // throw trash
            if ( Input.GetKeyUp(KeyCode.Space) ) {

                IsTrashInHands = false;
                IsTrashFlying = true;
                T = 0;

            }

        }


        // trash in the air
        if ( IsTrashFlying ) {

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
            if ( t01 >= 1 ) {

                IsTrashFlying = false;
                Trash.GetComponent<Rigidbody>().isKinematic = false;

            }

        }

    }

    private void OnTriggerEnter( Collider other ) {

        if ( !IsTrashInHands && !IsTrashFlying ) {

            IsTrashInHands = true;
            Trash.GetComponent<Rigidbody>().isKinematic = true;

        }

    }
}
