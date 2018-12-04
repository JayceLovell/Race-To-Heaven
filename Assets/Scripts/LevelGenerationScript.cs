using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LevelGenerationScript : MonoBehaviour {

    [Header("objects")]
    public GameObject[] prefabs;
    public float[] spawnHeightAdjustment;
    GameObject spawnedObject;
    public float obstacleMinWidth;
    public float obstacleMaxWidth;
    public float speedMultipliler;
    int prefabSelection;


    void Start()
    {
            prefabSelection = Random.Range(0, prefabs.Length);
            spawnedObject = Instantiate(prefabs[prefabSelection], transform.position + new Vector3(Random.Range(obstacleMinWidth, obstacleMaxWidth), spawnHeightAdjustment[prefabSelection], 0), Quaternion.identity);
            NetworkServer.Spawn(spawnedObject);          
    }
    private void FixedUpdate()
    {
            obstacleMinWidth += Time.deltaTime * speedMultipliler;
            obstacleMaxWidth += Time.deltaTime * speedMultipliler * (obstacleMaxWidth / obstacleMinWidth);


            if (spawnedObject.transform.position.x < transform.position.x)
            {
                prefabSelection = Random.Range(0, prefabs.Length);

                spawnedObject = Instantiate(prefabs[prefabSelection], transform.position + new Vector3(Random.Range(obstacleMinWidth, obstacleMaxWidth), spawnHeightAdjustment[prefabSelection], 0), Quaternion.identity);
                    NetworkServer.Spawn(spawnedObject);
            }
    }
}
