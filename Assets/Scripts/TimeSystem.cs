using System;
using System.Collections;
using Survey;
using UnityEngine;

public class TimeSystem : MonoBehaviour
{
    public static TimeSystem Instance { get; set; }
    public string CurrentTime { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }

        Destroy(gameObject);
    }

    //Coroutine for calculating the time of passing the test
    public IEnumerator TimerCoroutine()
    {
        var elapsedTime = 0f;

        while (true)
        {
            var time = TimeSpan.FromSeconds(elapsedTime);

            SurveyScreen.Instance.TimeLabel.text = time.ToString("mm\\:ss");
            CurrentTime = time.ToString("mm\\:ss");
            yield return new WaitForSeconds(1f);
            
            elapsedTime += 1;
            
        }
    }
}
