using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class OptionsScript : MonoBehaviour {

    private NetworkManager _networkManager;

    // Use this for initialization
    void Start () {
        _networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
