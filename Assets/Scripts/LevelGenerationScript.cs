using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LevelGenerationScript : NetworkBehaviour {
    public GameObject cam;

    [Header("obstacles")]
    public GameObject[] obstaclePrefabs;
    public Transform obstavleInitialSpawnLoc;
    GameObject obstacle;
    public float ObstacleMinWidth;
    public float ObstacleMaxWidth;


    [Header("floors")]
    public GameObject floorPrefab;
    public GameObject floor;
    public float floorWidth;

    /*[Header("backgrounds")]
    public GameObject backgroundPrefab;
    public GameObject background;
    public float backgroundWidth;*/

    void Start()
    {
        obstacle = Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length - 1)], obstavleInitialSpawnLoc.position, Quaternion.identity);
    }
    private void FixedUpdate()
    {
        if (obstacle.transform.position.x < cam.transform.position.x + 50)
        {
            obstacle = Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length - 1)], obstacle.transform.position + new Vector3(Random.Range(ObstacleMinWidth, ObstacleMaxWidth), 0, 0), Quaternion.identity);
        }
            

        if (floor.transform.position.x < cam.transform.position.x + 50)
            floor = Instantiate(floorPrefab, floor.transform.position + new Vector3(floorWidth, 0, 0), Quaternion.identity);

        //if (background.transform.position.x < cam.transform.position.x + 50)
        //    background = Instantiate(backgroundPrefab, background.transform.position + new Vector3(backgroundWidth, 0, 0), Quaternion.identity);
    }
}
