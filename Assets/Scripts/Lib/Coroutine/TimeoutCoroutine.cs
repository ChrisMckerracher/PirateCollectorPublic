using UnityEngine;
using System;
using System.Collections;

// Do not use timeout coroutine for routines with a fixed end, as currently the timeout could last longer than the actual coroutine
public class TimeoutCoroutine : MonoBehaviour {

    public IEnumerator RunSomething(TimeoutCoroutineRequest request) {
        Coroutine test = request.CoroutineWrapper();

        for (int i = 0; i < request.Iterations; i++) {
            yield return new WaitForSeconds(request.Seconds);
        }

        StopCoroutine(test);
    }


}
