using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaBehaviour : MonoBehaviour
{
    private ToppingSwitcher _toppingSwitcher;

    private void Start()
    {
        _toppingSwitcher = GameObject.Find("ToppingSwitcher").GetComponent<ToppingSwitcher>();
    }

    private void OnMouseDown()
    {
        AddTopping(_toppingSwitcher.GetPlayer1Topping());
    }

    public void AddTopping(ToppingSwitcher.Toppings toppingAdded)
    {
        //gets a string that tells us which topping to spawn onto it
        string toppingName = _toppingSwitcher.getToppingObject((int)toppingAdded);

        GameObject toppingToAdd = ObjectPooler.instance.SpawnFromPool(toppingName, new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0f), Quaternion.identity);

        toppingToAdd.transform.parent = transform;
    }
}
