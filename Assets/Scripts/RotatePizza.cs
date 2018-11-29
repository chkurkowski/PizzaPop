using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePizza : MonoBehaviour
{
    public float rotationSpeed;

    private void Update()
    {
        transform.Rotate(new Vector3(0f, 0f, rotationSpeed * Time.deltaTime));
    }
}
