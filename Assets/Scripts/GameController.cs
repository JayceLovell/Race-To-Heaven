using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameController : NetworkBehaviour {

    [SyncVar(hook = "OnPlayersConnectedChange")]
    public int PlayersConnected;
    [SyncVar(hook = "OnPlayersReadyChange")]
    public int PlayersReady;
    [SyncVar(hook = "OnGameActiveChange")]
    public bool GameActive;
    [SyncVar(hook = "OnTimerChange")]
    public float Timer;
    [SyncVar(hook = "OnSpeedChange")] 
    public float Speed;

    private GameObject _startButton;
    private Text _txtamountOfPlayers;
    private bool _displayPlayers;
    private LevelGenerationScript _levelGenerationScript;
    private NetworkManager _networkManager;


    // Use this for initialization
    void Start () {
        Initilize();
        _displayPlayers = true;
        _startButton = GameObject.Find("btnStart");
        _txtamountOfPlayers = GameObject.Find("txtAmountOfPlayers").GetComponent<Text>();
        Speed = 3.5f;
    }

     void Initilize()
    {
        _levelGenerationScript = GetComponent<LevelGenerationScript>();
        _networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
    }

    // Update is called once per frame
    void Update () {
        if (_displayPlayers)
        {
            if (isServer)
            {
                PlayersConnected = NetworkServer.connections.Count;
            }
            _txtamountOfPlayers.text = "Players Connected: " + PlayersConnected+"/4";
        }
        if(GameActive)
        {
            IncreaseDiffculty();
        }
    }

    public void StartGame()
    {
        PlayersReady++;
        _startButton.SetActive(false);
        if (PlayersReady == PlayersConnected)
        {
            GameActive = true;
        }
    }
    void IncreaseDiffculty()
    {
        if (isServer)
        {
            Timer += Time.deltaTime;
            if (Timer >= 10)
            {
                Timer = 0;
                Speed++;
                _levelGenerationScript.ObstacleMaxWidth++;
                _levelGenerationScript.ObstacleMinWidth++;
            }
        }
    }
    void OnPlayersConnectedChange(int valueToChangeTo)
    {
        PlayersConnected = valueToChangeTo;
    }
    void OnPlayersReadyChange(int valueToChangeTo)
    {
        Debug.Log("Changing value");
        PlayersReady = valueToChangeTo;
    }
    void OnGameActiveChange(bool valueToChangeTo)
    {
        GameActive = valueToChangeTo;
    }
    void OnTimerChange(float valueToChangeTo)
    {
        Timer = valueToChangeTo;
    }
    void OnSpeedChange(float valueToChangeTo)
    {
        Speed = valueToChangeTo;
    }
}
