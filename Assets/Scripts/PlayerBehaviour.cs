using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour 
{
    public enum Players { Player1, Player2 };
    public Players player;


    [SerializeField]
    private GameObject cursorSprite;

    private Cursor cursor;

	// Use this for initialization
	void Start () 
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
	}
	
	void Update () 
    {
        Vector3 mousePos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0f);
        cursorSprite.transform.position = mousePos;
	}
}
