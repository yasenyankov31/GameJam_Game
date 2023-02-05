using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float time = 0;
    public float Minutes;
    public float Seconds;
    public Text timeText;

    void Update()
    {
        time += Time.deltaTime;
        DisplayTime(time);
    }

    void DisplayTime(float timeToDisplay)
    {
        Minutes = Mathf.FloorToInt(timeToDisplay / 60);
        Seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("\n\n\n{0:00}:{1:00}", Minutes, Seconds);
    }
}