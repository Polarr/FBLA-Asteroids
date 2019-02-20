using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f; //The amount in units the player will move in per update

    public float timeBetweenShots = 0.5f; //The time in seconds, between each shot
    public float timeSinceLastShot = 0; //The time since the last shot
    public GameObject projectilePrefab;
    
    public Animator animator;

    public int health = 10;

    void Start()
    {
        //PlayerPrefs.GetInt("PlayerMovement", 0);
    }

    //Update is called once per frame
    private void Update()
    {
        if (health <= 0){ //if we have no health
            AudioManager.Instance.PlaySound(SoundEffects.PlayerDeath);
            Destroy(gameObject); //destory the player ship
            SceneManager.LoadScene(9);
        }
        HandleMovement(); //Function to handle the movement of the player
        HandleShoot();
    }

    private void HandleShoot(){

        if (timeSinceLastShot > timeBetweenShots && Input.GetKey(KeyCode.Space)){ //If the time since the last shot is greater than the time between shots
            var proj = Instantiate(projectilePrefab, position: transform.position, rotation: Quaternion.Euler(0, 0, 0)); //Shoot
            proj.GetComponent<ProjectileController>().isPlayer = true;
            AudioManager.Instance.PlaySound(SoundEffects.Shoot);
            timeSinceLastShot = 0; //set the time since last shot to 0
        }

        timeSinceLastShot += Time.deltaTime; //Add the time since the last frame to the time since the last shot
    }

    private void HandleMovement(){
        float mouseX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;

        /*
        Time.deltaTime is the time since the last frame, since frames are inconsistent,
        you must multiply the speed by the delta time to account for the differences of each frame
         */
        if (Mathf.Abs(mouseX - transform.position.x) > 0.1f){
            if (mouseX > transform.position.x){ //If the mouse position is to the right of the player
                transform.Translate(Vector2.right * speed * Time.deltaTime); //Move to the right by "speed" units 
                animator.SetInteger("horizontal", 1);
            }
            else if (mouseX < transform.position.x){ //Mouse position is to the left of the player
                transform.Translate(Vector2.left * speed * Time.deltaTime); //Move to the right by "speed" units 
                animator.SetInteger("horizontal", -1);
            }
        }
        else
            animator.SetInteger("horizontal", 0);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.transform.tag == "Projectile" && !other.gameObject.GetComponent<ProjectileController>().isPlayer ){ //if a enemy projectile hits
            health--;
            Destroy(other.gameObject);
        }
    }
}
