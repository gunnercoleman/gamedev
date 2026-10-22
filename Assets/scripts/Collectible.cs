using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] AudioClip collectSound;
    [SerializeField] float volume = 1f;
    [SerializeField] GameObject collectEffect;

    ScoreManager scoreManager;

    void Start()
    {
        scoreManager = FindFirstObjectByType<ScoreManager>();

        if (scoreManager == null)
        {
            Debug.LogWarning("No ScoreManager found in the scene.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (scoreManager != null)
        {
            scoreManager.IncreaseScore();
        }

        if (collectSound != null)
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position, volume);
        }

        if (collectEffect != null)
        {
            Instantiate(collectEffect, transform.position, collectEffect.transform.rotation);
        }


        gameObject.SetActive(false);
    }
}