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

    Timer invincibilityTimer;

    public bool Invincible {get {return invincible; }}
    public int CurrentHP {get {return currentHealth; }}
    public int PreviousHP {get {return previousHealth; }}
    public int MaxHP {get {return maxHealth; }}

    void Awake()
    {
        currentHealth = maxHealth;
        previousHealth = currentHealth;
        invincible = false;
        invincibilityTimer = new Timer(invincibileTimer, () => {
            invincible = false;
        });
    }

    void Update()
    {
        invincibilityTimer.Tick(Time.deltaTime);
    }

    public void TakeDamage(int damage = 1)
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
            invincibilityTimer.Begin();
            //timer = invincibileTimer;
        }
    }
}
