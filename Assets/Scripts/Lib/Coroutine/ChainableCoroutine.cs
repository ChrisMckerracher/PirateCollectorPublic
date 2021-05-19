using System;
using System.Collections;
using UnityEngine;

public class ChainableCoroutine : MonoBehaviour
{
    public IEnumerator Chain(Func<Coroutine>[] requests)
    {
        //This should not be a while loop, and was left in for testing ToDo: Remove this
        // ToDo have some way to communicate that the chain is complete
        while (true)
            foreach (var request in requests)
            {
                yield return new WaitForSeconds(Time.fixedDeltaTime);
                yield return request();
            }
    }
}