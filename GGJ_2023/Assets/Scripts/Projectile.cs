using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] LayerMask targets;

    [SerializeField] float duration;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float radius;
    [SerializeField] int damage;

    [Header("Particles")]
    [SerializeField] GameObject particlePrefab; 
    [SerializeField] float particlePeriod;
    [SerializeField] float particlePeriodMaxVariance;

    GameObject owner; 
    float timer;
    float particleTimer;
    float periodVarience;

    List<Health> damagedUnits = new List<Health>();
    // Start is called before the first frame update
    void Start()
    {
        periodVarience = Random.Range(-particlePeriodMaxVariance, particlePeriodMaxVariance); 
    }

    public void Init(GameObject owner)
    {
        this.owner = owner;
        transform.localScale = owner.transform.localScale; 
    }

    // Update is called once per frame
    void Update()
    {
        float lastTimer = timer;

        transform.position = transform.position + (Vector3.right * transform.localScale.x) * (moveSpeed * Time.deltaTime); 

        List<Collider2D> collisions = Physics2D.OverlapCircleAll(transform.position, radius, targets).ToList();
        foreach (Collider2D collider in collisions)
        {
            if (collider.gameObject.TryGetComponent<Health>(out Health health))
            {
                if (damagedUnits.Contains(health)) continue;
                if (health.gameObject == owner) continue;

                if (health.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
                {
                    enemy.Knockback(owner.transform.localScale.x);
                }

                if (health.gameObject.TryGetComponent<Player>(out Player player))
                {
                    //player.Knockback(transform.localScale.x);
                }

                health.TakeDamage(damage);
                damagedUnits.Add(health);
            }
        }

        if (timer > duration)
        {
            Destroy(this.gameObject);
        }
        timer += Time.deltaTime;

        /*
        // If the previous time was greater, it means the timer has passed the period to spawn the particles
        if (particleTimer > particlePeriod + periodVarience)
        {
            Instantiate(particlePrefab, transform.position, Quaternion.identity);
            particleTimer = 0;
            periodVarience = Random.Range(-particlePeriodMaxVariance, particlePeriodMaxVariance); 
        }
        particleTimer += Time.deltaTime; */
    
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);  
    }
}
