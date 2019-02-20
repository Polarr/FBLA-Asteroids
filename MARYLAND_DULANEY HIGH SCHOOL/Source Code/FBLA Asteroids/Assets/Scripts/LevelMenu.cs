using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button lv1;
    public Button lv2;
    public Button lv3;
    public Button lv4;
    public Button lv5;

    private void Start(){
        if (PlayerPrefs.GetInt("Level 1") == 1){
            lv1.interactable = false;
            lv1.GetComponentInChildren<Text>().text = "Completed";
        }
        if (PlayerPrefs.GetInt("Level 2") == 1){
            lv2.interactable = false;
            lv2.GetComponentInChildren<Text>().text = "Completed";
        }
        if (PlayerPrefs.GetInt("Level 3") == 1){
            lv3.interactable = false;
            lv3.GetComponentInChildren<Text>().text = "Completed";
        }
        if (PlayerPrefs.GetInt("Level 4") == 1){
            lv4.interactable = false;
            lv4.GetComponentInChildren<Text>().text = "Completed";
        }
        if (PlayerPrefs.GetInt("Level 5") == 1){
            lv5.interactable = false;
            lv5.GetComponentInChildren<Text>().text = "Completed";
        }
    }

    public void LoadLevel(int num){
        AudioManager.Instance.PlaySound(SoundEffects.ButtonSelect);
        SceneManager.LoadScene(1 + num);
    }

    public void Back(){
        AudioManager.Instance.PlaySound(SoundEffects.ButtonSelect);
        SceneManager.LoadScene(0);
    }
}
