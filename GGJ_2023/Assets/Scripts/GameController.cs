using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public CharacterController player;

    float timerTime;
    float ironBits;
    float health;
    private float maxHealth;
    float lives;
    int level;

    [SerializeField] List<string> levels;

    //Everything displayed on the UI should be passed through/handled here. Level advancement should be handled here as well.

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timerTime > 0)
        {
            timerTime -= Time.deltaTime;
        }
        else
        {

        }
    }

    public float GetTime()
    {
        return timerTime;
    }

    public float GetIronBits()
    {
        return ironBits;
    }

    public void AddIron(int value = 1)
    {
        ironBits += value;
    }

    public int GetLevel()
    {
        return level;
    }

    // read the players health and determine if dies; reset level
    public void Death()
    {

    }

    //read the players lives and determine if game over. Load main menu scene
    public void GameOver()
    {

    }
}
