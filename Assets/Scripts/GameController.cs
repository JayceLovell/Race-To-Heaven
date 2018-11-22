using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameController : NetworkBehaviour {

    [SyncVar] private int _playersConnected;
    [SyncVar] private int _playersReady;
    [SyncVar] private float _timer;
    [SyncVar] private float _speed;
    [SyncVar] private bool _gameActive;

    private GameObject _startButton;
    private Text _txtamountOfPlayers;
    private bool _displayPlayers;
    private LevelGenerationScript _levelGenerationScript;
    private NetworkManager _networkManager;

    public bool GameActive
    {
        get
        {
            return _gameActive;
        }

        set
        {
            _gameActive = value;
        }
    }

    public float Speed
    {
        get
        {
            return _speed;
        }

        set
        {
            _speed = value;
        }
    }


    // Use this for initialization
    void Start () {
        Initilize();
        _displayPlayers = true;
        _startButton = GameObject.Find("btnStart");
        _txtamountOfPlayers = GameObject.Find("txtAmountOfPlayers").GetComponent<Text>();
        _speed = 3.5f;
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
                _playersConnected = NetworkServer.connections.Count;
            }
            _txtamountOfPlayers.text = "Players Connected: " + _playersConnected+"/4";
        }
        if(_gameActive)
        {
            IncreaseDiffculty();
            _startButton.SetActive(false);
        }
    }
    public void StartGame()
    {
        _playersReady++;
        if(_playersReady == _playersConnected)
        {
            _gameActive = true;
        }
    }
    void IncreaseDiffculty()
    {
        if (isServer)
        {
            _timer += Time.deltaTime;
            if (_timer >= 10)
            {
                _timer = 0;
                _speed++;
                _levelGenerationScript.ObstacleMaxWidth++;
                _levelGenerationScript.ObstacleMinWidth++;
            }
        }
    }
}
