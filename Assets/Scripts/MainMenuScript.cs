using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {

    private string _playerName;
    private InputField _inputPlayerNameField;
    private GameManager _gameManager;
    private Dropdown _dropdownLevelSelector;
    private string _selectedlevel;

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
        _inputPlayerNameField = GameObject.Find("InputPlayerName").GetComponent<InputField>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _dropdownLevelSelector = GameObject.Find("DropdownLevelSelector").GetComponent<Dropdown>();
	}
	
	// Update is called once per frame
	void Update () {
        
	}
    
    public void HostGame()
    {
        _gameManager.LevelChoice = _selectedlevel;
        _gameManager.PlayerName = PlayerName;
        _gameManager.HostGame();
    }
    public void JoinGame()
    {
        _gameManager.PlayerName = PlayerName;
        _gameManager.JoinGame();
    }
    public void ClickExit()
    {
        Application.Quit();
    }
    public void EnteredName()
    {
        PlayerName=_inputPlayerNameField.text;
    }
    public void LevelSelected()
    {
        switch(_dropdownLevelSelector.value)
        {
            case 2:
                _selectedlevel = "Small Level";
                break;
            case 3:
                _selectedlevel = "Medium Level";
                break;
        }
    }
}
