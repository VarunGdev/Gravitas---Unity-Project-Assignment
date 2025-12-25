using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private int scoreValue = 1;

    [Header("Audio")]
    [SerializeField] private AudioClip pickupSFX;
    [SerializeField] private float volume = 1f;

    public scoreManager ScoreManager;
    private bool collected = false;


    void OnTriggerEnter(Collider other)
    {
        if (collected) return;

        if (other.CompareTag("Player"))
        {
            collected = true;

            // Increase score
            ScoreManager.AddScore(scoreValue);

            // Play sound at pickup position (safe even after destroy)
            if (pickupSFX != null)
            {
                AudioSource.PlayClipAtPoint(pickupSFX, transform.position, volume);
            }

            // Destroy collectible
            Destroy(gameObject);
        }
    }
}
