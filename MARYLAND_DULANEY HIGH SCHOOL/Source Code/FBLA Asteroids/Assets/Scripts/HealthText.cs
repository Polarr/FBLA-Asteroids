using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    public PlayerController player;
    public TextMeshProUGUI text;

    void Update()
    {
        text.text = "Health: " + player.health;
    }
}
