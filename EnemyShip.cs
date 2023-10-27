using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public int maxHealth = 1;
    public GameObject enemyBulletPrefab;
    public Transform enemyBulletSpawner;
    public float enemyBulletSpeed = 10.0f;

    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        InvokeRepeating("Fire", 1.0f, 2.0f); // Fire every 2 seconds after 1 second delay
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckBounds();
    }

    void Move()
    {
        // Implement enemy ship movement
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);

        // Implement enemy ship horizontal movement
        float horizontalMovementFactor = 2.0f; // Adjust this value to control the horizontal movement speed
        float horizontalMovement = Mathf.Cos(Time.time) * moveSpeed * horizontalMovementFactor;
        transform.Translate(new Vector3(horizontalMovement, 0, 0) * Time.deltaTime);

        // Implement boundary constraints
        float clampedX = Mathf.Clamp(transform.position.x, -8.0f, 8.0f);
    }

    void CheckBounds()
    {
        // Implement boundary constraints for bottom part of the screen
        if (transform.position.y < -5.0f)
        {
            Destroy(gameObject);
        }
    }

    void Fire()
    {
        // Implement enemy ship firing
        GameObject newBullet = Instantiate(enemyBulletPrefab, enemyBulletSpawner.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, enemyBulletSpeed);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    
}
