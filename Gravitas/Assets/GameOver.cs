using UnityEngine;
using System.Collections;
using TMPro;

public class GameOver : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip gameOverSFX;
    [SerializeField] private AudioClip gameOverMusic;


    [Header("UI")]
    [SerializeField] private TMP_Text timerText; // Assign in Inspector

    [SerializeField] private Timer timer;

    [SerializeField] private GameObject GameplayUI;

    private AudioSource audioSource;
    private bool triggered;

    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.ignoreListenerPause = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            GameplayUI.SetActive(false);
            // ⏱️ Show final time
            if (timerText != null)
                timerText.text = timer.Get();

            // 🛑 Stop game + other SFX
            GameManager.GameOver();

            // 🔊 Play audio
            StartCoroutine(PlayGameOverAudio());
        }
    }

    IEnumerator PlayGameOverAudio()
    {
        if (gameOverSFX != null)
        {
            audioSource.clip = gameOverSFX;
            audioSource.loop = false;
            audioSource.Play();

            yield return new WaitForSecondsRealtime(gameOverSFX.length);
        }

        if (gameOverMusic != null)
        {
            audioSource.clip = gameOverMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}
