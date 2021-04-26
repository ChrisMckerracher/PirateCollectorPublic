using UnityEngine;
using System;
using System.Collections;
public class TimeoutCoroutineRequest {

    public readonly float Seconds;
    public readonly int Iterations;
    // CoRoutine Wrapper should look like this () -> { return StartCoroutine(ActualCoroutine(its methods))}
    public readonly Func<Coroutine> CoroutineWrapper;

    public TimeoutCoroutineRequest(float seconds, int iterations, Func<Coroutine> coroutineWrapper) {
        this.Seconds = seconds;
        this.Iterations = iterations;
        this.CoroutineWrapper = coroutineWrapper;
    }

}