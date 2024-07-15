using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{

    public AudioSource clickSound;
    public void playButton()
    {
        
        clickSound.Play();
        StartCoroutine( delaySoundOnPlay() );
        print("The play button has been pressed, loading Game Field scene...");

    }

    public void exitButton()
    {
        
        clickSound.Play();
        StartCoroutine( delaySoundOnExit() );
        print("The exit button has been pressed, exiting Game...");

    }

    IEnumerator delaySoundOnPlay()
    {

        yield return new WaitForSeconds( 1.5f );
        SceneManager.LoadScene( 1 );
        Debug.Log("Game Played!");

    }

    IEnumerator delaySoundOnExit()
    {

        yield return new WaitForSeconds( 1.5f );
        Application.Quit();
        Debug.Log("Game Exit!");

    }

}
