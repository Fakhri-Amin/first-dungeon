using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] bool islastDoor = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (islastDoor == true)
        {
            StartCoroutine("LoadWinGame");
        }
        else
        {
            StartCoroutine("LoadNextLevel");
        }
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(levelLoadDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator LoadWinGame()
    {
        yield return new WaitForSeconds(levelLoadDelay);
        FindObjectOfType<GameSession>().DestroySelf();
        SceneManager.LoadScene("Win Level");
    }
}
