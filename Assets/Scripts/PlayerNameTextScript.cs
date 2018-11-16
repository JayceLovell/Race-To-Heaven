using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerNameTextScript : NetworkBehaviour {

    [SyncVar] public string PlayerName;
    public Text PlayerNameText;

    private GameManager _gameManager;

    // Use this for initialization
    void Start () {

        if (isLocalPlayer)
        {

        }
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        PlayerName = _gameManager.PlayerName;
        CmdSetPlayerName(PlayerName);
    }
    [Command]
    void CmdSetPlayerName(string PlayerName)
    {
        PlayerNameText.text = PlayerName;
    }
}
