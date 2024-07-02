using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFigure8 : MonoBehaviour
{
    public float duration = 10.0f; // Durasi untuk satu putaran
    public float radius = 2.0f;   // Jari-jari lingkaran
    private float timer = 0.0f;

    void Update()
    {
        timer += Time.deltaTime;

        float t = (timer % duration) / duration * Mathf.PI * 2;
        
        // Menghitung posisi x dan z untuk membentuk angka 8 mendatar
        float x = radius * Mathf.Sin(t);
        float z = radius * Mathf.Sin(t) * Mathf.Cos(t);

        // Mengatur posisi kamera
        transform.position = new Vector3(x, transform.position.y, z);
    }
}
