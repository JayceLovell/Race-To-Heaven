using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Cube : MonoBehaviour {

    public float staminaCost;
    public float staminaRegen;
    public float jumpForce;
    public GameObject portalPrefab;

    float maxStamina = 100;
    float currStamina;
    bool jumpReady = true;
    GameObject portal = null;
    Rigidbody2D rb;
    // Use this for initialization
    void Start()
    {
        currStamina = maxStamina;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currStamina < maxStamina) currStamina += staminaRegen;
        else if (currStamina > maxStamina) currStamina = maxStamina;



        if (currStamina > staminaCost && Input.GetButtonDown("skill") && jumpReady)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            jumpReady = false;
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground" && !jumpReady)
            jumpReady = true;


            if (collision.gameObject.tag == "Obsticle2")
            {
                GetComponent<PlayerController>().Animator.SetBool("IsStruck", true);
            rb.AddForce(new Vector2(-1, 0), ForceMode2D.Impulse);
            Destroy(collision.gameObject);
        }
        
    }
}
