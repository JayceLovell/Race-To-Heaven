using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

    private string _playerName;
    private InputField _inputPlayerNameField;
    private GameManager _gameManager;
    private Dropdown _dropdownLevelSelector;
    private Text _version;
    private string _selectedlevel;
    public GameObject Settings;
    public AudioSource MainMenuMusic;

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


    // Use this for initialization
    void Start () {
        Initialize();
        _version.text = "Version :" + Application.version;
        _selectedlevel = "Test";
        MainMenuMusic.volume = _gameManager.GameSettings.MusicVolume;
	}

    void Initialize()
    {
        _inputPlayerNameField = GameObject.Find("InputPlayerName").GetComponent<InputField>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _dropdownLevelSelector = GameObject.Find("DropdownLevelSelector").GetComponent<Dropdown>();
        _version = GameObject.Find("txtVersion").GetComponent<Text>();
        MainMenuMusic = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        Settings.transform.Rotate(0,0,Time.deltaTime*5);
	}
    
    public void HostGame()
    {
        _gameManager.LevelChoice = _selectedlevel;
        _gameManager.PlayerName = PlayerName;
        _gameManager.HostGame();
    }
    public void JoinGame()
    {
        _gameManager.LevelChoice = _selectedlevel;
        _gameManager.PlayerName = PlayerName;
        _gameManager.JoinGame();
    }
    public void ClickExit()
    {
        Application.Quit();
    }
    public void ClickSettings()
    {
        SceneManager.LoadScene("Options");
    }
    public void ClickCharacterChange()
    {
        SceneManager.LoadScene("Character Selection", LoadSceneMode.Additive);
    }
    public void EnteredName()
    {
        PlayerName=_inputPlayerNameField.text;
    }
    public void LevelSelected()
    {
        //Debug.Log(_dropdownLevelSelector.value);
        switch(_dropdownLevelSelector.value)
        {
            case 0:
                _selectedlevel = "Test";
                break;
            case 1:
                _selectedlevel = "Aether";
                break;
            case 2:
                _selectedlevel = "Limbo";
                break;
            case 3:
                _selectedlevel = "Test";
                break;
        }
    }
}
