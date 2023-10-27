using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour
{
    public float scrollSpeed = 1.0f;

    // Update is called once per frame
    void Update()
    {
        // Move the tutorial text down the screen
        transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);

        // Destroy the tutorial text when it is off the screen
        if (transform.position.y < -10.0f)
        {
            Destroy(gameObject);
        }
    }
}
