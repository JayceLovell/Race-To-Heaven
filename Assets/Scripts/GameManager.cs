using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour {
    private string _levelChoice;
    private string _playerName;
    private NetworkManager _networkManager;

    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
    public GameObject playertest;

    public string LevelChoice
    {
        get
        {
            return _levelChoice;
        }

        set
        {
            _levelChoice = value;
        }
    }

    public string PlayerName
    {
        get
        {
            return _playerName;
        }

        set
        {
            _playerName = value;
        }
    }

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }
        // Use this for initialization
    void Start () {
        _networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void HostGame()
    {
        _networkManager.onlineScene = "Test";
        //_networkManager.onlineScene = _levelChoice;
        _networkManager.StartHost();
    }
    public void JoinGame()
    {
        _networkManager.StartClient();
    }
}
