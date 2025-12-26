using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static bool isGameOver;
    [SerializeField] private GameObject gameOverScreen;

    void Awake()
    { 
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        isGameOver = false;
        if (gameOverScreen != null)
            gameOverScreen.SetActive(false);

        Time.timeScale = 1f;
    }

    public static void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        if (Instance.gameOverScreen != null)
            Instance.gameOverScreen.SetActive(true);

        AudioSource[] sources = Object.FindObjectsOfType<AudioSource>();
        foreach (AudioSource source in sources)
        {
            source.Stop();
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }
}
