using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer mainMixer;

    [SerializeField] private Button volumeButton;
    [SerializeField] private Button graphicsButton;
    [SerializeField] private GameObject volumeNotch;
    [SerializeField] private int maxVolume;
    [SerializeField] private int minVolume;
    [SerializeField] private int changeInVolume;
    [SerializeField] private Vector3 change;
    [SerializeField] private List<Button> graphicsButtons;
    private int volume;

    private bool volumeChangeable;
    private bool graphicsChangeable;

    void Start()
    {
        volumeChangeable = false;
        volumeButton.gameObject.GetComponent<Button>().Select();
    }

    public void SetVolume(int newVolume)
    {
        mainMixer.SetFloat("volumeParameter", newVolume);
        Debug.Log(newVolume);
        Debug.Log(volume);
        // needs to be sent to game manager
    }

    void Update()
    {
        if (volumeChangeable)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) && volume != minVolume)
            {
                volumeNotch.gameObject.transform.Translate(-change);
                volume = volume - changeInVolume;
                SetVolume(volume);

            }

            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) && volume != maxVolume)
            {
                volumeNotch.gameObject.transform.Translate(change);
                volume = volume + changeInVolume;
                SetVolume(volume);
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                volumeChangeable = false;
                volumeButton.Select();
            }
        }
        else if (graphicsChangeable)
        {
            volumeButton.interactable = false;
            graphicsButton.interactable = false;

            graphicsButtons[0].Select();
            int buttonSelected = 0;

            for (int i = 0; i < graphicsButtons.Count; i++)
            {
                graphicsButtons[i].interactable = true;
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) && (buttonSelected - 1) !< 0 )
            {
                buttonSelected--;
                graphicsButtons[buttonSelected].Select();
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) && (buttonSelected + 1)! > graphicsButtons.Count)
            {
                buttonSelected++;
                graphicsButtons[buttonSelected].Select();
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                graphicsChangeable = false;
                for (int i = 0; i < graphicsButtons.Count; i++)
                {
                    graphicsButtons[i].interactable = false;
                }
                volumeButton.interactable = true;
                graphicsButton.interactable = true;
                graphicsButton.Select();
            }
        }


    }

    public void VolumeControl()
    {
        volumeChangeable = true;
    }

    public void GraphicsToggle()
    {
        graphicsChangeable = true;
    }
}
