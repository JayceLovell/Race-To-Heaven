using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerNameTextScript : NetworkBehaviour {
    [SyncVar(hook = "SetPlayerName")]
    public Text PlayerName;

    private GameManager _gameManager;

    // Use this for initialization
    void Start () {

        if (isLocalPlayer)
        {

        }
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        SetPlayerName();
    }
    void SetPlayerName()
    {
        PlayerName.text = _gameManager.PlayerName;
    }
}
