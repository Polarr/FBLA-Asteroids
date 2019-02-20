using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsMenu : MonoBehaviour
{
    public void Back(){
        AudioManager.Instance.PlaySound(SoundEffects.ButtonSelect);
        SceneManager.LoadScene(0);
    }
}
