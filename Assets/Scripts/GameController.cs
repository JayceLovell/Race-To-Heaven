using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameController : NetworkBehaviour {

    [SyncVar] public int PlayersConnected;
    [SyncVar(hook = "PlayersReadyIncrement")] public int PlayersReady;
    [SyncVar] public bool GameActive;
    public float Timer;
    public float Speed;
    public GameObject[] Players;
    public GameObject ReadyButton;

    private GameObject _readyButton;
    private Text _txtamountOfPlayers;
    private Text _txtClock;
    private bool _displayPlayers;
    private float _previousTime;
    private GameObject _fastfoward;

    //public GameObject PrefabTestplayer;


    // Use this for initialization
    void Start () {
        Initilize();
        _displayPlayers = true;
        Speed = 4f;
        _fastfoward.SetActive(false);
    }

     void Initilize()
    {
        _readyButton = GameObject.Find("btnReady");
        _txtamountOfPlayers = GameObject.Find("txtAmountOfPlayers").GetComponent<Text>();
        _txtClock = GameObject.Find("TxtClock").GetComponent<Text>();
        _fastfoward = GameObject.Find("FastFoward");
    }

    // Update is called once per frame
    void Update () {
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
        if (PlayersReady == PlayersConnected)
        {
            GameActive = true;
        }
        _txtamountOfPlayers.text = "Players Connected: " + PlayersConnected + "/4";
    }
    void PlayersReadyIncrement(int PlayersReadySoFar)
    {
        PlayersReady = PlayersReadySoFar;
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
        ReadyButton.SetActive(false);
    }
    IEnumerator DisableFastfoward()
    {
        _fastfoward.SetActive(true);
        yield return new WaitForSeconds(2f);
        _fastfoward.SetActive(false);
    }
}
