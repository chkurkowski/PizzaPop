using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToppingScript : MonoBehaviour, iPoolerObject
{
    private bool added = false;

    private Vector2 destination = new Vector2(0f,0f);

    [SerializeField]
    private float flySpeed;

    public PlayerBehaviour.Players playerShooter;
	
	// Update is called once per frame
	void Update () 
    {
        if (!added)
        {
            Vector2 launchPosition = Vector2.Lerp(transform.position, destination, flySpeed);
            Vector2 smoothedScale = Vector3.Lerp(transform.localScale, new Vector3(0.8f, 0.8f, 0.8f), flySpeed);

            transform.position = launchPosition;
            transform.localScale = smoothedScale;
        }

        //TODO: have the toppings fall/dissapear in some way after hitting the wall
        if (transform.localScale.x < 0.9f)
        {
            gameObject.SetActive(false);
        }
	}

    public void SetDestination(Vector3 desToSet)
    {
        destination = desToSet;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (transform.localScale.x < 1.0f && collision.gameObject.tag == "Pizza" && !added)
        {
            PizzaBehaviour pizzaHit = collision.GetComponent<PizzaBehaviour>();
            float distance = Vector2.Distance(transform.position, collision.transform.position);

            if ((pizzaHit.pizzaSize == PizzaBehaviour.PizzaSizes.Large && distance < 1.5f)
                || (pizzaHit.pizzaSize == PizzaBehaviour.PizzaSizes.Medium && distance < 1.0f)
                || (pizzaHit.pizzaSize == PizzaBehaviour.PizzaSizes.Small && distance < 0.75f))
            {
                Debug.Log("Hit Pizza");
                added = true;
                pizzaHit.AddTopping(gameObject);
            }

        }
    }

    public void OnSpawnedByPooler()
    {
        added = false;
    }
}
