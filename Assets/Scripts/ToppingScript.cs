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
	}

    public void SetDestination(Vector3 desToSet)
    {
        destination = desToSet;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (transform.localScale.x < 1.1f && collision.gameObject.tag == "Pizza" && !added && Vector2.Distance(transform.position, collision.transform.position) < 1.5f)
        {
            Debug.Log("Hit Pizza");
            added = true;
            collision.gameObject.GetComponent<PizzaBehaviour>().AddTopping(gameObject);
        }
    }

    public void OnSpawnedByPooler()
    {
        added = false;
    }
}
