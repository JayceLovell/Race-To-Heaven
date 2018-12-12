using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private string _hostaddress;
    private string _levelChoice;
    private string _playerName;
    private NetworkManager _networkManager;
    private int _PlayerCharacterChoice;
    private bool _selectedCharacter;

    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.

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

    public int PlayerCharacterChoice
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

    public bool SelectedCharacter
    {
        get
        {
            return _selectedCharacter;
        }

        set
        {
            _selectedCharacter = value;
        }
    }

    public string Hostaddress
    {
        get
        {
            return _hostaddress;
        }

        set
        {
            _hostaddress = value;
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
        LoadSettings();
    }
    public void HostGame()
    {
        _networkManager.onlineScene = _levelChoice;
        _networkManager.StartHost();
    }
    public void JoinGame()
    {
        _networkManager.onlineScene = _levelChoice;
        if (_hostaddress != null)
        {
            _networkManager.networkAddress = _hostaddress;
        }
        _networkManager.StartClient();
    }
    public void ChangedSettings()
    {
        GameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));
    }
    void LoadSettings()
    {
        QualitySettings.antiAliasing = GameSettings.Antialiasing;
        QualitySettings.vSyncCount = GameSettings.VSync;
        QualitySettings.masterTextureLimit = GameSettings.TextureQuality;
        Screen.fullScreen = GameSettings.Fullscreen;
    }

}
