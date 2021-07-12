using System;
using System.Collections;
using UnityEngine;


public class DelayExecutor : MonoBehaviourSingleton<DelayExecutor>
{
    public void Execute(float delay, Action action)
    {
        StartCoroutine(ExecuteRoutine(delay, action));
    }

    private IEnumerator ExecuteRoutine(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);

        action?.Invoke();
    }
}
