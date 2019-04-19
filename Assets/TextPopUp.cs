using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextPopUp : MonoBehaviour, iPoolerObject 
{
    public void OnSpawnedByPooler()
    {
        StartCoroutine(disappearTimer());
    }

    //this script will control size of the text popup for player feedback, as well as making it disapear after a second

    private IEnumerator disappearTimer()
    {
        yield return new WaitForSeconds(1.0f);

        gameObject.SetActive(false);
    }

    void Start () 
    {
        //RestrainPosition();
	}

    private void OnEnable()
    {
        //RestrainPosition();
    }

    // Update is called once per frame
    void Update () 
    {
		
	}

    public void RestrainPosition()
    {
        //Check X
        if (transform.position.x < -5)
        {
            transform.position = new Vector3(0f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > 5)
        {
            transform.position = new Vector3(0f, transform.position.y, transform.position.z);
        }

        if (transform.position.y < 5f)
        {
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        }
        else if (transform.position.y > 5f)
        {
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        }
    }
}
