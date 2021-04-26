using UnityEngine;
using System;
using System.Collections;

/**
Do NOT use co routines for things you need fixed updates on: WaitForSeconds(t) means "wait until the first frame after t time has passed", not "wait exactly t seconds". So in WaitForSeconds(t), t is a lower bound on the amount of time that will pass, not an exact amount. The difference between that lower bound and the time the coroutine steps next on is dependent on the framerate.
WaitForSeconds(t) means "wait until the first frame after t time has passed", not "wait exactly t seconds". So in WaitForSeconds(t), t is a lower bound on the amount of time that will pass, not an exact amount. The difference between that lower bound and the time the coroutine steps next on is dependent on the framerate.
*/
public class ChainableTimeoutCoroutine : MonoBehaviour {
    public TimeoutCoroutine timeoutCoroutine;

    public IEnumerator chain(TimeoutCoroutineRequest[] requests) {

        foreach (TimeoutCoroutineRequest request in requests)
            yield return StartCoroutine(timeoutCoroutine.RunSomething(request));

    }
    
}