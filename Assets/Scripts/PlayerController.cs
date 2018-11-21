﻿using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerController : NetworkBehaviour
{
    // Public variables
    [Header("Player Name")]
    [SyncVar] public string PlayerName;

    public Text PlayerNameText;

    [Header("Jump Settings")]
    public float JumpForce;
    public float JumpTime;
    public float JumpTimeCounter;
    public bool Grounded;
    public LayerMask WhatIsGround;
    public bool StoppedJumping;
    public float GroundCheckRadius;
    [Header("Animator")]
    public Animator Animator;

    // Private variables
    private Rigidbody2D _rigidBody;
    private SpriteRenderer _sprite;
    private NetworkManager _networkManager;
    private NetworkAnimator _networkanimator;
    private GameManager _gameManager;
    private GameController _gameController;
    private Transform _groundCheck;


    // Use this for initialization
    void Start () {
        if (!isLocalPlayer)
        {
            return;
        }
        Initialise();

        PlayerName = _gameManager.PlayerName;
        CmdSetPlayerName(PlayerName);
        JumpTimeCounter = JumpTime;
        WhatIsGround = LayerMask.GetMask("Ground");
        GroundCheckRadius = 1;
    }
    void Initialise()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _rigidBody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        _networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        _networkanimator = GetComponent<NetworkAnimator>();
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();
        _groundCheck = this.gameObject.transform;
    }
    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if (Grounded)
        {
            Animator.SetBool("IsGrounded", true);
            JumpTimeCounter = JumpTime;
        }
        else
        {
            Animator.SetBool("IsGrounded", false);
        }
        if (_gameController.GameActive)
        {
            Animator.SetBool("IsRunning", true);
        }

        _networkanimator.GetParameterAutoSend(3);
        _networkanimator.GetParameterAutoSend(2);
        _networkanimator.GetParameterAutoSend(1);
        _networkanimator.GetParameterAutoSend(0);
    }
    // Update is called once per frame
    void FixedUpdate () {
        if (!isLocalPlayer)
        {
            return;
        }
        Grounded = Physics2D.OverlapCircle(_groundCheck.position, GroundCheckRadius, WhatIsGround);
        if (Input.GetKeyDown("space"))
        {
            //and you are on the ground...
            if (Grounded)
            {
                //jump!
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, JumpForce);
                StoppedJumping = false;
            }
            //https://forum.unity.com/threads/mario-style-jumping.381906/
            //use this link to implement mario style jumping
        }
        //if you keep holding down the jump button...
        if (Input.GetKeyDown("space") && !StoppedJumping)
        {
            //and your counter hasn't reached zero...
            if (JumpTimeCounter > 0)
            {
                //keep jumping!
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, JumpForce);
                JumpTimeCounter -= Time.deltaTime;
            }
        }
        //if you stop holding down the jump button...
        if (Input.GetKeyDown("space"))
        {
            //stop jumping and set your counter to zero.  The timer will reset once we touch the ground again in the update function.
            JumpTimeCounter = 0;
            StoppedJumping = true;
        }
        if (_gameController.GameActive)
        {
            Animator.SetBool("IsRunning", true);
        }
        else
        {
            Animator.SetBool("IsRunning", false);
        }
    }
    [Command]
    void CmdSetPlayerName(string PlayerName)
    {
        PlayerNameText.text = PlayerName;
    }
}
