using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Placeholder : MonoBehaviour {
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
                _skillbar.StaminaAmount -= staminaCost;
                portal = Instantiate(portalPrefab, this.transform.position, Quaternion.identity);
        }
        else if ( Input.GetButtonDown("Skill") && portal != null)
        {
            this.transform.position = portal.transform.position;
            Destroy(portal);
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
