using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PizzaToLaunch
{
    public Vector2 startingPosition;
    public Vector2 forceToPush;
    public PizzaBehaviour.PizzaSizes size;

}

[System.Serializable]
public class ScriptedPizzaEvent 
{
    public PizzaToLaunch[] pizzas;

    public void launch()
    {
        foreach(PizzaToLaunch p in pizzas)
        {
            string pizzaObject;

            if (p.size == PizzaBehaviour.PizzaSizes.Small)
            {
                pizzaObject = "SmallPizza";
            }
            else if (p.size == PizzaBehaviour.PizzaSizes.Medium)
            {
                pizzaObject = "MediumPizza";
            }
            else
            {
                pizzaObject = "LargePizza";
            }

            GameObject g = ObjectPooler.instance.SpawnFromPool(pizzaObject, p.startingPosition, Quaternion.identity);

            g.GetComponent<Rigidbody2D>().AddForce(p.forceToPush, ForceMode2D.Impulse);
        }
    }
}
