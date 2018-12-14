using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Skill_Chicken : NetworkBehaviour
{
    public float staminaCost;
    public float currCD = 0;
    public AudioSource TakeDamage;

    float currStamina;
    private SkillBarScript _skillbar;
    Rigidbody2D rb;

    void Start () {
        _skillbar = GetComponent<SkillBarScript>();
        currStamina = _skillbar.StaminaAmount;
        rb = GetComponent<Rigidbody2D>();
        currCD = 0;
        TakeDamage.volume = GameObject.Find("GameManager").GetComponent<GameManager>().GameSettings.MusicVolume;
    }
	
	void FixedUpdate () {
        if (!isLocalPlayer)
        {
            // exit from update if this is not the local player
            return;
        }
        currStamina = _skillbar.StaminaAmount;
        if (currCD > 0)
            currCD -= Time.deltaTime;
        else if (currCD < 0)
            currCD = 0;

        if (currStamina > staminaCost && rb.velocity.y < -0.5f && Input.GetButton("Skill") && currCD <= 0)
        {
            GetComponent<PlayerController>().PlayerAnimator.SetBool("IsUsingSkill", true);
            _skillbar.StaminaAmount -= staminaCost * Time.deltaTime;
            rb.velocity = new Vector2(rb.velocity.x, -0.5f);
        }
        else if(Input.GetButtonUp("Skill"))
        {
            GetComponent<PlayerController>().PlayerAnimator.SetBool("IsUsingSkill", false);
        }
        /*else
        {
            GetComponent<PlayerController>().Animator.SetBool("IsUsingSkill", false);
        }*/
        if (currStamina <= staminaCost)
        {
            currCD = 1;
        }
	}
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obsticle2")
        {
            GetComponent<PlayerController>().PlayerAnimator.SetBool("IsStruck", true);
            rb.AddForce(new Vector2(-3, 2), ForceMode2D.Impulse);
            //CmdAddForce();
            //CmdDeleteObject(collision.gameObject);
            StartCoroutine(Timer(0.1f));
            TakeDamage.Play();
        }
    }
    IEnumerator Timer(float counter)
    {
        yield return new WaitForSeconds(counter);
        GetComponent<PlayerController>().PlayerAnimator.SetBool("IsUsingSkill", false);
        GetComponent<PlayerController>().PlayerAnimator.SetBool("IsStruck", false);
    }
    [Command]
    void CmdAddForce()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(-3, 2), ForceMode2D.Impulse);
    }
    /*[Command]
    void CmdDeleteObject(GameObject delete)
    {
        NetworkServer.Destroy(delete);
    }*/
}
