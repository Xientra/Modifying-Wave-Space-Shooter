using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Set sprite transparent
        // ----------------------
        Color color = text.color;
        color.a = 0;
        text.color = color;

        startPosition = transform.position;

        // Destroy
        // -------
        Destroy(this.gameObject, time);
    }

    // Update is called once per frame
    void Update()
    {
        // Compute distance to startPosition
        // ---------------------------------
        float dist = (transform.position - startPosition).magnitude;

        // Translate until distance is reached
        // -----------------------------------
        if (dist < distance)
        {
            // Move upwards slowly
            // -------------------
            transform.Translate(-transform.forward * Time.deltaTime * m_speed, Space.Self);
        }

        // Slowly increase sprite transparency
        // -----------------------------------
        Color color = text.color;
        color.a += Mathf.Clamp(transparency, 0, 1);
        transparency += Time.deltaTime * transparencySpeed;
        text.color = color;
    }

    public float m_speed = 1;
    public float transparencySpeed = 0.2f;
    public TMP_Text text = null;
    public float distance = 1;
    public float time = 1;
    
    private float transparency = 0;
    private Vector3 startPosition = default;
    //private bool once = true;
}
