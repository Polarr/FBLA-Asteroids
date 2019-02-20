using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] clips;
    public static AudioManager Instance = null; //Singleton instance
        //Awake is always called before any Start functions
    private void Awake()
    {
        //Check if instance already exists
        if (Instance == null)
            Instance = this;
            
            //If instance already exists and it's not this:
        else if (Instance != this)
            Destroy(gameObject);    
            
            //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }
    public void PlaySound(SoundEffects index){
        audioSource.clip = clips[(int)index];
        audioSource.Play();
    }
}
public enum SoundEffects{
    Error,
    Success,
    PlayerDeath,
    Shoot,
    ButtonSelect
}
