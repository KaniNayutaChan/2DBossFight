using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BossHealth : MonoBehaviour
{
    public float maxHealth;
    [HideInInspector] public float currentHealth;
    Animator animator;
    bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth < 0 && !isDead)
        {
            isDead = true;
            Die();
        }
    }

    void Die()
    {
        animator.Play("Death");
    }
}
