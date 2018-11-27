using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeObsticlemove : MonoBehaviour {

    public int obsticleType;
    
    private GameController _gameController;
    private bool goingUp = true;


    // Use this for initialization
    void Start () {
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();

        switch (obsticleType)
        {
            case 0:
                transform.Translate(Vector2.down * 0.3f);
                break;
            case 1:
                transform.Translate(Vector2.down * 0.85f);
                break;
            case 2:
                transform.Translate(Vector2.up * Random.Range(0,2f));
                StartCoroutine(movingPlatform(2));
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (_gameController.GameActive)
        {
            transform.Translate(Vector2.left * _gameController.Speed * Time.deltaTime);
        }
        if (obsticleType == 2)
        {
            if (goingUp)
                transform.Translate(Vector2.up * Time.deltaTime);
            else
                transform.Translate(Vector2.down * Time.deltaTime);
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
    IEnumerator movingPlatform(int counter)
    {
        yield return new WaitForSeconds(counter);
        goingUp = !goingUp;
        StartCoroutine(movingPlatform(counter));
    }
}
