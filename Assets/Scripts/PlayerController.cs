using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerController : NetworkBehaviour {

    [Header("Movement Settings")]
    public float speed = 10.0f;

    // Private variables
    private Rigidbody2D rBody;
    private GameManager _gameManager;


    // Use this for initialization
    void Start () {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rBody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!isLocalPlayer)
        {
            return;
        }

        float horiz = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(horiz, 0);
        movement.x *= speed; // Scale the speed value to have faster movements.

        // Assign the movement to the players velocity.
        rBody.velocity = movement;

            Debug.Log(horiz);

    }
    public override void OnStartLocalPlayer()
    {

    }
}
