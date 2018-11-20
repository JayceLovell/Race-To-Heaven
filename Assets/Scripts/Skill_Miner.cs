using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Miner : MonoBehaviour {
    public float staminaCost;
    public float staminaRegen;
    public GameObject slashPrefab;
    public float cooldown; // must be more than 1, since our previous slash gameobject takes 1 sec to despawn

    float maxStamina = 100;
    float currStamina;
    float currCD = 0;
    GameObject slash = null;
    Rigidbody2D rb;

    void Start()
    {
        currStamina = maxStamina;
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (currStamina < maxStamina) currStamina += staminaRegen;
        else if (currStamina > maxStamina) currStamina = maxStamina;

        if (currCD > 0) currCD -= Time.deltaTime;
        else if (currCD < 0) currCD = 0;


        if (currStamina > staminaCost && Input.GetButtonDown("skill") && currCD == 0)
        {
            currStamina -= staminaCost;
            currCD = cooldown;
            slash = Instantiate(slashPrefab, this.transform.position+ new Vector3(2,0,0), Quaternion.identity);
            StartCoroutine(Timer(1));
        }

    }

    IEnumerator Timer(int counter)
    {
        yield return new WaitForSeconds(counter);
        Destroy(slash);
    }
}
