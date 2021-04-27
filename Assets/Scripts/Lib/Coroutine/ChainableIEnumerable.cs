using UnityEngine;
using System;
using System.Collections;
public class ChainableIEnumerable : MonoBehaviour{

    public IEnumerable chain(Func<IEnumerable>[] requests) {
        //This should not be a while loop, and was left in for testing ToDo: Remove this
        // ToDo have some way to communicate that the chain is complete
        foreach (Func<IEnumerable> request in requests) {
            foreach (object? x in request()) {
                yield return x;
            }
        }
    }

}