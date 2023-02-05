using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootAttack : MonoBehaviour
{
    [SerializeField] GameObject windUpObject;
    [SerializeField] GameObject attackObject;
    [SerializeField] float timeToStart;
    [SerializeField] float duration;

    Timer attackStartUpTimer;
    Timer attackDuration;
    void Awake()
    {
        attackDuration = new Timer(duration, () => {
            Destroy(this.gameObject);
        });
        attackStartUpTimer = new Timer(timeToStart, () => {
            windUpObject.SetActive(false);
            attackObject.SetActive(true);
            attackDuration.Begin(); 
        });
    }

    void Start()
    {
        attackStartUpTimer.Begin();
    }

    void Update()
    {
        attackDuration.Tick(Time.deltaTime);
        attackStartUpTimer.Tick(Time.deltaTime);
    }
}
