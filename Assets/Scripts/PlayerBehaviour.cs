using UnityEngine;
using UnityEngine.UI;


public class PlayerBehaviour : MonoBehaviour 
{
    public float mouseSpeed;

    public enum Players { Player1, Player2 };
    public Players player;
    public Vector2 initalPosition;

    private float shotTimer = 0;

    [SerializeField]
    private float pepperoniReloadTime;

    [SerializeField]
    private float pepperReloadTime;

    [SerializeField]
    private float mushroomReloadTime;

    [SerializeField]
    private float oliveReloadTime;

    [SerializeField]
    private float onionReloadTime;

    [SerializeField]
    private GameObject cursorSprite;

    private void Awake()
    {

    }

    //Two Device Objects
    //Two IVirtual Axis


    void Start () 
    {

    }
    
    void Update () 
    {

        SetReticleSprites();

        //cursorSprite.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0f);
        Follow();


        //player1Device = inputState.FindFirstDown();
        //player1Axis = player1Device[InputCode.MouseLeft];
        shotTimer += Time.deltaTime;

       // if (_regPlayers.Axis1LeftClick() != null)
        {
            if (Input.GetButton("P1Trigger") && player == Players.Player1)
            {

                if (ToppingSwitcher.instance.GetPlayer1Topping() == ToppingSwitcher.Toppings.Pepperoni
                    && shotTimer >= pepperoniReloadTime)
                {
                    AudioManager.instance.PlayWithRandomPitch("Pepparoni");
                    shotTimer = 0;
                    Shoot();
                }
                else if (ToppingSwitcher.instance.GetPlayer1Topping() == ToppingSwitcher.Toppings.GreenPepper
                    && shotTimer >= pepperReloadTime)
                {
                    AudioManager.instance.PlayWithRandomPitch("GreenPepper");
                    shotTimer = 0;
                    Shoot();
                }
                else if (ToppingSwitcher.instance.GetPlayer1Topping() == ToppingSwitcher.Toppings.Onion && shotTimer >= onionReloadTime)
                {
                    AudioManager.instance.PlayWithRandomPitch("Onion");
                    shotTimer = 0;
                    Arc();
                }
                else if (ToppingSwitcher.instance.GetPlayer1Topping() == ToppingSwitcher.Toppings.Mushroom && shotTimer >= mushroomReloadTime)
                {
                    AudioManager.instance.PlayWithRandomPitch("Mushroom");
                    shotTimer = 0;
                    Shoot();
                }
                else if (ToppingSwitcher.instance.GetPlayer1Topping() == ToppingSwitcher.Toppings.BlackOlive && shotTimer >= oliveReloadTime)
                {
                    AudioManager.instance.PlayWithRandomPitch("Olive");
                    shotTimer = 0;
                    Shotgun();
                }

            }
        }

        //if (_regPlayers.Axis2LeftClick() != null)
        {
            if (Input.GetButton("P2Trigger") && player == Players.Player2)
            {


                if (ToppingSwitcher.instance.GetPlayer2Topping() == ToppingSwitcher.Toppings.Pepperoni
                    && shotTimer >= pepperoniReloadTime)
                {
                    AudioManager.instance.PlayWithRandomPitch("Pepparoni");
                    shotTimer = 0;
                    Shoot();
                }
                else if (ToppingSwitcher.instance.GetPlayer2Topping() == ToppingSwitcher.Toppings.GreenPepper
                    && shotTimer >= pepperReloadTime)
                {
                    AudioManager.instance.PlayWithRandomPitch("GreenPepper");

                    shotTimer = 0;
                    Shoot();
                }
                else if (ToppingSwitcher.instance.GetPlayer2Topping() == ToppingSwitcher.Toppings.Onion && shotTimer >= onionReloadTime)
                {
                    AudioManager.instance.PlayWithRandomPitch("Onion");

                    shotTimer = 0;
                    Arc();
                }
                else if (ToppingSwitcher.instance.GetPlayer2Topping() == ToppingSwitcher.Toppings.Mushroom && shotTimer >= mushroomReloadTime)
                {
                    AudioManager.instance.PlayWithRandomPitch("Mushroom");

                    shotTimer = 0;
                    Shoot();
                }
                else if (ToppingSwitcher.instance.GetPlayer2Topping() == ToppingSwitcher.Toppings.BlackOlive && shotTimer >= oliveReloadTime)
                {
                    AudioManager.instance.PlayWithRandomPitch("Olive");

                    shotTimer = 0;
                    Shotgun();
                }

            }


        }


        // if (_regPlayers.Axis1RightClick() != null)
        //{
        //    if (_regPlayers.Axis1RightClick().IsDown && player == Players.Player1)
        //    {
        //        ToppingSwitcher.instance.SwitchPlayer1Topping(4);
        //        Shotgun();
        //    }
        //}

        //if (_regPlayers.Axis2LeftClick() != null)
        //{
        //    if (_regPlayers.Axis2LeftClick().IsDown && player == Players.Player2)
        //    {
        //        ToppingSwitcher.instance.SwitchPlayer2Topping(0);
        //        Shoot();
        //    }
        //}

        // if (_regPlayers.Axis2RightClick() != null)
        //{
        //    if (_regPlayers.Axis2RightClick().IsDown && player == Players.Player2)
        //    {
        //        ToppingSwitcher.instance.SwitchPlayer2Topping(4);
        //        Shotgun();
        //    }

        //}











    }

