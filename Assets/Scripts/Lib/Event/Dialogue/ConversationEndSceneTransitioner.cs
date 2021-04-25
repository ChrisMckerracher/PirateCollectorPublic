using System;
using UnityEngine;

public class ConversationEndSceneTransitioner : MonoBehaviour {

    public SceneTransition SceneTransition;

    void Start() {
        EventSystem.Instance.OnCancelCharSequenceEvent += HandleSceneTransition;
    }

    public void HandleSceneTransition(object? sender, CancelCharSequenceEventArgs eventArgs) {
        Debug.Log("trying to transition");
        SceneTransition.TransitionScene();
    }

}
