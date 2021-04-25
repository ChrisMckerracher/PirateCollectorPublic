using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour {
    public string SceneToLoad;

    public void TransitionScene() {
        SceneManager.LoadScene(SceneToLoad);
    }
    
}
