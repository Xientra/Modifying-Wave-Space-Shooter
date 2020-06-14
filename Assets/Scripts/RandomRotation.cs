using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var rnd = new System.Random();  
        m_axis.x = rnd.Next(1, 10);
        m_axis.y = rnd.Next(1, 10);
        m_axis.z = rnd.Next(1, 10);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(m_axis, m_angle, Space.Self);
    }

    Vector3 m_axis = default;
    public float m_angle = 10;
}
