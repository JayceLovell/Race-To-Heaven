﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private string _levelChoice;
    private string _playerName;
    private NetworkManager _networkManager;

    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.

    [Header("Obsticles Prefabs")]
    public GameObject[] TestObsticles;
    public GameObject[] AetherObsticles;
    public GameObject[] LimboObstricles;
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

        //Loads game Settings
        GameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));
    }
    // Use this for initialization
    void Start() {
        _networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
    }

    // Update is called once per frame
    void Update() {

    }
    void PrepareNetWorkManager()
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
                break;
            case "Limbo":
                break;
        }
    }
    public void HostGame()
    {
        _networkManager.onlineScene = _levelChoice;
        PrepareNetWorkManager();
        _networkManager.StartHost();
    }
    public void JoinGame()
    {
            _networkManager.onlineScene = _levelChoice;
            PrepareNetWorkManager();
            _networkManager.StartClient();        
    }
    public void ChangedSettings()
    {
        GameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));
    }

}
