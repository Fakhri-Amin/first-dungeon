using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalScroll : MonoBehaviour
{
    [Tooltip("Game units per second")]
    [SerializeField] float scrollRate = 0.4f;

    [SerializeField] float maxY = 3.8f;
    [SerializeField] float minY = 1.2f;

    private bool hasReached = false;

    void Update()
    {
        if (hasReached == false)
        {
            GoUp();
        }
        else if (hasReached)
        {
            GoDown();
        }
    }

    private void GoUp()
    {
        float yMove = scrollRate * Time.deltaTime;
        if (transform.position.y < maxY)
        {
            transform.Translate(new Vector2(0f, yMove));
        }
        else
        {
            hasReached = true;
        }
    }

    private void GoDown()
    {
        float yMove = scrollRate * Time.deltaTime;
        if (transform.position.y > minY)
        {
            transform.Translate(new Vector2(0f, -yMove));
        }
        else
        {
            hasReached = false;
        }
    }
}
