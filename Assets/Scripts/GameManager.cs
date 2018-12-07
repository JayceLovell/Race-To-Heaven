using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private string _levelChoice;
    private string _playerName;
    private NetworkManager _networkManager;
    private GameObject _PlayerCharacterChoice;

    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.

    [Header("Obsticles Prefabs")]
    public GameObject[] TestObsticles;
    public GameObject[] AetherObsticles;
    public GameObject[] LimboObsticles;
    public GameSettings GameSettings;
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

    public GameObject PlayerCharacterChoice
    {
        get
        {
            return _PlayerCharacterChoice;
        }

        set
        {
            _PlayerCharacterChoice = value;
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
        if(System.IO.File.Exists(Application.persistentDataPath + "/gamesettings.json"))
        {
            //Loads game Settings
            GameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));
        }
        else
        {
            //Create File
            Debug.Log("Creating file");
            GameSettings = new GameSettings();
            GameSettings.MusicVolume = 50f;
            string jsonData = JsonUtility.ToJson(GameSettings, true);
            File.WriteAllText(Application.persistentDataPath + "/gamesettings.json", jsonData);
        }
    }
    // Use this for initialization
    void Start() {
        _networkManager = GameObject.Find("NetworkManager").GetComponent<MyNetworkManager>();
    }

    // Update is called once per frame
    void Update() {

    }
    private void _prepareNetWorkManager()
    {
        switch (_levelChoice)
        {
            case "Test":
                foreach(var prefab in TestObsticles)
                {
                    _networkManager.spawnPrefabs.Add(prefab);
                    ClientScene.RegisterPrefab(prefab);
                }
                break;
            case "Aether":
                foreach (var prefab in AetherObsticles)
                {
                    _networkManager.spawnPrefabs.Add(prefab);
                    ClientScene.RegisterPrefab(prefab);
                }
                break;
            case "Limbo":
                foreach (var prefab in LimboObsticles)
                {
                    _networkManager.spawnPrefabs.Add(prefab);
                    ClientScene.RegisterPrefab(prefab);
                }
                break;
        }
    }
    public void HostGame()
    {
        _networkManager.onlineScene = _levelChoice;
        _prepareNetWorkManager();
        _networkManager.StartHost();
    }
    public void JoinGame()
    {
        _prepareNetWorkManager();
        _networkManager.onlineScene = _levelChoice;
        _networkManager.StartClient();        
    }
    public void ChangedSettings()
    {
        GameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));
    }

}
