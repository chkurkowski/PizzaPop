using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaBehaviour : MonoBehaviour
{
    private void OnMouseDown()
    {
        //Need functionality to find out who clicked ya
        //
    }

    public void AddTopping(GameObject g)
    {
        g.transform.parent = transform;
    }
}
