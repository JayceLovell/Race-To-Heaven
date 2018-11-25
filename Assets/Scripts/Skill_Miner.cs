﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Miner : MonoBehaviour {

    public float cooldown; // must be more than 1, since our previous slash gameobject takes 1 sec to despawn

    float currStamina;
    float currCD = 0;
    private Rigidbody2D _rigibody;
    private SkillBarScript _skillbar;
    private float _staminaCost;
    private bool isUsingSkill;



    void Start()
    {
        _rigibody = GetComponent<Rigidbody2D>();
        _skillbar = GetComponent<SkillBarScript>();
        currStamina = _skillbar.StaminaAmount;
        _staminaCost = 50;
    }


    void Update()
    {

        currStamina = _skillbar.StaminaAmount;
        if (currCD > 0)
            currCD -= Time.deltaTime;
        else if (currCD < 0)
            currCD = 0;


        if (currStamina > _staminaCost && Input.GetButtonDown("Skill") && currCD == 0)
        {
            _skillbar.StaminaAmount -= _staminaCost;
            currCD = cooldown;
            GetComponent<PlayerController>().Animator.SetBool("IsUsingSkill", true);
            isUsingSkill = true;
            StartCoroutine(Timer(1));
        }

    }

    IEnumerator Timer(int counter)
    {
        yield return new WaitForSeconds(counter);
        isUsingSkill = false;
        GetComponent<PlayerController>().Animator.SetBool("IsUsingSkill", false);
        GetComponent<PlayerController>().Animator.SetBool("IsStruck", false);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Obsticle1" || collision.gameObject.tag == "Obsticle2") && isUsingSkill)
        {
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.tag == "Obsticle2")
        {
            GetComponent<PlayerController>().Animator.SetBool("IsStruck", true);
            _rigibody.AddForce(new Vector2(-1, 0), ForceMode2D.Impulse);
            Destroy(collision.gameObject);
            StartCoroutine(Timer(1));
        }
    }

}
