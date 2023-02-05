using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    [SerializeField] LayerMask targets;
    [SerializeField] float radius; 
    [SerializeField] int damage;

    GameObject owner;

    List<Health> damagedUnits;

    void Start()
    {
        damagedUnits = new List<Health>(); 
    }

    public void Init(GameObject owner)
    {
        this.owner = owner;
    }

    void Update()
    {
        List<Collider2D> collisions = Physics2D.OverlapCircleAll(transform.position, radius, targets).ToList();
        foreach (Collider2D collider in collisions)
        {
            if (collider.gameObject.TryGetComponent<Health>(out Health health))
            {
                if (damagedUnits.Contains(health)) continue;

                if (health.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
                {
                    //enemy.Knockback(owner.transform.localScale.x);
                }

                if (health.gameObject.TryGetComponent<Player>(out Player player))
                {
                    //player.Knockback(transform.localScale.x);
                }

                health.TakeDamage(damage);
                damagedUnits.Add(health);
            }
        }
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);  
    }
}
