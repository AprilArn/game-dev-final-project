using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    
    private bool isOpen = false;
    private float duration = 0.5f;

    void Update()
    {
        
        if ( Input.GetKeyDown(KeyCode.R) )
        {

            if ( isOpen )
            {

                LeanTween.rotateX( gameObject, 0f, duration ).setEase( LeanTweenType.easeInOutQuad );

            }

            else
            {

                LeanTween.rotateX( gameObject, -30f, duration ).setEase( LeanTweenType.easeInOutQuad );

            }
            isOpen = !isOpen;

        }

    }

}
