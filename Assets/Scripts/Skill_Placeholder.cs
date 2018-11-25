using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Placeholder : MonoBehaviour {
    public float staminaCost;
    public float staminaRegen;
    public float cooldown;
    public GameObject portalPrefab;
   
    float maxStamina = 100;
    float currStamina;
    float currCD = 0;
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

        if (currCD > 0) currCD -= Time.deltaTime;
        else if (currCD < 0) currCD = 0;


        if (currStamina > staminaCost && Input.GetButtonDown("skill") && currCD == 0)
        {
            if (portal == null)
            {
                currStamina -= staminaCost;
                portal = Instantiate(portalPrefab, this.transform.position, Quaternion.identity);
            }
            else
            {
                currCD = cooldown;
                this.transform.position = portal.transform.position;
                Destroy(portal);   
            }
        }

       
    }
    private void OnCollisionStay2D(Collision2D collision)
    {


        if (collision.gameObject.tag == "Obsticle2")
        {
            GetComponent<PlayerController>().Animator.SetBool("IsStruck", true);
            rb.AddForce(new Vector2(-1, 0), ForceMode2D.Impulse);
            Destroy(collision.gameObject);
        }

    }
}
