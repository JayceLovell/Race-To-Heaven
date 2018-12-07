using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyNetworkManager : NetworkManager {


    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        Debug.Log("Was called");
        var playerPrefab = GameObject.Find("GameManager").GetComponent<GameManager>().PlayerCharacterChoice;
        GameObject player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }
    public override void OnClientConnect(NetworkConnection conn)
    {
        Debug.Log("OnClientConnect");
        ClientScene.AddPlayer(conn, 0);
    }
    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        //Put this here so add player wouldn't be called twice
        //base.OnClientSceneChanged(conn);

        Debug.Log("Server triggered scene change and we've done the same, do any extra work here for the client...");

    }

}
