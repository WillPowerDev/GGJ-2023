using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    //[SerializeField] LayerMask playerMask;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Collectable.cs  Collected");
            Destroy(this.gameObject);
        }
    }
}
