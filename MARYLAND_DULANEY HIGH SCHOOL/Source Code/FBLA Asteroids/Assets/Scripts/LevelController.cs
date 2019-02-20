using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public List<Wave> waves;
    public List<Question> questions;
    public List<Question> questionsToAsk;
    public int numWaves;

    public List<GameObject> enemies;
    public static LevelController Instance;

    public TextMeshProUGUI questionText;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        questionsToAsk = new List<Question>();
        enemies = new List<GameObject>();

        for (int x = 0; x < numWaves; x++){ //for each wave
            Question q = questions[UnityEngine.Random.Range(0, questions.Count)]; //generate a random question
            while (questionsToAsk.Contains(q)){ //make sure that the question has not been added yet
                q = questions[UnityEngine.Random.Range(0, questions.Count)];
            }
            questionsToAsk.Add(q); //add it
        }

        SetQuestion(false); //put the first question on the question board
    }

    private void Update(){
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(0); //load the menu if the user presses escape
    }

    public void SetQuestion(bool isTrue){
        if (isTrue){
            questionText.text = "Correct!";
        }
        if (questionsToAsk.Count > 0){
            enemies.Clear(); //clear the enemies first

            Question q = questionsToAsk[0]; //new question

            for (int x = 0; x < waves[questions.IndexOf(q)].enemies.Count; x++){ //instantiate all of the enemies from that question's wave

                var e = Instantiate(waves[questions.IndexOf(q)].enemies[x].prefab, position: new Vector2(0, 12), rotation: Quaternion.identity);
                e.GetComponent<IEnemyController>().OnSpawn(waves[questions.IndexOf(q)].enemies[x].position, q.answers[x], waves[questions.IndexOf(q)].enemies[x].health, x == q.answerIndex);

                enemies.Add(e); //add them to the enemies
            }

            questionText.text = q.question; //change the text up top
        }
        else{ //if there are no more questions
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1); //set the level to complete

            if (PlayerPrefs.GetInt("Level 1") == 1 && PlayerPrefs.GetInt("Level 2") == 1 && PlayerPrefs.GetInt("Level 3") == 1 && PlayerPrefs.GetInt("Level 4") == 1 && PlayerPrefs.GetInt("Level 5") == 1)
            {
                SceneManager.LoadScene(11); //if you've beaten all levels, you can enter the hall of fame
            }
            else
                SceneManager.LoadScene(10); //if you haven't go back to level select and beat them!
        }
    }
}

[Serializable] //each level will have waves
public class Wave{
    public List<SpawnPosition> enemies;
}

[Serializable]
public class SpawnPosition{ //where each enemy will be
    public Vector2 position;
    public GameObject prefab;
    public int health;
}

[Serializable]
public class Question{
    public string question;
    public List<string> answers;
    public int answerIndex;
}