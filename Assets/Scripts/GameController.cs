using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameController : NetworkBehaviour {

    [SyncVar] public int PlayersConnected;
    [SyncVar] public int PlayersReady;
    [SyncVar] public bool GameActive;
    [SyncVar] public int PlayersAlive;
    [SyncVar] public float Timer;
    [SyncVar] public float Speed;
    public GameObject[] Players;
    public List<GameObject> objects;

    private Text _txtamountOfPlayers;
    private Text _txtClock;
    private float _previousTime;
    private GameObject _fastfoward;
    private bool _playingWinner;  
    private NetworkManager _networkManager;
    private GameManager _gameManager;

    public AudioSource aSource;
    public AudioClip[] aClips;



    // Use this for initialization
    void Start () {
        Initilize();
        Speed = 4f;
        _fastfoward.SetActive(false);
    }

     void Initilize()
    {
        aSource = GetComponent<AudioSource>();
        aSource.clip = aClips[0];
        aSource.Play();
        _txtamountOfPlayers = GameObject.Find("txtAmountOfPlayers").GetComponent<Text>();
        _txtClock = GameObject.Find("TxtClock").GetComponent<Text>();
        _fastfoward = GameObject.Find("FastFoward");
        _networkManager = GameObject.Find("NetworkManager").GetComponent<MyNetworkManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        aSource.volume = _gameManager.GameSettings.MusicVolume;
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
                RpcIncreaseDiffculty(Timer);
                TxtClock(Timer);
            }
            if ((PlayersReady >= PlayersConnected) && !GameActive && !_playingWinner)
            {
                GameActive = true;
                PlayersAlive = PlayersConnected;
                _networkManager.maxConnections = PlayersConnected;
            }
            if (PlayersAlive == 1 && GameActive && !_playingWinner)
            {
                objects.Add(GameObject.Find("SpawnSet 1"));
                objects.Add(GameObject.Find("SpawnSet 2"));
                //objects.AddRange(GameObject.FindGameObjectsWithTag("spawner"));
                objects.AddRange(GameObject.FindGameObjectsWithTag("Obsticle3"));
                objects.AddRange(GameObject.FindGameObjectsWithTag("Obsticle2"));
                objects.AddRange(GameObject.FindGameObjectsWithTag("Obsticle1"));
                foreach (var Object in objects)
                {
                    NetworkServer.Destroy(Object);
                }
                GameActive = false;
                Debug.Log("Calling Winner Client RPC");
                RpcWinner();
            }
        }
        if (!GameActive && !_playingWinner)
        {
            _txtamountOfPlayers.text = "Players Connected: " + PlayersConnected + "/4";
            Players = GameObject.FindGameObjectsWithTag("Player");
        }
        else
        {
            _txtamountOfPlayers.text = "Players Live: " + PlayersAlive + "/" + PlayersConnected;
        }
    }
    public void CheckIfPlayersReady()
    {
        if (PlayersConnected >= 2 && !GameActive)
        {
            Players = GameObject.FindGameObjectsWithTag("Player");
            foreach (var player in Players)
            {
                if (player.GetComponent<PlayerController>().PlayerReady)
                {
                    PlayersReady++;
                }
            }
        }
    }
    void TxtClock(float timer)
    {
        float minutes = Mathf.Floor(timer / 60);
        float seconds = timer % 60;
        _txtClock.text = "Time: " + minutes + ":" + Mathf.RoundToInt(seconds);
    }
    [ClientRpc]
    void RpcIncreaseDiffculty(float _timer)
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
    [ClientRpc]
    void RpcWinner()
    {
        Debug.Log("Inside Winner Client RPC");
        //Write code to find winner player
        var PlayerLeft = GameObject.FindGameObjectWithTag("Player");
        PlayerLeft.GetComponent<PlayerController>().Winner();
        _playingWinner = true;
        StartCoroutine(CountDownToEndGame());
        if (aSource.clip != aClips[1])
        {
            aSource.Stop();
            aSource.clip = aClips[1];
            aSource.Play();
        }

    }
    [Command]
    public void CmdPlayerDead()
    {
        PlayersAlive = PlayersAlive-1;
    }

    IEnumerator DisableFastfoward()
    {
        _fastfoward.SetActive(true);
        yield return new WaitForSeconds(2f);
        _fastfoward.SetActive(false);
    }
    IEnumerator CountDownToEndGame()
    {

        Debug.Log("Now to Count 10 sec");
        yield return new WaitForSeconds(10f);
        Debug.Log("Finish Count");
        if (isServer)
        {
            Debug.Log("Stopping Server");
            MyNetworkManager.singleton.StopServer();
        }
        else if(isClient){
            Debug.Log("Stopping Client");
            MyNetworkManager.singleton.StopClient();
        }
    }
}
