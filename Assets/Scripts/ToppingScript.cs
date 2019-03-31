using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToppingScript : MonoBehaviour, iPoolerObject
{
    private bool added = false;
    private bool touchingPizza = false;

    private Vector2 destination = new Vector2(0f,0f);

    private int rand;

    [SerializeField]
    private float flySpeed;

    public PlayerBehaviour.Players playerShooter;

    private void Start()
    {
        rand = Random.Range(0, 2);
    }
    

    // Update is called once per frame
    void Update () 
    {
        if (!added)
        {
            Vector2 launchPosition = Vector2.Lerp(transform.position, destination, flySpeed);
            Vector2 smoothedScale = Vector3.Lerp(transform.localScale, new Vector3(0.8f, 0.8f, 0.8f), flySpeed);

            RotateOverTime();

            transform.position = launchPosition;
            transform.localScale = smoothedScale;
        }

        //TODO: have the toppings fall/dissapear in some way after hitting the wall
        if (transform.localScale.x < 0.81f && !touchingPizza)
        {
            GetComponent<SpriteRenderer>().color = Color.grey;
            gameObject.tag = "Untagged";
            added = true;

            //TODO: Change layer of topping once it hits the wall
            //gameObject.SetActive(false);
        }
	}

    private void RotateOverTime()
    { 

        if(rand == 0)
        {
            transform.Rotate(0, 0, 4);
        }
        else if(rand == 1)
        {
            transform.Rotate(0, 0, -4);
        }
    }

    public void SetDestination(Vector3 desToSet)
    {
        destination = desToSet;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        touchingPizza = true;

        if (transform.localScale.x <= 0.85f && collision.gameObject.tag == "Pizza" && !added)
        {
            PizzaBehaviour pizzaHit = collision.GetComponent<PizzaBehaviour>();
            float distance = Vector2.Distance(transform.position, collision.transform.position);

            if ((pizzaHit.pizzaSize == PizzaBehaviour.PizzaSizes.Large && distance < 1.5f)
                || (pizzaHit.pizzaSize == PizzaBehaviour.PizzaSizes.Medium && distance < 1.0f)
                || (pizzaHit.pizzaSize == PizzaBehaviour.PizzaSizes.Small && distance < 0.75f))
            {
                Debug.Log("Hit Pizza");
                GetComponent<ParticleSystem>().Play();
                added = true;
                pizzaHit.AddTopping(gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pizza")
        {
            touchingPizza = false;
        }
    }



    public void OnSpawnedByPooler()
    {
        transform.parent = null;
        added = false;
    }
}
