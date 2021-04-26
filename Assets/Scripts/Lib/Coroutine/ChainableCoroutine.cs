using UnityEngine;
using System;
using System.Collections;

public class ChainableCoroutine : MonoBehaviour {

    public IEnumerator chain(Func<Coroutine>[] requests) {
        //This should not be a while loop, and was left in for testing ToDo: Remove this
        // ToDo have some way to communicate that the chain is complete
        while(true) {
            foreach (Func<Coroutine> request in requests)
                yield return request();
        }

    }

}
