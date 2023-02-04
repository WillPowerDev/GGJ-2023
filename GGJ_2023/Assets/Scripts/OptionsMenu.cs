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
    [SerializeField] private GameObject volumeNotch;
    [SerializeField] private int maxVolume;
    [SerializeField] private int minVolume;
    [SerializeField] private int changeInVolume;
    [SerializeField] private Vector3 change;
    private int volume;

    private bool volumeChangeable;

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
        if(volumeChangeable)
        {
            if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) && volume != minVolume)
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

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                volumeChangeable = false;
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {

            }
        }
    }

    public void VolumeControl()
    {
        volumeChangeable = true;
    }
}
