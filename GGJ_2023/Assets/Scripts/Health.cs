using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event EventHandler OnDead;
    public event EventHandler OnDamaged;

    public static event EventHandler OnAnyDamaged;

    [SerializeField] float invincibileTimer;
    [SerializeField] int maxHealth;

    bool invincible;
    float timer;
    int currentHealth;
    int previousHealth;

    public int CurrentHP {get {return currentHealth; }}
    public int PreviousHP {get {return previousHealth; }}
    public int MaxHP {get {return maxHealth; }}

    void Awake()
    {
        currentHealth = maxHealth;
        previousHealth = currentHealth;
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            invincible = false; 
        }
    }

    public void TakeDamage(int damage)
    {
        if (!invincible)
        {
            Debug.Log("Health.cs " + gameObject.name + " taking damage: " + damage);
            currentHealth -= damage;
            OnDamaged?.Invoke(this, EventArgs.Empty);
            OnAnyDamaged?.Invoke(this, EventArgs.Empty);
            
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                OnDead?.Invoke(this, EventArgs.Empty);
            }

            previousHealth = currentHealth;
            invincible = true; 
            timer = invincibileTimer;
        }
    }
}
