using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
//using DG.Tweening;

public class MainMenuManager : MonoBehaviour, ISelectHandler
{
    private GameController gameController;
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject buttonsToActivate;
    private bool canActivateButtons;
    [SerializeField] private GameObject gameSelectButtons;
    private bool gameSelectButtonsActivated;
    [SerializeField] private Button play;
    [SerializeField] private Button startGame;
    [SerializeField] private GameObject levelButtons;
    [SerializeField] private Button firstLevelButton;
    [SerializeField] private GameObject logo;
    private bool levelSelectButtonsActivated;

    [SerializeField] private GameObject optionsMenu;
    private bool isOptionsMenuOpen;

    // Start is called before the first frame update
    void Start()
    {
        //DOTween.Init(autoKillMode, useSafeMode, logBehaviour);
        text.gameObject.SetActive(true);
        buttonsToActivate.gameObject.SetActive(false);
        canActivateButtons = true;
        gameSelectButtons.gameObject.SetActive(false);
        gameSelectButtonsActivated = false;
        levelButtons.gameObject.SetActive(false);
        levelSelectButtonsActivated = false;
        optionsMenu.gameObject.SetActive(false);
        isOptionsMenuOpen = false;
        logo.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && canActivateButtons)
        {
            text.gameObject.SetActive(false);
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

        if (Input.GetKeyDown(KeyCode.Escape) && isOptionsMenuOpen)
        {
            optionsMenu.gameObject.SetActive(false);
            isOptionsMenuOpen = false;
            buttonsToActivate.gameObject.SetActive(true);
            play.Select();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && levelSelectButtonsActivated)
        {
            levelButtons.gameObject.SetActive(false);
            levelSelectButtonsActivated = false;
            gameSelectButtons.gameObject.SetActive(true);
            gameSelectButtonsActivated = true;
            logo.gameObject.SetActive(true);
            startGame.Select();
        }

        if (EventSystem.current.currentSelectedGameObject == null)
        {
            play.Select();
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
            //SceneManager.LoadSceneAsync(1);
            SceneManager.LoadScene(1);
        }
        else
        {
            //SceneManager.LoadSceneAsync(levelToStart);
            SceneManager.LoadScene(levelToStart);
        }
        Instantiate(gameController);
    }

    public void LevelSelect()
    {
        gameSelectButtonsActivated = false;
        gameSelectButtons.gameObject.SetActive(false);
        levelButtons.gameObject.SetActive(true);
        levelSelectButtonsActivated = true;
        firstLevelButton.Select();
        logo.gameObject.SetActive(false);

    }

    public void OptionsMenu()
    {
        optionsMenu.gameObject.SetActive(true);
        isOptionsMenuOpen = true;
        buttonsToActivate.gameObject.SetActive(false);
    }
    public void Credits()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnSelect(BaseEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void ButtonSound()
    {
        SoundManager.Instance.PlaySound(Sound.buttonSFX);
    }
}
