using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroy the bullet and the enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject); // Destroy the enemy
            Destroy(gameObject); // Destroy the bullet
        }
        else if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            // Destroy the bullet
            Destroy(gameObject);

            // Destroy the enemy bullet
            Destroy(collision.gameObject);
        }
        else
        {
            Destroy(gameObject); // Destroy the bullet
        }
    }
}
