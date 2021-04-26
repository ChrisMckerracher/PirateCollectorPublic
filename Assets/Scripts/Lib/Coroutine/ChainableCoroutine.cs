using UnityEngine;
using System;
using System.Collections;

public class ChainableCoroutine : MonoBehaviour {

    public IEnumerator chain(Func<Coroutine>[] requests) {

        while(true) {
            foreach (Func<Coroutine> request in requests)
                yield return request();
        }

    }

}