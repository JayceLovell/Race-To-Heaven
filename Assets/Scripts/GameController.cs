using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameController : NetworkBehaviour {

    [SyncVar] private int _playersConnected;
    [SyncVar] private int _playersReady;

    private GameObject _startButton;
    private Text _txtamountOfPlayers;
    private bool _displayPlayers;
    private bool _gameActive;
    private float _speed;
    private float _timer;
    private LevelGenerationScript _levelGenerationScript;

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
        _displayPlayers = true;
        _startButton = GameObject.Find("btnStart");
        _txtamountOfPlayers = GameObject.Find("txtAmountOfPlayers").GetComponent<Text>();
        _speed = 3.5f;
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
        }
    }
    public void StartGame()
    {
        _playersReady++;
        if(_playersReady == _playersConnected)
        {
            _startButton.SetActive(false);
            _gameActive = true;
        }
    }
    void IncreaseDiffculty()
    {
        _timer += Time.deltaTime;
        if (_timer >= 20)
        {
            _timer = 0;
            _speed++;
            _levelGenerationScript.ObstacleMaxWidth++;
            _levelGenerationScript.ObstacleMinWidth++;
        }
    }
}
