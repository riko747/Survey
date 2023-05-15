using System;
using System.Collections;
using UnityEngine;

public class TimeSystem : MonoBehaviour
{
    private void Awake()
    {
        throw new NotImplementedException();
    }

    IEnumerator TimeCountCoroutine()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1);
        }
    }
}
