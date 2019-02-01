using UnityEngine;
using UnityEngine.UI;
using RavingBots.MultiInput;


public class PlayerBehaviour : MonoBehaviour 
{
    public float mouseSpeed;

    public enum Players { Player1, Player2 };
    public Players player;
    public Vector2 initalPosition;

    private RegisterPlayers _regPlayers;

    private InputState inputState;

    [SerializeField]
    private GameObject cursorSprite;

    private void Awake()
    {
        inputState = FindObjectOfType<InputState>();
        _regPlayers = FindObjectOfType<RegisterPlayers>();
    }

    //Two Device Objects
    //Two IVirtual Axis

    public IDevice player1Device;

    IVirtualAxis player1Axis;
    IVirtualAxis player2Axis;

    void Start () 
    {

    }
    
    void Update () 
    {
        //cursorSprite.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0f);
        Follow();


        //player1Device = inputState.FindFirstDown();
        //player1Axis = player1Device[InputCode.MouseLeft];

        if (_regPlayers.Axis1LeftClick() != null)
        {
            if (_regPlayers.Axis1LeftClick().IsDown && player == Players.Player1)
            {
                ToppingSwitcher.instance.SwitchPlayer1Topping(0);
                Shoot();
            }
        }

         if (_regPlayers.Axis1RightClick() != null)
        {
            if (_regPlayers.Axis1RightClick().IsDown && player == Players.Player1)
            {
                ToppingSwitcher.instance.SwitchPlayer1Topping(4);
                Shotgun();
            }
        }

        if (_regPlayers.Axis2LeftClick() != null)
        {
            if (_regPlayers.Axis2LeftClick().IsDown && player == Players.Player2)
            {
                ToppingSwitcher.instance.SwitchPlayer2Topping(0);
                Shoot();
            }
        }

         if (_regPlayers.Axis2RightClick() != null)
        {
            if (_regPlayers.Axis2RightClick().IsDown && player == Players.Player2)
            {
                ToppingSwitcher.instance.SwitchPlayer2Topping(4);
                Shotgun();
            }
        }

    }

    public void resetPosition()
    {
        transform.position = initalPosition;
    }

    void Follow()
    {
        RegisterPlayers players = _regPlayers;

        if (player == Players.Player1 && players.Mouse1() != null)
        {
            cursorSprite.transform.position += new Vector3(players.Mouse1()[InputCode.MouseX].Value, players.Mouse1()[InputCode.MouseY].Value, 0f) * Time.deltaTime * mouseSpeed;
        }
        else if (player == Players.Player2 && players.Mouse2() != null)
        {
            cursorSprite.transform.position += new Vector3(players.Mouse2()[InputCode.MouseX].Value, players.Mouse2()[InputCode.MouseY].Value, 0f) * Time.deltaTime * mouseSpeed;
        }
    }

    private void Shoot()
    {
        AudioManager.instance.Play("Shoot");
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
        AudioManager.instance.Play("Shoot");
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
        AudioManager.instance.Play("Shoot");
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
}
