﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObject : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this);
    }
}