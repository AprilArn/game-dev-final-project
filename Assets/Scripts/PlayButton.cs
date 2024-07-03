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
        StartCoroutine( delaySound() );
        print("The play button has been pressed, loading Game Field scene...");

    }

    IEnumerator delaySound()
    {

        yield return new WaitForSeconds( 1.5f );
        SceneManager.LoadScene( 1 );

    }

}
