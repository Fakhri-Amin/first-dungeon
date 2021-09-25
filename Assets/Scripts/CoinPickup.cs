using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] int coinScore = 100;
    [SerializeField] AudioClip coinPickupSFX;

    private void OnTriggerEnter2D(Collider2D other)
    {
        AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
        FindObjectOfType<GameSession>().AddToScore(coinScore);
        Destroy(gameObject);
    }
}
