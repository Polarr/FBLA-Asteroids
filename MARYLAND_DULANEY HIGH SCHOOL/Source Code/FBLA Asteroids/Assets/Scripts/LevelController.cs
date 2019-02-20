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

        for (int x = 0; x < numWaves; x++){
            Question q = questions[UnityEngine.Random.Range(0, questions.Count)];
            while (questionsToAsk.Contains(q)){
                q = questions[UnityEngine.Random.Range(0, questions.Count)];
            }
            questionsToAsk.Add(q);
        }

        SetQuestion(false);
    }

    private void Update(){
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(0);
    }

    public void SetQuestion(bool isTrue){
        if (isTrue){
            questionText.text = "Correct!";
            //yield return new WaitForSeconds(0);
        }
        if (questionsToAsk.Count > 0){
            enemies.Clear();

            Question q = questionsToAsk[0];

            for (int x = 0; x < waves[questions.IndexOf(q)].enemies.Count; x++){

                var e = Instantiate(waves[questions.IndexOf(q)].enemies[x].prefab, position: new Vector2(0, 12), rotation: Quaternion.identity);
                e.GetComponent<IEnemyController>().OnSpawn(waves[questions.IndexOf(q)].enemies[x].position, q.answers[x], waves[questions.IndexOf(q)].enemies[x].health, x == q.answerIndex);

                enemies.Add(e);
            }

            questionText.text = q.question;
        }
        else{
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);

            if (PlayerPrefs.GetInt("Level 1") == 1 && PlayerPrefs.GetInt("Level 2") == 1 && PlayerPrefs.GetInt("Level 3") == 1 && PlayerPrefs.GetInt("Level 4") == 1 && PlayerPrefs.GetInt("Level 5") == 1)
            {
                SceneManager.LoadScene(11);
            }
            else
                SceneManager.LoadScene(10);
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