﻿using System.Collections;
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
		
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}
}
