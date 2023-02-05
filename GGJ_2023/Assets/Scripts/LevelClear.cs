using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelClear : MonoBehaviour
{
    [SerializeField] GameObject clearParticles;
    [SerializeField] float clearDuration;
    Timer clearTimer;
    void Awake()
    {
        clearTimer = new Timer(clearDuration, () => {
            GameController.Instance.NextLevel();
        });
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Instantiate(clearParticles, transform.position, Quaternion.identity);
            other.gameObject.SetActive(false);
            clearTimer.Begin();
        }
    }

    void Update()
    {
        clearTimer.Tick(Time.deltaTime);
    }
}
