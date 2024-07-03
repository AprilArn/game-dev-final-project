using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartOnTab : MonoBehaviour
{

    private bool isTabHeld = false;
    private float tabHoldTime = 0f;
    private float requiredHoldTime = 0.5f;

    void Update()
    {

        if ( Input.GetKey(KeyCode.Tab) )
        {

            if ( !isTabHeld )
            {

                isTabHeld = true;
                tabHoldTime = Time.time;

            }
            else if ( Time.time - tabHoldTime >= requiredHoldTime )
            {

                RestartScene();
                print("The restart button has been pressed, Restarting...");

            }
        }

        else
        {

            isTabHeld = false;

        }
    }

    void RestartScene()
    {

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene( currentScene.name );

    }

}