using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    public float scrollSpeed = 3.0f;
    public float tilingX = 1.0f;
    public float screenRatio = 1.0f;

    float imageHeight;
    float resetPosition;
    bool isDuplicate = false;

    void Start()
    {
        // Set the tiling of the material
        GetComponent<Renderer>().material.mainTextureScale = new Vector2(tilingX, 1.0f);

        // Calculate the tilingY value based on the size of the background image and the size of the screen
        imageHeight = GetComponent<Renderer>().bounds.size.y;
        float screenHeight = Camera.main.orthographicSize * 2.0f;
        float screenWidth = screenHeight * screenRatio;
        float tilingY = screenHeight / imageHeight;
        GetComponent<Renderer>().material.mainTextureScale = new Vector2(tilingX, tilingY);

        // Calculate the reset position based on the size of the background image and the number of times it is tiled vertically
        resetPosition = transform.position.y - imageHeight * GetComponent<Renderer>().material.mainTextureScale.y;
    }

    void Update()
    {
        // Move the background downward
        transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);

        // Check if the background has scrolled past the screen
        if (transform.position.y < resetPosition)
        {
            // Reset the position of the background to create an infinite loop
            transform.position = new Vector3(transform.position.x, transform.position.y + imageHeight * GetComponent<Renderer>().material.mainTextureScale.y, transform.position.z);

            // Duplicate the background off-camera
            if (!isDuplicate)
            {
                GameObject duplicate = Instantiate(gameObject, transform.position + new Vector3(0, imageHeight * GetComponent<Renderer>().material.mainTextureScale.y, 0), transform.rotation);
                duplicate.GetComponent<ScrollBackground>().isDuplicate = true;
            }
        }

        // Destroy the background if it is out of the camera view
        if (transform.position.y < -imageHeight * GetComponent<Renderer>().material.mainTextureScale.y)
        {
            Destroy(gameObject);
        }
    }
}
