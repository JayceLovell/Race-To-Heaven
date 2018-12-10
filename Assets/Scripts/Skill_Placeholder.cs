using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Skill_Placeholder : NetworkBehaviour
{
    public float staminaCost;
    public GameObject portalPrefab;
    private SkillBarScript _skillbar;
    float currStamina;
    GameObject portal = null;
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

        if (currStamina > staminaCost && Input.GetButtonDown("Skill") && portal == null)
        {
            GetComponent<PlayerController>().Animator.SetBool("IsUsingSkill", true);
            StartCoroutine(Timer(0.5f));
            _skillbar.StaminaAmount -= staminaCost;
            portal = Instantiate(portalPrefab, this.transform.position, Quaternion.identity);
        }
        else if ( Input.GetButtonDown("Skill") && portal != null)
        {
            GetComponent<PlayerController>().Animator.SetBool("IsUsingSkill", true);
            this.transform.position = portal.transform.position;
            Destroy(portal);
            StartCoroutine(Timer(0.5f));
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {


        if (collision.gameObject.tag == "Obsticle2")
        {
            GetComponent<PlayerController>().Animator.SetBool("IsStruck", true);
            CmdAddForce();
            NetworkServer.Destroy(collision.gameObject);
            StartCoroutine(Timer(0.1f));
        }

    }
    [Command]
    void CmdAddForce()
    {
        rb.AddForce(new Vector2(-3, 2), ForceMode2D.Impulse);
    }
    IEnumerator Timer(float counter)
    {
        yield return new WaitForSeconds(counter);
        GetComponent<PlayerController>().Animator.SetBool("IsUsingSkill", false);
        GetComponent<PlayerController>().Animator.SetBool("IsStruck", false);
    }
}
