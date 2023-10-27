using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerShipController : MonoBehaviour
{
    public float speed = 5.0f;
    public Vector2 boundaryMin;
    public Vector2 boundaryMax;
    public GameObject laserBulletPrefab;
    public float bulletSpeed = 10.0f;
    public Transform bulletSpawnPoint;
    public int playerLives = 3;
    public Text livesText;
    public float respawnTime = 1.0f; // Time in seconds for respawn
    public float temporaryInvincibilityTime = 5.0f; // Time in seconds for temporary invincibility
    private bool isRespawning = false;
    private bool isGameOver = false;
    public Text gameOverText;
    public Text invincibilityTimeText;
    private float invincibilityTimeRemaining;
    private AudioSource musicPlayer;

    // Scoring variables
    public Text currentTimeText;
    public Text bestTimeText;
    public Text newPersonalBestText;
    private float startTime;
    private float currentTime;
    private float bestTime;
    private bool newPersonalTime;


    void Start()
    {
        

        // Hide the invincibility time text
        invincibilityTimeText.gameObject.SetActive(false);

        // Initialize scoring variables
        startTime = Time.time;
        bestTime = PlayerPrefs.GetFloat("BestTime", 0);
        UpdateScoreUI();

        UpdateLivesUI();
        gameOverText.gameObject.SetActive(false); // Hide the Game Over text

        // Find the MusicPlayer game object
        GameObject musicPlayerObject = GameObject.Find("MusicPlayer");
        if (musicPlayerObject != null)
        {
            // Get the AudioSource component
            musicPlayer = musicPlayerObject.GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
{  
    if (!isRespawning)
    {
        float horizontalInput = Input.GetAxis("Vertical");
        float verticalInput = -Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(movement * speed * Time.deltaTime);

        // Apply boundary constraints
        float clampedX = Mathf.Clamp(transform.position.x, boundaryMin.x, boundaryMax.x);
        float clampedY = Mathf.Clamp(transform.position.y, boundaryMin.y, boundaryMax.y);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);

        // Shoot bullets
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
        
        if (!isGameOver)
        {
            // Update current time
            currentTime = Time.time - startTime;
            if (currentTime > bestTime)
            {
                bestTime = currentTime;
                PlayerPrefs.SetFloat("BestTime", bestTime);

                // Show new personal best text
                if (newPersonalBestText != null)
                {
                    float newPersonalTime = currentTime;
                    newPersonalBestText.text = "New Personal Best!";
                    StartCoroutine(ClearNewPersonalBestText());
                }
            }
            
            UpdateScoreUI();
        }
    }
    else
    {
        // Disable player movement during respawn time
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

    // Check if game is over
    if (isGameOver && Input.GetKeyDown(KeyCode.R))
    {
        RestartGame();
    }
}

    // Creating a Fire method to shoot bullets
    void Fire()
    {
        GameObject bullet = Instantiate(laserBulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = Vector2.up * bulletSpeed;
    }

    IEnumerator TemporaryInvincibility()
{
    // Show invincibility time text
    invincibilityTimeText.gameObject.SetActive(true);
    // Disable player's collider during invincibility time
    GetComponent<Collider2D>().enabled = false;

    // Set invincibility time remaining to full duration
    invincibilityTimeRemaining = temporaryInvincibilityTime;

    // Update invincibility time text
    UpdateInvincibilityTimeText();

    // Wait for a few seconds
    while (invincibilityTimeRemaining > 0)
    {
        yield return new WaitForSeconds(0.1f);

        // Decrease invincibility time remaining
        invincibilityTimeRemaining -= 0.1f;

        // Update invincibility time text
        UpdateInvincibilityTimeText();
    }

    // Enable player's collider after invincibility time
    GetComponent<Collider2D>().enabled = true;

    // Reset respawn flag
    isRespawning = false;

    // Remove invincibility time text
    invincibilityTimeText.gameObject.SetActive(false);
}

void UpdateInvincibilityTimeText()
{
    // Update invincibility time text
    invincibilityTimeText.text = "Invincibility Time: " + invincibilityTimeRemaining.ToString("F1");
}

    void TakeDamage()
{
    playerLives--;

    if (playerLives <= 0)
    {
        GameOver();
    }
    else
    {
        isRespawning = true;
        StartCoroutine(Respawn());
    }

    UpdateLivesUI();
}

    IEnumerator Respawn()
    {
        // Disable enemy spawner during respawn time
        GameObject.Find("EnemyWaveSpawner").GetComponent<EnemyWaveSpawner>().enabled = false;
        // Get sprite renderer component
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        // Set player ship to active
        gameObject.SetActive(true);

        // Set ship opacity to 0%
        spriteRenderer.color = new Color(1f, 1f, 1f, 0f);

        // Change order in layer to -20
        spriteRenderer.sortingOrder = -20;

        yield return new WaitForSeconds(respawnTime);

        // Reset player position
        transform.position = new Vector3(0, -3, 0);

        // Set ship opacity to 100%
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);

        // Change order in layer to 0
        spriteRenderer.sortingOrder = 0;

        // Start temporary invincibility coroutine
        StartCoroutine(TemporaryInvincibility());

        // Enable enemy spawner after respawn time
        GameObject.Find("EnemyWaveSpawner").GetComponent<EnemyWaveSpawner>().enabled = true;

        // Reset respawn flag
        isRespawning = false;
}

    void UpdateLivesUI()
    {
        if (livesText != null)
        {
            livesText.text = "Lives: " + playerLives;
        }
    }

    void GameOver()
    {
        isGameOver = true;
        gameOverText.gameObject.SetActive(true);

        // Stop the music
        if (musicPlayer != null)
        {
            musicPlayer.Stop();
        }
    }

    void RestartGame()
    {
        playerLives = 3;
        isGameOver = false;
        UpdateLivesUI();
        gameOverText.gameObject.SetActive(false); // Deactivate the gameOverText object
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            TakeDamage();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();
            Destroy(other.gameObject);
        }
    }

    void UpdateScoreUI()
    {
        if (currentTimeText != null)
        {
            currentTimeText.text = "Time: " + currentTime.ToString("F1");
        }

        if (bestTimeText != null)
        {
            bestTimeText.text = "Best Time: " + bestTime.ToString("F1");
        }
    }

    IEnumerator ClearNewPersonalBestText()
    {
        yield return new WaitForSeconds(2.0f);
        newPersonalBestText.text = "";

        // Clear new personal best
        if (newPersonalBestText != null)
        {
            newPersonalBestText.text = "";
        }
    }

    
}
