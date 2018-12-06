using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameController : NetworkBehaviour {

    [SyncVar] public int PlayersConnected;
    [SyncVar] public int PlayersReady;
    [SyncVar] public bool GameActive;
    [SyncVar] public int PlayersAlive;
    public float Timer;
    public float Speed;
    public GameObject[] Players;

    private Text _txtamountOfPlayers;
    private Text _txtClock;
    private float _previousTime;
    private GameObject _fastfoward;
    private bool _playingWinner;


    // Use this for initialization
    void Start () {
        Initilize();
        Speed = 4f;
        _fastfoward.SetActive(false);
    }

     void Initilize()
    {
        _txtamountOfPlayers = GameObject.Find("txtAmountOfPlayers").GetComponent<Text>();
        _txtClock = GameObject.Find("TxtClock").GetComponent<Text>();
        _fastfoward = GameObject.Find("FastFoward");
    }

    // Update is called once per frame
    void Update () {
        //Players connected link
        //https://answers.unity.com/questions/1259697/how-to-display-connections-count-on-client.html
        if (isServer)
        {
            PlayersConnected = NetworkServer.connections.Count;

            if (GameActive)
            {
                Timer += Time.deltaTime;
                IncreaseDiffculty(Timer);
                TxtClock(Timer);
            }
        }
        if ((PlayersReady == PlayersConnected) && !GameActive )
        {
            //Debug.Log("Calling Command RPC");
            GameActive = true;
            PlayersAlive = PlayersConnected;
            //RpcStartGame();
        }
        if (!GameActive)
        {
            _txtamountOfPlayers.text = "Players Connected: " + PlayersConnected + "/4";
        }
        else
        {
            _txtamountOfPlayers.text = "Players Live: " + PlayersAlive + "/" + PlayersConnected;
        }
        if (PlayersAlive == 1 && GameActive && !_playingWinner)
        {
            //Write code to find winner player
            GameActive = false;
            var obsticles2 = GameObject.FindGameObjectsWithTag("Obsticle2");
            var obsticles1 = GameObject.FindGameObjectsWithTag("Obsticle1");
            foreach(var obsticle in obsticles1)
            {
                NetworkServer.Destroy(obsticle);
            }
            foreach (var obsticle in obsticles2)
            {
                NetworkServer.Destroy(obsticle);
            }
            var PlayerLeft = GameObject.FindGameObjectWithTag("Player");
            PlayerLeft.GetComponent<PlayerController>().Winner();
            _playingWinner = true;
        }
    }
    void TxtClock(float timer)
    {
        float minutes = Mathf.Floor(timer / 60);
        float seconds = timer % 60;
        _txtClock.text = "Time: " + minutes + ":" + Mathf.RoundToInt(seconds);
    }
    void IncreaseDiffculty(float _timer)
    {
            if (_timer >= (_previousTime+10))
            {
            StartCoroutine(DisableFastfoward());
                _previousTime = _timer;
                Speed++;
                Players = GameObject.FindGameObjectsWithTag("Player");
                foreach(var player in Players)
                {
                    player.GetComponent<Animator>().speed+=0.5f;
                    player.GetComponent<Rigidbody2D>().gravityScale+=0.5f;
                    player.GetComponent<PlayerController>().JumpForce++;
                }
            }
    }
    
    public void PlayerReady()
    {
        PlayersReady++;
        Debug.Log("Done");
    }
    public void PlayerDead()
    {
        PlayersAlive--;
    }

    IEnumerator DisableFastfoward()
    {
        _fastfoward.SetActive(true);
        yield return new WaitForSeconds(2f);
        _fastfoward.SetActive(false);
    }
}