    private int GetToppingIndex()
    {
        if (player  == Players.Player1)
        {
            return (int)ToppingSwitcher.instance.GetPlayer1Topping();
        }
        else if (player == Players.Player2)
        {
            return (int)ToppingSwitcher.instance.GetPlayer2Topping();
        }

        return 0;
    }

    public void resetPosition()
    {
        transform.position = initalPosition;
    }

    void Follow()
    {


        if (player == Players.Player1)
        {
            //cursorSprite.transform.position += new Vector3(players.Mouse1()[InputCode.MouseX].Value, players.Mouse1()[InputCode.MouseY].Value, 0f) * Time.deltaTime * mouseSpeed;
            cursorSprite.transform.position = new Vector3((Input.GetAxis("P1Horizontal") * 7.27f), Input.GetAxis("P1Vertical") * 3.8f) ;
 
        }
        else if (player == Players.Player2)
        {
            //cursorSprite.transform.position += new Vector3(players.Mouse2()[InputCode.MouseX].Value, players.Mouse2()[InputCode.MouseY].Value, 0f) * Time.deltaTime * mouseSpeed;
            cursorSprite.transform.position = new Vector3((Input.GetAxis("P2Horizontal") * 7.27f), Input.GetAxis("P2Vertical") * 3.8f) ;
        }
        Debug.Log(Input.GetAxis("P1Horizontal"));
        Debug.Log(Input.GetAxis("P1Vertical"));
    }

    private void Shoot()
    {
        //AudioManager.instance.Play("Shoot");
        Vector3 ShotPos;
        GameObject bullet;

        if (player == Players.Player1)
        {
            ShotPos = new Vector3(-5f, -5f, 0f);
            bullet = ObjectPooler.instance.SpawnFromPool(ToppingSwitcher.instance.getPlayer1ToppingName(), ShotPos, Quaternion.identity);
        }
        else
        {
            ShotPos = new Vector3(5f, -5f, 0f);
            bullet = ObjectPooler.instance.SpawnFromPool(ToppingSwitcher.instance.getPlayer2ToppingName(), ShotPos, Quaternion.identity);
        }

        bullet.GetComponent<ToppingScript>().SetDestination(transform.position);
       
        bullet.GetComponent<ToppingScript>().playerShooter = player;

        bullet.transform.localScale = new Vector3(5f, 5f, 5f);

    }

    private void Shotgun()
    {
        Vector3 ShotPos;
        GameObject[] bullets = new GameObject[6];

        if (player == Players.Player1)
        {
            ShotPos = new Vector3(-5f, -5f, 0f);
            for(int i = 0; i < bullets.Length; i++)
            {
                bullets[i] = ObjectPooler.instance.SpawnFromPool(ToppingSwitcher.instance.getPlayer1ToppingName(), ShotPos, Quaternion.identity);
            }
        }
        else
        {
            ShotPos = new Vector3(5f, -5f, 0f);
            for (int i = 0; i < bullets.Length; i++)
            {
                bullets[i] = ObjectPooler.instance.SpawnFromPool(ToppingSwitcher.instance.getPlayer2ToppingName(), ShotPos, Quaternion.identity);
            }
        }

        for(int i = 0; i < bullets.Length; i++)
        {
            float offsetX = Random.Range(-2f, 2f);
            float offsetY = Random.Range(-2f, 2f);

            bullets[i].GetComponent<ToppingScript>().SetDestination(transform.position + new Vector3(offsetX, offsetY, 0));

            bullets[i].GetComponent<ToppingScript>().playerShooter = player;

            bullets[i].transform.localScale = new Vector3(5f, 5f, 5f);
        }
    }

    private void Arc()
    {
       // AudioManager.instance.Play("Shoot");
        Vector3 ShotPos;
        GameObject[] bullets = new GameObject[5];

        if (player == Players.Player1)
        {
            ShotPos = new Vector3(-5f, -5f, 0f);
            for (int i = 0; i < bullets.Length; i++)
            {
                bullets[i] = ObjectPooler.instance.SpawnFromPool(ToppingSwitcher.instance.getPlayer1ToppingName(), ShotPos, Quaternion.identity);
            }
        }
        else
        {
            ShotPos = new Vector3(5f, -5f, 0f);
            for (int i = 0; i < bullets.Length; i++)
            {
                bullets[i] = ObjectPooler.instance.SpawnFromPool(ToppingSwitcher.instance.getPlayer2ToppingName(), ShotPos, Quaternion.identity);
            }
        }

        for (int i = 0; i < bullets.Length; i++)
        {
            float offsetX = -.55f + (i * .5f); //Random.Range(-1f, 2f);
            float offsetY = -(offsetX * offsetX) + (offsetX); //This is for a parabola

            bullets[i].GetComponent<ToppingScript>().SetDestination(transform.position + new Vector3(offsetX, offsetY, 0));

            bullets[i].GetComponent<ToppingScript>().playerShooter = player;

            bullets[i].transform.localScale = new Vector3(5f, 5f, 5f);
        }
    }

    private void SetReticleSprites()
    {
        if (player == Players.Player1)
        {
            GetComponent<SpriteRenderer>().sprite = ToppingSwitcher.instance.player1CenterIcons[GetToppingIndex()];

        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = ToppingSwitcher.instance.player2CenterIcons[GetToppingIndex()];
        }

    }
}
