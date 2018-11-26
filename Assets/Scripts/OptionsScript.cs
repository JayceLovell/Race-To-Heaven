using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsScript : MonoBehaviour {

    //private NetworkManager _networkManager;

    public Button Chicken, MeatBoy, Miner, Placeholder, Angle;
    public GameObject[] CharacterPrefabs;

    // Use this for initialization
    void Start () {
        //_networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
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
        SceneManager.LoadScene("MainMenu");
    }
    void ChangePrefabToChoice(string _characterChocie)
    {
        /*switch (_characterChocie)
        {
            case "Chicken":
                _networkManager.playerPrefab = CharacterPrefabs[1];
                break;
            case "Miner":
                _networkManager.playerPrefab = CharacterPrefabs[1];
                break;
            case "Placeholder":
                _networkManager.playerPrefab = CharacterPrefabs[1];
                break;
            case "Angle":
                _networkManager.playerPrefab = CharacterPrefabs[1];
                break;
            case "Meatboy":
                _networkManager.playerPrefab = CharacterPrefabs[1];
                break;
            default:
                _networkManager.playerPrefab = CharacterPrefabs[0];
                break;
        }*/
    }

}
