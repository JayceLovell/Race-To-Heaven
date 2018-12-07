using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : NetworkBehaviour
{
    [SyncVar] public bool PlayerReady;
    // Public variables
    public Text PlayerNameText;
    public GameObject gameMangerPrefab;

    [Header("Jump Settings")]
    public float JumpTime;
    public float JumpTimeCounter;
    public bool Grounded;
    public LayerMask WhatIsGround;
    public bool StoppedJumping;
    public float GroundCheckRadius;
    [Header("Animator")]
    public Animator Animator;
    public AudioSource GameOver;

    // Private variables
    private Rigidbody2D _rigidBody;
    private GameManager _gameManager;
    private GameController _gameController;
    private Transform _groundCheck;
    private string _playerName;
    private float _jumpForce;

    public float JumpForce
    {
        get
        {
            return _jumpForce;
        }

        set
        {
            _jumpForce = value;
        }
    }


    // Use this for initialization
    void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        Initilize();

        _playerName = _gameManager.PlayerName;
        SetPlayerName(_playerName);
        JumpTimeCounter = JumpTime;
        GroundCheckRadius = 1;
        JumpForce = 9;
    }
    void Initilize()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _rigidBody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();
        _groundCheck = this.gameObject.transform;
        WhatIsGround = LayerMask.GetMask("Ground");
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            // exit from update if this is not the local player
            return;
        }

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
        else
        {
            Animator.SetBool("IsRunning", false);
        }
    }
        // Update is called once per frame
    void FixedUpdate()
        {
        if (!isLocalPlayer)
        {
            // exit from update if this is not the local player
            return;
        }

        if (Input.GetKey("space"))
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
            if (Input.GetKey("space") && !StoppedJumping)
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
            if (Input.GetKey("space"))
            {
                //stop jumping and set your counter to zero.  The timer will reset once we touch the ground again in the update function.
                JumpTimeCounter = 0;
                StoppedJumping = true;
            }
        if ((Input.GetButtonDown("Submit")||Input.GetKeyDown(KeyCode.Space)) && !_gameController.GameActive && !PlayerReady)
        {
            CmdThisPlayerReady();
        }
    }
    [Command]
    private void CmdThisPlayerReady()
    {
        _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        PlayerReady = true;
        _gameController.CheckIfPlayersReady();
    }
    void SetPlayerName(string PlayerName)
    {
        PlayerNameText.text = PlayerName;        
    }
    public void Winner()
    {
        Animator.SetBool("Winner", true);
    }
    [Command]
    void CmdPlayerDead()
    {
        _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        GameOver.Play();
        _gameController.PlayerDead();
        NetworkIdentity.Destroy(this.gameObject);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Off")
        {
            CmdPlayerDead();            
        }
    }
}

    
