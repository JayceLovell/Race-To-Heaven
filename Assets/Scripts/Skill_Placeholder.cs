using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Skill_Placeholder : NetworkBehaviour
{
    public float staminaCost;
    public GameObject portalPrefab;
    public AudioSource TakeDamage;
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
        TakeDamage.volume = GameObject.Find("GameManager").GetComponent<GameManager>().GameSettings.MusicVolume;
    }

    // Update is called once per frame
    void Update()
    {

        currStamina = _skillbar.StaminaAmount;

        if (currStamina > staminaCost && Input.GetButtonDown("Skill") && portal == null)
        {
            GetComponent<PlayerController>().PlayerAnimator.SetBool("IsUsingSkill", true);
            StartCoroutine(Timer(0.5f));
            _skillbar.StaminaAmount -= staminaCost;
            portal = Instantiate(portalPrefab, this.transform.position, Quaternion.identity);
        }
        else if ( Input.GetButtonDown("Skill") && portal != null)
        {
            GetComponent<PlayerController>().PlayerAnimator.SetBool("IsUsingSkill", true);
            this.transform.position = portal.transform.position;
            Destroy(portal);
            StartCoroutine(Timer(0.5f));
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {


        if (collision.gameObject.tag == "Obsticle2")
        {
            GetComponent<PlayerController>().PlayerAnimator.SetBool("IsStruck", true);
            CmdAddForce();
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
        GetComponent<PlayerController>().PlayerAnimator.SetBool("IsUsingSkill", false);
        GetComponent<PlayerController>().PlayerAnimator.SetBool("IsStruck", false);
    }
}
