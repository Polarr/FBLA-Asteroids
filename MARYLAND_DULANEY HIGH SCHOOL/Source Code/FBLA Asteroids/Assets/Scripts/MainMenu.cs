using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play(){ //These are just methods for each button, we could have placed a parameter, but it's slightly less readable that way, plus this isn't messy either
        AudioManager.Instance.PlaySound(SoundEffects.ButtonSelect);
        SceneManager.LoadScene(1);
    }

    public void Instructions(){
        AudioManager.Instance.PlaySound(SoundEffects.ButtonSelect);
        SceneManager.LoadScene(7);   
    }

    public void Credits(){
        AudioManager.Instance.PlaySound(SoundEffects.ButtonSelect);
        SceneManager.LoadScene(8);   
    }

    public void Hall()
    {
        AudioManager.Instance.PlaySound(SoundEffects.ButtonSelect);
        SceneManager.LoadScene(12);
    }

    public void Exit(){
        AudioManager.Instance.PlaySound(SoundEffects.ButtonSelect);
        Application.Quit();
    }
}
