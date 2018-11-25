using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LevelGenerationScript : MonoBehaviour {
    public GameObject cam;

    [Header("obstacles")]
    public GameObject[] obstaclePrefabs;
    public Transform obstavleInitialSpawnLoc;
    GameObject obstacle;
    public float _obstacleMinWidth;
    public float _obstacleMaxWidth;


    [Header("floors")]
    public GameObject floorPrefab;
    public GameObject floor;
    public float floorWidth;

    public float ObstacleMinWidth
    {
        get
        {
            return _obstacleMinWidth;
        }

        set
        {
            _obstacleMinWidth = value;
        }
    }

    public float ObstacleMaxWidth
    {
        get
        {
            return _obstacleMaxWidth;
        }

        set
        {
            _obstacleMaxWidth = value;
        }
    }

    /*[Header("backgrounds")]
    public GameObject backgroundPrefab;
    public GameObject background;
    public float backgroundWidth;*/

    void Start()
    {
        _obstacleMaxWidth = 15;
        _obstacleMinWidth = 10;
        obstacle = Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length - 1)], obstavleInitialSpawnLoc.position, Quaternion.identity);
        //NetworkServer.Spawn(obstacle);
    }
    private void FixedUpdate()
    {

        if (obstacle.transform.position.x < cam.transform.position.x + 50)
        {
            obstacle = Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length - 1)], obstacle.transform.position + new Vector3(Random.Range(ObstacleMinWidth, ObstacleMaxWidth), 0, 0), Quaternion.identity);
            //NetworkServer.Spawn(obstacle);
        }


        /*if (floor.transform.position.x < cam.transform.position.x + 50)
        {
            floor = Instantiate(floorPrefab, floor.transform.position + new Vector3(floorWidth, 0, 0), Quaternion.identity);
            NetworkServer.Spawn(floor);
        }*/


        //if (background.transform.position.x < cam.transform.position.x + 50)
        //    background = Instantiate(backgroundPrefab, background.transform.position + new Vector3(backgroundWidth, 0, 0), Quaternion.identity);
    }
}
