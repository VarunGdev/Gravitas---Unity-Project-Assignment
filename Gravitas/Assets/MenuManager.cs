using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public AudioSource SelectaudioSource;

    public void QuitGame()
    {
        SelectaudioSource.Play();
        Application.Quit();
    }

    public void NextScene()
    {
        SelectaudioSource.Play();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void Retry()
    {
        SelectaudioSource.Play();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
