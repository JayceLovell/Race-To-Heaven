using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class MyNetworkManager : NetworkManager {
    /// <summary>
    /// Links for later 
    /// https://docs.unity3d.com/Manual/NetworkManagerCallbacks.html
    /// https://docs.unity3d.com/Manual/UNetManager.html?_ga=2.79407635.682158424.1544194853-1590010813.1536928597
    /// https://answers.unity.com/questions/1137966/onserveraddplayer-is-not-called.html
    /// https://docs.unity3d.com/2018.3/Documentation/Manual/UNetPlayersCustom.html
    /// https://docs.unity3d.com/2018.3/Documentation/Manual/UNetManager.html
    /// </summary>
    /// <param name="conn"></param>
    /// <param name="playerControllerId"></param>
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        Debug.Log("On Server Add Player Was called");
        var playerChoosenPrefab = GameObject.Find("GameManager").GetComponent<GameManager>().PlayerCharacterChoice;
        GameObject player = (GameObject)Instantiate(playerChoosenPrefab, Vector3.zero, Quaternion.identity);
        Debug.Log("Spawning");
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }
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
    public override void OnClientConnect(NetworkConnection conn)
    {
        Debug.Log("OnClientConnect");
        ClientScene.AddPlayer(conn, 0);
    }
    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        //Put this here so add player wouldn't be called twice
        //base.OnClientSceneChanged(conn);
        Debug.Log("On Client Scene Changed");
    }

}
