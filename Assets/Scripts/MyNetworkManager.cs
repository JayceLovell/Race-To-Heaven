using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class MyNetworkManager : NetworkManager {

    public int curPlayer;

    //Called on client when connect
    public override void OnClientConnect(NetworkConnection conn)
    {
        Debug.Log("OnClientConnect");
        curPlayer = GameObject.Find("GameManager").GetComponent<GameManager>().PlayerCharacterChoice;

        // Create message to set the player
        IntegerMessage msg = new IntegerMessage(curPlayer);

        // Call Add player and pass the message
        ClientScene.AddPlayer(conn, 0, msg);
    }

    // Server
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        Debug.Log("On Server Add Player Was called");
        // Read client message and receive index

        var stream = extraMessageReader.ReadMessage<IntegerMessage>();
            curPlayer = stream.value;

        //Select the prefab from the spawnable objects list
        var playerPrefab = spawnPrefabs[curPlayer];

        // Create player object with prefab
        GameObject player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        Debug.Log("Spawning");
        // Add player object for connection
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }
    /*
        public override void OnClientConnect(NetworkConnection conn)
    {
        Debug.Log("OnClientConnect");
        ClientScene.AddPlayer(conn, 0);
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        Debug.Log("On Server Add Player Was called");
        var playerChoosenPrefab = GameObject.Find("GameManager").GetComponent<GameManager>().PlayerCharacterChoice;
        GameObject player = (GameObject)Instantiate(playerChoosenPrefab, Vector3.zero, Quaternion.identity);
        Debug.Log("Spawning");
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }
    */
    public override void OnServerSceneChanged(string sceneName)
    {
        Debug.Log("Server Scene Changed");
        //base.OnServerSceneChanged(sceneName);
    }
    public override void OnServerConnect(NetworkConnection conn)
    {
        Debug.Log("OnServerConnect");
        //base.OnServerConnect(conn);
    }

    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        //Put this here so add player wouldn't be called twice
        //base.OnClientSceneChanged(conn);
        Debug.Log("On Client Scene Changed");
    }

}
