using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public void RestartGame()
    {

        SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex );
        print("Restart button is clicked...");

    }
    

}
