using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
//using DG.Tweening;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject buttonsToActivate;
    private bool canActivateButtons;
    [SerializeField] private GameObject gameSelectButtons;
    private bool gameSelectButtonsActivated;
    [SerializeField] private Button play;
    [SerializeField] private Button startGame;

    // Start is called before the first frame update
    void Start()
    {
        //DOTween.Init(autoKillMode, useSafeMode, logBehaviour);
        buttonsToActivate.gameObject.SetActive(false);
        canActivateButtons = true;
        gameSelectButtons.gameObject.SetActive(false);
        gameSelectButtonsActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && canActivateButtons)
        {
                        
            buttonsToActivate.gameObject.SetActive(true);
            canActivateButtons = false;
        }

        if(Input.GetKeyDown(KeyCode.Escape) && gameSelectButtonsActivated)
        {
            gameSelectButtons.gameObject.SetActive(false);
            gameSelectButtonsActivated = false;
            buttonsToActivate.gameObject.SetActive(true);
            play.gameObject.GetComponent<Button>().Select();
        }

        if(EventSystem.current.currentSelectedGameObject == null)
        {
            FindObjectOfType<Button>().Select();
        }



    }

    public void PlaySelect()
    {
        buttonsToActivate.gameObject.SetActive(false);
        gameSelectButtons.gameObject.SetActive(true);
        gameSelectButtonsActivated = true;

        startGame.gameObject.GetComponent<Button>().Select();
    }

    public void StartGame(string levelToStart)
    {
        if(levelToStart == null)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(levelToStart);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
