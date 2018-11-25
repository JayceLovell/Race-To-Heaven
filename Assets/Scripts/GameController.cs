using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    //public int PlayersConnected;
    //public int PlayersReady;
    public bool GameActive;
    public float Timer;
    public float Speed;

    private GameObject _startButton;
    private Text _txtamountOfPlayers;
    private bool _displayPlayers;
    private LevelGenerationScript _levelGenerationScript;

    public GameObject PrefabTestplayer;


    // Use this for initialization
    void Start () {
        //this code only here for non multiplayer
        Instantiate(PrefabTestplayer, new Vector3(0,0),Quaternion.identity);
        //resume
        Initilize();
        _displayPlayers = true;
        _startButton = GameObject.Find("btnStart");
        _txtamountOfPlayers = GameObject.Find("txtAmountOfPlayers").GetComponent<Text>();
        Speed = 3.5f;
    }

     void Initilize()
    {
        _levelGenerationScript = GetComponent<LevelGenerationScript>();
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
            IncreaseDiffculty();
        }
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
        _startButton.SetActive(false);
        _levelGenerationScript.ObstacleMaxWidth=8;
        _levelGenerationScript.ObstacleMinWidth=10;
    }
    void IncreaseDiffculty()
    {
        //if (isServer)
        //{
            Timer += Time.deltaTime;
            if (Timer >= 10)
            {
                Timer = 0;
                Speed++;
                _levelGenerationScript.ObstacleMaxWidth++;
                _levelGenerationScript.ObstacleMinWidth++;
            }
        //}
    }
}
