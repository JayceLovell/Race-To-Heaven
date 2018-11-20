using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameController : NetworkBehaviour {

    [SyncVar] public int _playersConnected;

    private GameObject _startButton;
    private Text _txtamountOfPlayers;
    private bool _displayPlayers;
    private bool _gameActive;

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


    // Use this for initialization
    void Start () {
        _displayPlayers = true;
        _startButton = GameObject.Find("btnStart");
        _txtamountOfPlayers = GameObject.Find("txtAmountOfPlayers").GetComponent<Text>();
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
    }
    public void StartGame()
    {
        _startButton.SetActive(false);
    }
}
