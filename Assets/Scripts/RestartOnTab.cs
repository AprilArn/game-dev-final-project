// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class RestartOnTab : MonoBehaviour
// {

//     private bool isTabHeld = false;
//     private float tabHoldTime = 0f;
//     private float requiredHoldTime = 0.5f;

//     void Update()
//     {

//         if ( Input.GetKey(KeyCode.Tab) )
//         {

//             if ( !isTabHeld )
//             {

//                 isTabHeld = true;
//                 tabHoldTime = Time.time;

//             }
//             else if ( Time.time - tabHoldTime >= requiredHoldTime )
//             {

//                 RestartScene();
//                 print("The restart button has been pressed, Restarting...");

//             }
//         }

//         else
//         {

//             isTabHeld = false;

//         }
//     }

//     void RestartScene()
//     {

//         Scene currentScene = SceneManager.GetActiveScene();
//         SceneManager.LoadScene( currentScene.name );

//     }

// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartOnTab : MonoBehaviour
{
    private bool isTabHeld = false;
    private float tabHoldTime = 0f;

    private bool isEscHeld = false;
    private float escHoldTime = 0f;

    private float requiredHoldTime = 0.5f;

    void Update()
    {
        // Check for Tab key
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
                print("The restart button has been pressed, restarting this scene...");

            }

        }

        else
        {

            isTabHeld = false;

        }

        // Check for Esc key
        if ( Input.GetKey(KeyCode.Escape) )
        {
            if ( !isEscHeld )
            {

                isEscHeld = true;
                escHoldTime = Time.time;

            }

            else if ( Time.time - escHoldTime >= requiredHoldTime )
            {

                GoToScene0();
                print("The escape button has been pressed, going to Main Menu scene...");

            }

        }

        else
        {

            isEscHeld = false;

        }

    }

    void RestartScene()
    {

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene( currentScene.name );

    }

    void GoToScene0()
    {

        SceneManager.LoadScene( 0 );

    }
    
}
