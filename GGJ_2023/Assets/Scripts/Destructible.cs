using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Destructible : MonoBehaviour
{
    [SerializeField] GameObject destructionParticle;

    void OnDestroy()
    {
        if(!this.gameObject.scene.isLoaded) return;
        Instantiate(destructionParticle, transform.position, Quaternion.identity);
    }
}
