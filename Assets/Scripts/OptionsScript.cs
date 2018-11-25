using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsScript : MonoBehaviour {

    //private NetworkManager _networkManager;

    public Button Character1, Character2, Character3, Character4, Character5;
    public GameObject[] CharacterPrefabs;

    // Use this for initialization
    void Start () {
        //_networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        Character1.onClick.AddListener(delegate { ChangePrefabToChoice("Test"); });
        Character2.onClick.AddListener(delegate { ChangePrefabToChoice("Test"); });
        Character3.onClick.AddListener(delegate { ChangePrefabToChoice("Test"); });
        Character4.onClick.AddListener(delegate { ChangePrefabToChoice("Test"); });
        Character5.onClick.AddListener(delegate { ChangePrefabToChoice("Test"); });
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
    void ChangePrefabToChoice(string CharacterName)
    {
        //_networkManager.playerPrefab = CharacterPrefabs[0];
    }

}
