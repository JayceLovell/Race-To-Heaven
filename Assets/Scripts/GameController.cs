using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    //public int PlayersConnected;
    //public int PlayersReady;
    public bool GameActive;
    public float Timer;
    public float Speed;
    public GameObject[] Players;

    private GameObject _readyButton;
    private Text _txtamountOfPlayers;
    private Text _txtClock;
    private bool _displayPlayers;
    private LevelGenerationScript _levelGenerationScript;
    private float _previousTime;
    private GameObject _fastfoward;

    public GameObject PrefabTestplayer;


    // Use this for initialization
    void Start () {
        //this code only here for non multiplayer
        Instantiate(PrefabTestplayer, new Vector3(0,0),Quaternion.identity);
        //resume
        Initilize();
        _displayPlayers = true;
        Speed = 3.5f;
        _fastfoward.SetActive(false);
    }

     void Initilize()
    {
        _levelGenerationScript = GetComponent<LevelGenerationScript>();
        _readyButton = GameObject.Find("btnReady");
        _txtamountOfPlayers = GameObject.Find("txtAmountOfPlayers").GetComponent<Text>();
        _txtClock = GameObject.Find("TxtClock").GetComponent<Text>();
        _fastfoward = GameObject.Find("FastFoward");
        //_networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
    }

    // Update is called once per frame
    void Update () {
        if (_displayPlayers)
        {
            /*if (isServer)
            {
                PlayersConnected = NetworkServer.connections.Count;
            }
            _txtamountOfPlayers.text = "Players Connected: " + PlayersConnected+"/4";*/
        }
        if (GameActive)
        {
            Timer += Time.deltaTime;
            IncreaseDiffculty(Timer);
            TxtClock(Timer);
        }
    }

    void TxtClock(float timer)
    {
        float minutes = Mathf.Floor(timer / 60);
        float seconds = timer % 60;
        _txtClock.text = "Time: " + minutes + ":" + Mathf.RoundToInt(seconds);
    }

    public void StartGame()
    {
        /*PlayersReady++;
        _startButton.SetActive(false);
        if (PlayersReady == PlayersConnected)
        {
            GameActive = true;
        }*/
        GameActive = true;
        _readyButton.SetActive(false);
        _levelGenerationScript.ObstacleMaxWidth=8;
        _levelGenerationScript.ObstacleMinWidth=10;
    }
    void IncreaseDiffculty(float _timer)
    {
        //if (isServer)
        //{
            if (_timer >= (_previousTime+10))
            {
            StartCoroutine(DisableFastfoward());
                _previousTime = _timer;
                Speed++;
                _levelGenerationScript.ObstacleMaxWidth++;
                _levelGenerationScript.ObstacleMinWidth++;
                Players = GameObject.FindGameObjectsWithTag("Player");
                foreach(var player in Players)
                {
                    player.GetComponent<Animator>().speed+=0.5f;
                }
            }
        //}
    }
    IEnumerator DisableFastfoward()
    {
        _fastfoward.SetActive(true);
        yield return new WaitForSeconds(2f);
        _fastfoward.SetActive(false);
    }
}
