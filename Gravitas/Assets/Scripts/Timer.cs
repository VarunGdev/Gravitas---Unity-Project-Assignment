using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float timeSpeed = 2f;
    private float elapsedTime;

    void Start()
    {
        elapsedTime = 0f;
        UpdateTimerText();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime * timeSpeed;
        UpdateTimerText();
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    public string Get()
    {
        return timerText.text;
    }
}
