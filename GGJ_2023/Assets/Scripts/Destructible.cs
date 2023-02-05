using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Destructible : MonoBehaviour
{
    [SerializeField] GameObject destructionParticle;
    [SerializeField] GameObject dropCollectablePrefab;
    [SerializeField] float dropChance;
    void OnDestroy()
    {
        if(!this.gameObject.scene.isLoaded) return;
        Instantiate(destructionParticle, transform.position, Quaternion.identity);
        
        if (Random.value <= dropChance)
        {
            Instantiate(dropCollectablePrefab, transform.position, Quaternion.identity);
        }
    }
}
