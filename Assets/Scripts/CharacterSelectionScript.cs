using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelectionScript : MonoBehaviour {

    //private NetworkManager _networkManager;
    private GameManager _gameManager;

    public Button Chicken, MeatBoy, Miner, Placeholder, Angle;
    public GameObject[] CharacterPrefabs;

    // Use this for initialization
    void Start () {
        //_networkManager = GameObject.Find("NetworkManager").GetComponent<MyNetworkManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Chicken.onClick.AddListener(delegate { ChangePrefabToChoice("Chicken"); });
        MeatBoy.onClick.AddListener(delegate { ChangePrefabToChoice("MeatBoy"); });
        Miner.onClick.AddListener(delegate { ChangePrefabToChoice("Miner"); });
        Placeholder.onClick.AddListener(delegate { ChangePrefabToChoice("Placeholder"); });
        Angle.onClick.AddListener(delegate { ChangePrefabToChoice("Angle"); });
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Back()
    {
        //SceneManager.LoadScene("Main Menu");
        SceneManager.UnloadSceneAsync("Character Selection");
    }
    void ChangePrefabToChoice(string _characterChocie)
    {
        switch (_characterChocie)
        {
            case "Chicken":
                _gameManager.PlayerCharacterChoice = 0;
                //_networkManager.playerPrefab = CharacterPrefabs[1];
                Debug.Log("Choose Chicken");
                break;
            case "Miner":
                _gameManager.PlayerCharacterChoice = 2;
                //_networkManager.playerPrefab = CharacterPrefabs[2];
                Debug.Log("Choose Miner");
                break;
            case "Placeholder":
                //_networkManager.playerPrefab = CharacterPrefabs[4];
                _gameManager.PlayerCharacterChoice = 3;
                Debug.Log("Choose Placeholder");
                break;
            case "MeatBoy":
                _gameManager.PlayerCharacterChoice = 1;
                // _networkManager.playerPrefab = CharacterPrefabs[3];
                Debug.Log("Choose MeatBoy");
                break;
            //case "Angle":
            //_networkManager.playerPrefab = CharacterPrefabs[1];
            //Debug.Log("Choose Angle");
            //break;
            default:
                _gameManager.PlayerCharacterChoice = 4;
                //_networkManager.playerPrefab = CharacterPrefabs[0];
                Debug.Log("Choose Test");
                break;
        }
        _gameManager.SelectedCharacter = true;
        Back();
    }

}
