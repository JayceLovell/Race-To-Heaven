using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeObsticlemove : MonoBehaviour {

    public int obsticleType;
    //type 0 no special
    //type 1 moves up and down
    
    GameController _gameController;
    bool goingUp = true;
    float verticalChangeTimer;


    // Use this for initialization
    void Start () {
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();
        verticalChangeTimer = _gameController.Speed;

        switch (obsticleType)
        {
            case 0:
                break;
            case 1:
                StartCoroutine(movingPlatform());
                break;
        }
	}
	
	void Update () {
        if (_gameController == null)
        {
            _gameController = GameObject.Find("GameController").GetComponent<GameController>();
        }


        if (_gameController.GameActive)
        {
            transform.Translate(Vector2.left * _gameController.Speed * Time.deltaTime);
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
            Destroy(this.gameObject);
        }
    }
    IEnumerator movingPlatform()
    {
        yield return new WaitForSeconds(4 / verticalChangeTimer);
        goingUp = !goingUp;
        StartCoroutine(movingPlatform());
    }
}
