using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerController : NetworkBehaviour
{
    // Public variables
    [Header("Player Name")]
    [SyncVar(hook = "OnPlayerNameChange")] public string PlayerName;

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
    void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        Initialise();

        PlayerName = _gameManager.PlayerName;
        SetPlayerName(PlayerName);
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
    void Update()
    {
        Grounded = Physics2D.OverlapCircle(_groundCheck.position, GroundCheckRadius, WhatIsGround);
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
    void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if (Input.GetKeyDown("space"))
        {
            //and you are on the ground...
            if (Grounded)
            {
                //jump!
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, JumpForce);
                StoppedJumping = false;
            }
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
            var otherplayers = GameObject.FindGameObjectsWithTag("Player");
            foreach (var player in otherplayers)
            {
                Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());

            }
        }
        else
        {
            Animator.SetBool("IsRunning", false);
        }
    }
    void SetPlayerName(string PlayerName)
    {
        PlayerNameText.text = PlayerName;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Off")
        {
            if (isClient)
            {
                _networkManager.StopClient();
            }
        }
    }
    /*public override void OnStartLocalPlayer()
    {
        var otherplayers = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in otherplayers)
        {
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        }
    }*/
    void OnPlayerNameChange(string valueToChangeTo)
    {
        PlayerName = valueToChangeTo;
    }
}
