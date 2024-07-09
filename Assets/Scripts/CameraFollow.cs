using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    public Transform target; // Target yang akan diikuti oleh kamera
    public float smoothSpeed; // Kecepatan kamera mengarahkan pandangannya

    // Update is called once per frame
    void LateUpdate()
    {

        // Tentukan rotasi yang diinginkan
        Quaternion desiredRotation = Quaternion.LookRotation( target.position - transform.position );
        // Interpolasi rotasi kamera untuk membuat gerakan lebih halus
        Quaternion smoothedRotation = Quaternion.Slerp( transform.rotation, desiredRotation, smoothSpeed );
        transform.rotation = smoothedRotation;

    }

}
