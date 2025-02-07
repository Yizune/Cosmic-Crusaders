using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameOverTimer : MonoBehaviour
{
    public UnityEvent onTimerEnd;
    void OnEnable()
    {
        StartCoroutine("Timer");
    }

    IEnumerator Timer() {
        yield return new WaitForSeconds(5);
        onTimerEnd.Invoke();
    }

}
