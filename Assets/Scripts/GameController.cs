using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameController : NetworkBehaviour {

    [SyncVar(hook = "OnPlayersConnectedChange")] public int PlayersConnected;
    [SyncVar] public int PlayersReady;
    [SyncVar(hook = "RpcGameActive")] public bool GameActive;
    public float Timer;
    public float Speed;
    public GameObject[] Players;

    private GameObject _readyButton;
    private Text _txtamountOfPlayers;
    private Text _txtClock;
    private bool _displayPlayers;
    private float _previousTime;
    private GameObject _fastfoward;

    public GameObject PrefabTestplayer;


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
    }
    public void OnPlayersConnectedChange(int players)
    {
        _txtamountOfPlayers.text = "Players Connected: " + players + "/4";
    }
    void TxtClock(float timer)
    {
        float minutes = Mathf.Floor(timer / 60);
        float seconds = timer % 60;
        _txtClock.text = "Time: " + minutes + ":" + Mathf.RoundToInt(seconds);
    }
    public void ReadyButtonClicked()
    {
        if (!isLocalPlayer)
        {
            // exit from update if this is not the local player
            return;
        }
        Debug.Log("Button Clicked");
        CmdReady();
    }
    [Command]
     void CmdReady()
    {
        Debug.Log("Command called");
        PlayersReady++;
        _readyButton.SetActive(false);
        if (PlayersReady == PlayersConnected)
        {
            Debug.Log("detect value change");
            GameActive = true;
        }
    }
    [ClientRpc]
    void RpcGameActive(bool GameActive)
    {
        Debug.Log("game set Active");
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
    IEnumerator DisableFastfoward()
    {
        _fastfoward.SetActive(true);
        yield return new WaitForSeconds(2f);
        _fastfoward.SetActive(false);
    }
}
