using UnityEngine;
using System;
using System.Collections;

/**
Do NOT use co routines for things you need fixed updates on: WaitForSeconds(t) means "wait until the first frame after t time has passed", not "wait exactly t seconds". So in WaitForSeconds(t), t is a lower bound on the amount of time that will pass, not an exact amount. The difference between that lower bound and the time the coroutine steps next on is dependent on the framerate.
WaitForSeconds(t) means "wait until the first frame after t time has passed", not "wait exactly t seconds". So in WaitForSeconds(t), t is a lower bound on the amount of time that will pass, not an exact amount. The difference between that lower bound and the time the coroutine steps next on is dependent on the framerate.
*/
public class TimeoutCoroutine : MonoBehaviour {

    public IEnumerator RunSomething(TimeoutCoroutineRequest request) {
        Coroutine test = request.CoroutineWrapper();

        for (int i = 0; i < request.Iterations; i++) {
            yield return new WaitForSeconds(request.Seconds);
        }

        StopCoroutine(test);
    }


}
