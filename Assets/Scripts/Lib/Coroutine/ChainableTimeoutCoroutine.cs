using UnityEngine;
using System;
using System.Collections;

public class ChainableTimeoutCoroutine : MonoBehaviour {
    public TimeoutCoroutine timeoutCoroutine;

    public IEnumerator chain(TimeoutCoroutineRequest[] requests) {

        foreach (TimeoutCoroutineRequest request in requests)
            yield return StartCoroutine(timeoutCoroutine.RunSomething(request));

    }
}