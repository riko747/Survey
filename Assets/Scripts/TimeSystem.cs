using System;
using System.Collections;
using Survey;
using UnityEngine;

public class TimeSystem : MonoBehaviour
{
    public static TimeSystem Instance { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }

        Destroy(gameObject);
    }

    public IEnumerator TimerCoroutine()
    {
        var elapsedTime = 0f;

        while (true)
        {
            yield return new WaitForSeconds(1f);
            
            elapsedTime += 1;
            var time = TimeSpan.FromSeconds(elapsedTime);

            SurveyScreen.Instance.TimeLabel.text = time.ToString("mm\\:ss");
        }
    }
}
