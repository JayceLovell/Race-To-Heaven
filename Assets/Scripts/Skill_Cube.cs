﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Cube : MonoBehaviour {

    public float staminaCost;
    public float jumpForce;

    private SkillBarScript _skillbar;
    float currStamina;
    Rigidbody2D rb;
    // Use this for initialization
    void Start()
    {
        _skillbar = GetComponent<SkillBarScript>();
        currStamina = _skillbar.StaminaAmount;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        currStamina = _skillbar.StaminaAmount;
        if (currStamina > staminaCost && Input.GetButtonDown("Skill") && rb.velocity.y<5)
        {
            _skillbar.StaminaAmount -= staminaCost;
            rb.velocity=(new Vector3(0, jumpForce,0));
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Obsticle2")
        {
            GetComponent<PlayerController>().Animator.SetBool("IsStruck", true);
            rb.AddForce(new Vector2(-1, 1), ForceMode2D.Impulse);
            Destroy(collision.gameObject);
        }
        
    }
}
