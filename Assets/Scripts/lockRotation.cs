﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockRotation : MonoBehaviour 
{
    public void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
    }
}
