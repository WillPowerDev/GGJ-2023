using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public LevelManager levelManager;

    public Player player;
    Health playerHealth;
    PlayerAnimator playerAnimator;
    float timerTime;
    float ironBits;
    float health;
    private float maxHealth;
    int lives;
    int level;
    [SerializeField] int maxLives;
    [SerializeField] float maxTime;
    [SerializeField] string mainMenu;
    [SerializeField] List<string> levels;

    //Everything displayed on the UI should be passed through/handled here. Level advancement should be handled here as well.

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one GameController! " + transform + " - " + Instance);
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        Reset();
    }

    public void Reset()
    {
        lives = maxLives; 
        ironBits = 0; 
    }

    public void Init(Player player)
    {
        this.player = player;
        playerAnimator = player.GetComponent<PlayerAnimator>();
        playerHealth = player.GetComponent<Health>();
        levelManager = FindObjectOfType<LevelManager>();
        timerTime = maxTime;
        levelManager.Initialize();

    }

    public int GetMaxHealth()
    {
        return playerHealth.MaxHP;
    }

    public int GetCurrentHealth()
    {
        return playerHealth.CurrentHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;
        if (timerTime > 0)
        {
            timerTime -= Time.deltaTime;
        }
        else
        {
            Death();
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

    public int GetLives()
    {
        return lives;
    }

    // read the players health and determine if dies; reset level
    public void Death()
    {
        Debug.Log("GameController.cs  lives: " + lives);
        lives -= 1;
        if (lives <= 0)
        {
            GameOver();
            return;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //read the players lives and determine if game over. Load main menu scene
    public void GameOver()
    {
        SceneManager.LoadScene(mainMenu);
        Reset();
    }

    public void NextLevel()
    {
        level += 1;
        SceneManager.LoadScene(levels[level]);
        timerTime = maxTime;
    }
}
