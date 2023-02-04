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
        
    }


}
