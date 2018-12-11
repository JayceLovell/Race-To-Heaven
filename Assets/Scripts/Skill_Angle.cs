using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Angle : MonoBehaviour {
    public float staminaCost;
    public float staminaRegen;
    public float jumpStrength;
    public float cooldown;
 
    float maxStamina = 100;
    float currStamina;
    float currCD=0;
    Rigidbody2D rb;

    void Start()
    {
        currStamina = maxStamina;
        rb = GetComponent<Rigidbody2D>();
    }


    void Update () {
        if (currStamina < maxStamina) currStamina += staminaRegen;
        else if (currStamina > maxStamina) currStamina = maxStamina;

        if (currCD > 0) currCD -= Time.deltaTime;
        else if (currCD < 0) currCD = 0;

        if (currStamina > staminaCost && Input.GetButtonDown("skill")&& currCD == 0)
        {
            currStamina -= staminaCost;
            currCD = cooldown;
            rb.AddForce(new Vector2(0,jumpStrength), ForceMode2D.Impulse);
        }

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obsticle2")
        {
            GetComponent<PlayerController>().PlayerAnimator.SetBool("IsStruck", true);
            rb.AddForce(new Vector2(-3, 2), ForceMode2D.Impulse);
            Destroy(collision.gameObject);
        }
    }
}
