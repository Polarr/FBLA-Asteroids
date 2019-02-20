using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Vector2 startPos;

    public bool isPlayer = false;

    private void Start(){
        startPos = transform.position;
    }

    private void Update()
    {
        transform.Translate(Vector2.up * 5 * Time.deltaTime); //Move up with delta time

        if (Vector2.Distance(transform.position, startPos) > 15) //If the bullet is outside of the screen
            Destroy(gameObject); //Destroy it
    }
}
