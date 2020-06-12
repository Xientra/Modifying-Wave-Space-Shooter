using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{

    public float health;

    
    protected virtual void Start()
    {
        transform.LookAt(WaveController.player.transform.position);
    }

    /*
    protected virtual void Update()
    {
        
    }
    */
    
}
