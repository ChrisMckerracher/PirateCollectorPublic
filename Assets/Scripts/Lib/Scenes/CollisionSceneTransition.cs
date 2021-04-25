using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionSceneTransition : SceneTransition {

    public string CollisionTag;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag(CollisionTag)) {
            SceneManager.LoadScene(SceneToLoad);
        }
        
    }
}
