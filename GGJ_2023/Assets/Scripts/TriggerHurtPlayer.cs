using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHurtPlayer : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Health _health = other.GetComponent<Health>();
            _health.TakeDamage();
        }
    }
}
