using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Chicken : MonoBehaviour {
    public float staminaCost;
    public float staminaRegen;

    float maxStamina = 100;
    float currStamina;
    Rigidbody2D rb;

    void Start () {
        currStamina = maxStamina;
        rb = GetComponent<Rigidbody2D>();
    }
	
	void Update () {
        if (currStamina < maxStamina) currStamina += staminaRegen;
        else if (currStamina > maxStamina) currStamina = maxStamina;

        if (currStamina > staminaCost && rb.velocity.y < -0.5f && Input.GetButton("skill"))
        {
            currStamina -= staminaCost * Time.deltaTime;
            rb.velocity = new Vector2(rb.velocity.x,-0.5f);
        }
	}
}
