using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBigController : MonoBehaviour, IEnemyController
{
    public GameObject projectilePrefab;
    public float timeBetweenShots = 1.5f; //The time in seconds, between each shot
    public bool hasShot = false; 

    private GameObject player;
    public Vector2 targetPos;
    public float speed = 2;

    private int health = 2;

    public string answer;
    public bool isAnswer = false;

    public void OnSpawn(Vector2 newTargetPos, string newAnswer, int newHealth, bool newIsAnswer)
    {
        player = FindObjectOfType<PlayerController>().gameObject; //Get a reference to the player object
        transform.position = new Vector2(0, 10);
        targetPos = newTargetPos;

        answer = newAnswer;
        health = newHealth;

        transform.GetChild(1).GetComponent<TextMeshPro>().text = answer;
        isAnswer = newIsAnswer;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0){ //if we have no health
            if (!isAnswer){
                FindObjectOfType<PlayerController>().health -= 2;
                LevelController.Instance.enemies.Remove(gameObject);
                AudioManager.Instance.PlaySound(SoundEffects.Error);
                Destroy(gameObject); //destory the ship
            }
            else {
                foreach(GameObject e in LevelController.Instance.enemies.ToArray()){
                    Destroy(e);
                    LevelController.Instance.enemies.Remove(e);
                }
                if (LevelController.Instance.questionsToAsk.Count > 0){ //check if there's any questions
                    LevelController.Instance.questionsToAsk.RemoveAt(0);
                    AudioManager.Instance.PlaySound(SoundEffects.Success);
                    LevelController.Instance.SetQuestion(true);
                }
            }
        }
        HandleMove();
        if ((Vector2)transform.position == targetPos && !hasShot)
            StartCoroutine(HandleShoot());
        if (player != null)
            LookAtPlayer();
    }

    private void HandleMove(){
        float step = Time.deltaTime * speed; //Used to keep constant keep with Time.deltaTime
        transform.position = Vector2.MoveTowards(transform.position, targetPos, step); //move towards target position
    }

    private void LookAtPlayer(){
        Vector2 difference = player.transform.position - transform.position; //need difference from target position to our position
        float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; //use atan2 to get the radian, use rad2deg to convert to degrees

        transform.GetChild(0).rotation = Quaternion.Euler(0, 0, angle - 90); //add 90 to get proper angle
        /*
            Additionally, the front of the enemy sprite is at the bottom
            so, on the gameobject, the y value is inversed so the front of the ship is at the top
        */
    }

    private IEnumerator HandleShoot(){
        hasShot = true;
        while (true){
            for (int x = 0; x < 360; x+=20){
                Instantiate(projectilePrefab, position: transform.position, 
                    rotation: Quaternion.Euler(0, 0, x));
                
                //AudioManager.Instance.PlaySound(SoundEffects.Shoot);

                yield return new WaitForSeconds(timeBetweenShots);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.transform.tag == "Projectile" && other.gameObject.GetComponent<ProjectileController>().isPlayer ){ //If a player projectile collides with the player
            health--; //subtract one from health
            Destroy(other.gameObject); //destroy the projectile
        }
    }
}
