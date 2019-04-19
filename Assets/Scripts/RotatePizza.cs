using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePizza : MonoBehaviour
{
    public float rotationSpeed;

    public void randomRotation()
    {
        int randomNum = Random.Range(0, 2);

        if (randomNum == 1)
        {
            rotationSpeed *= -1f;
        }

        rotationSpeed = Random.Range(-250f, 250f);
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0f, 0f, rotationSpeed * Time.deltaTime));
    }
}
