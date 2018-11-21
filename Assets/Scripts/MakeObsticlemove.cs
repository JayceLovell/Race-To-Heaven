using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeObsticlemove : MonoBehaviour {

    private GameController _gameController;

    // Use this for initialization
    void Start () {
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (_gameController.GameActive)
        {
            //GetComponent<Rigidbody2D>().AddForce(new Vector2(-1f, 0f) * _speed);
            transform.Translate(Vector2.left * _gameController.Speed * Time.deltaTime);
        }
    }
    //eliminate spawned objects on the left when they exit the collision box
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "Off")
        {
            Debug.Log("Destroy");
            Destroy(this.gameObject);
        }
    }
}
