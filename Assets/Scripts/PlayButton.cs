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
        SceneManager.LoadScene( 1 );

    }

}
