using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLight : MonoBehaviour
{

    public Light _Light;
    public float MinTime;
    public float MaxTime;
    public Material LightOnMaterial; // Material terang
    public Material LightOffMaterial; // Material gelap
    private float timer;
    private bool isLightOff;
    private Renderer _Renderer; // Renderer objek yang materialnya akan diubah

    // Start is called before the first frame update
    void Start()
    {

        timer = Random.Range( MinTime, MaxTime );
        isLightOff = false;
        _Renderer = GetComponent<Renderer>(); // Mendapatkan komponen Renderer dari objek

    }

    // Update is called once per frame
    void Update()
    {
        FlickeringLight();
    }

    void FlickeringLight()
    {

        if ( timer > 0 )
        {

            timer -= Time.deltaTime;

        }

        if ( timer <= 0 )
        {

            if ( _Light.enabled )
            {

                _Light.enabled = false;
                timer = 0.2f;
                isLightOff = true;
                _Renderer.material = LightOffMaterial; // Mengubah material menjadi gelap

            }

            else if ( isLightOff )
            {

                _Light.enabled = true;
                timer = Random.Range( MinTime, MaxTime );
                isLightOff = false;
                _Renderer.material = LightOnMaterial; // Mengubah material menjadi terang

            }

        }

    }

}
