using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Skill_Cube : NetworkBehaviour {

    public float staminaCost;
    public float jumpForce;
    public AudioSource TakeDamage;

    private SkillBarScript _skillbar;
    float currStamina;
    Rigidbody2D rb;
    // Use this for initialization
    void Start()
    {
        _skillbar = GetComponent<SkillBarScript>();
        currStamina = _skillbar.StaminaAmount;
        rb = GetComponent<Rigidbody2D>();
        TakeDamage.volume = GameObject.Find("GameManager").GetComponent<GameManager>().GameSettings.MusicVolume;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            // exit from update if this is not the local player
            return;
        }
        currStamina = _skillbar.StaminaAmount;
        if (currStamina > staminaCost && Input.GetButtonDown("Skill") && rb.velocity.y<5)
        {
            _skillbar.StaminaAmount -= staminaCost;
            rb.velocity=(new Vector3(0, jumpForce,0));
            GetComponent<PlayerController>().PlayerAnimator.SetBool("IsUsingSkill", true);
            StartCoroutine(Timer(0.1f));
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Obsticle2")
        {
            GetComponent<PlayerController>().PlayerAnimator.SetBool("IsStruck", true);
            rb.AddForce(new Vector2(-3, 2), ForceMode2D.Impulse);
            //CmdAddForce();
            //CmdDeleteObject(collision.gameObject);
            StartCoroutine(Timer(0.1f));
        }
        
    }
    [Command]
    void CmdAddForce()
    {
        Debug.Log("adding Force");
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(-3, 2), ForceMode2D.Impulse);
    }
    /*[Command]
    void CmdDeleteObject(GameObject delete)
    {
        NetworkServer.Destroy(delete);
    }*/
    IEnumerator Timer(float counter)
    {
        yield return new WaitForSeconds(counter);
        GetComponent<PlayerController>().PlayerAnimator.SetBool("IsUsingSkill", false);
        GetComponent<PlayerController>().PlayerAnimator.SetBool("IsStruck", false);
    }
}
