using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class flash : MonoBehaviour {

    public float flashRate;
    private TextMeshProUGUI menuText;

	// Use this for initialization
	void Start () 
    {
        menuText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(FlashText());
	}

    IEnumerator FlashText()
    {
        while(gameObject.activeSelf)
        {
            menuText.enabled = true;
            yield return new WaitForSeconds(flashRate + 1);
            menuText.enabled = false;
            yield return new WaitForSeconds(flashRate);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
