using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerController : NetworkBehaviour
{
    [SyncVar] public string PlayerName;

    // Public variables
    [Header("Movement Settings")]
    public float speed = 10.0f;

    public Text PlayerNameText;

    // Private variables
    private Rigidbody2D _rigidBody;
    private Animator _animator;
    private SpriteRenderer _sprite;
    private NetworkManager _networkManager;
    private NetworkAnimator _networkanimator;
    private GameManager _gameManager;


    // Use this for initialization
    void Start () {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        _networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        _networkanimator = GetComponent<NetworkAnimator>();
        PlayerName = _gameManager.PlayerName;
        CmdSetPlayerName(PlayerName);
    }
    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        _networkanimator.GetParameterAutoSend(1);
        _networkanimator.GetParameterAutoSend(0);
    }
    // Update is called once per frame
    void FixedUpdate () {
        if (!isLocalPlayer)
        {
            return;
        }
        _animator.SetBool("IsGrounded", true);
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _rigidBody.velocity = movement * speed;

        if (_rigidBody.velocity.x < 0)
            _sprite.flipX = true;
        else
            _sprite.flipX = false;
        if (_rigidBody.velocity.magnitude > 0.1f)
            _animator.SetBool("IsRunning", true);
        else
            _animator.SetBool("IsRunning", false);
        //CmdMove(movement);
    }
    [Command]
    void CmdSetPlayerName(string PlayerName)
    {
        PlayerNameText.text = PlayerName;
    }
    /*[Command]
    void CmdMove(Vector2 movement)
    {
        _rigidBody.velocity = movement * speed;

        if (_rigidBody.velocity.x < 0)
            _sprite.flipX = true;
        else
            _sprite.flipX = false;
        if (_rigidBody.velocity.magnitude > 0.1f)
            _animator.SetBool("IsRunning", true);
        else
            _animator.SetBool("IsRunning", false);
    }*/
}
