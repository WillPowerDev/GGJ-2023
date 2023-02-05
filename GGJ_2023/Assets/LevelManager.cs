using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameTimer;
    [SerializeField] private GameObject[] healthBar;
    private int gameHealth;
    [SerializeField] private TextMeshProUGUI lifeCounter;
    [SerializeField] private TextMeshProUGUI bitsCounter;
    private bool isInitialized;

    void Start()
    {
        gameTimer.text = Mathf.Round(GameController.Instance.GetTime()).ToString();
        lifeCounter.text = GameController.Instance.GetLives().ToString();
        bitsCounter.text = ("x " + GameController.Instance.GetIronBits().ToString());
        isInitialized = false;
    }

    void Update()
    {
        Debug.Log(gameHealth + " is gameHealth");
        Debug.Log(GameController.Instance.GetCurrentHealth() + " is the current health");
        gameTimer.text = Mathf.Round(GameController.Instance.GetTime()).ToString(); 
        lifeCounter.text = GameController.Instance.GetLives().ToString();
        bitsCounter.text = ("x " + GameController.Instance.GetIronBits().ToString());

        if(isInitialized && gameHealth != GameController.Instance.GetCurrentHealth())
        {
            healthBar[gameHealth - 1].SetActive(false);
            gameHealth--;
        }
    }

    public void Initialize()
    {
        gameHealth = GameController.Instance.GetCurrentHealth();



        for (int i = 0; i < GameController.Instance.GetMaxHealth(); i++)
        {
            if (i <= gameHealth)
            {
                healthBar[i].gameObject.SetActive(true);
            }
            else
            {
                healthBar[i].gameObject.SetActive(false);
            }
        }

        isInitialized = true;
    }
}
