using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeObsticlemove : MonoBehaviour {

    private float _speed;
    private GameController _gameController;

    public float Speed
    {
        get
        {
            return _speed;
        }

        set
        {
            _speed = value;
        }
    }

    // Use this for initialization
    void Start () {
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();
        _speed = 1f;
	}
	
	// Update is called once per frame
	void Update () {
        if (_gameController.GameActive)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-1f, 0f) * _speed);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Off")
        {
            Destroy(this.gameObject);
        }
    }
}
