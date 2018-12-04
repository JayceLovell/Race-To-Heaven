using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : NetworkBehaviour
{
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
    private SpriteRenderer _sprite;
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
        CmdSetPlayerName(_playerName);
        JumpTimeCounter = JumpTime;
        GroundCheckRadius = 1;
        JumpForce = 9;
    }
    void Initilize()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _rigidBody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
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
            /*if (_gameController.GameActive)
            {
                Animator.SetBool("IsRunning", true);
                var otherplayers = GameObject.FindGameObjectsWithTag("Player");
                foreach (var player in otherplayers)
                {
                    Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                }
            }*/
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
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Off")
        {
            GameOver.Play();
            NetworkIdentity.Destroy(this.gameObject);
            //SceneManager.LoadScene("Main Menu");
        }
    }
}

    
