using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private string _levelChoice;
    private string _playerName;
    //private NetworkManager _networkManager;

    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.

    [Header("Obsticles Prefabs")]
    public GameObject TestObstible;
    public GameObject SmallLevel;
    public GameObject MediumLevel;
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
    void Start() {
        //_networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
    }

    // Update is called once per frame
    void Update() {

    }
    void PrepareNetWorkManager()
    {
        switch (_levelChoice)
        {
            case "Test":
                //_networkManager.spawnPrefabs.Add(TestObstible);
                //ClientScene.RegisterPrefab(TestObstible);
                break;
            case "Aether":
                break;
            case "Limbo":
                break;
        }
    }
    public void HostGame()
    {
        //_networkManager.onlineScene = _levelChoice;
        // _networkManager.StartHost();
        SceneManager.LoadScene(_levelChoice);
        PrepareNetWorkManager();
    }
    public void JoinGame()
    {
        //_networkManager.onlineScene = _levelChoice;
        //_networkManager.StartClient();
        PrepareNetWorkManager();
    }

}
