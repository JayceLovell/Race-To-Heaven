using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MakeObsticlemove : NetworkBehaviour {

    public int obsticleType;
    //type 0 no special
    //type 1 moves up and down
    //type 2 moves left

    GameController _gameController;
    bool goingUp = true;
    float verticalChangeTimer;


    // Use this for initialization
    void Start () {
        _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        verticalChangeTimer = _gameController.Speed;

        switch (obsticleType)
        {
            case 0:
                break;
            case 1:
                StartCoroutine(MovingPlatform());
                break;
        }
	}
	
	void Update () {
        if (!isServer)
        {
            return;
        }
        if (_gameController.GameActive)
        {
            transform.Translate(Vector2.left * _gameController.Speed * Time.deltaTime);
            if (obsticleType == 2)
            {

                transform.Translate(Vector2.left * _gameController.Speed * Time.deltaTime/2);
            }
        }
        if (obsticleType == 1)
        {
            if (goingUp)
                transform.Translate(Vector2.up * verticalChangeTimer * Time.deltaTime/2);
            else
                transform.Translate(Vector2.down * verticalChangeTimer * Time.deltaTime/2);
        }
       
    }
    //eliminate spawned objects on the left when they exit the collision box
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Off")
        {
            NetworkServer.Destroy(this.gameObject);
        }
        else if(collision.gameObject.tag == "Player" && this.gameObject.tag == "Obsticle2")
        {
            Debug.Log("sending player back");
            NetworkServer.Destroy(this.gameObject);
        }
        
    }
    IEnumerator MovingPlatform()
    {
        yield return new WaitForSeconds(4 / verticalChangeTimer);
        goingUp = !goingUp;
        StartCoroutine(MovingPlatform());
    }
}
