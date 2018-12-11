﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Skill_Miner : NetworkBehaviour
{

    public float cooldown; // must be more than 1, since our previous slash gameobject takes 1 sec to despawn
    public AudioSource TakeDamage;

    float currStamina;
    float currCD = 0;
    private Rigidbody2D _rigibody;
    private SkillBarScript _skillbar;
    public float _staminaCost;
    private bool isUsingSkill;



    void Start()
    {
        _rigibody = GetComponent<Rigidbody2D>();
        _skillbar = GetComponent<SkillBarScript>();
        currStamina = _skillbar.StaminaAmount;
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

    IEnumerator Timer(float counter)
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
            CmdDeleteObject(collision.gameObject);
        }
        else if(collision.gameObject.tag == "Obsticle2")
        {
            this.gameObject.GetComponent<PlayerController>().Animator.SetBool("IsStruck", true);
            StartCoroutine(Timer(0.1f));
            CmdAddForce();
            CmdDeleteObject(collision.gameObject);
            TakeDamage.Play();
        }
    }
    [Command]
    void CmdAddForce()
    {
        _rigibody.AddForce(new Vector2(-3, 2), ForceMode2D.Impulse);
    }
    [Command]
    void CmdDeleteObject(GameObject delete)
    {
        NetworkServer.Destroy(delete);
    }

}
