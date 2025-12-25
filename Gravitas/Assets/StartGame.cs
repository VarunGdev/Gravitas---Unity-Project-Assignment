using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject GameplayUI;
    public AudioSource audioSource;
    public AudioSource audioSource2;


    void Start()
    {
        GameplayUI.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        startScreen.SetActive(true);
        Time.timeScale = 0f; // pause game at start

    }

    public void ClickStart()
    {
        audioSource.Play();
        audioSource2.Play();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        startScreen.SetActive(false);
        GameplayUI.SetActive(true);
        Time.timeScale = 1f; // resume game
    }



}
