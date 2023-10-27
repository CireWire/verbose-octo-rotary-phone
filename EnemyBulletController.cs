using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{

    public Vector2 boundaryMin;
    public Vector2 boundaryMax;

    // Update is called once per frame
    void Update()
    {
        // Check if bullet is outside of the boundary
        if (transform.position.x < boundaryMin.x || transform.position.x > boundaryMax.x ||
            transform.position.y < boundaryMin.y || transform.position.y > boundaryMax.y)
        {
            Destroy(gameObject); // Destroy the bullet
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Disable the bullet and the player
            Destroy(gameObject); // Disable the bullet
        }
        else if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            // Disable the bullet
            Destroy(gameObject);

            // Disable the player bullet
            Destroy(collision.gameObject);
        }
        else
        {
            // Disable the bullet
            Destroy(gameObject);
        }
    }

    
}
