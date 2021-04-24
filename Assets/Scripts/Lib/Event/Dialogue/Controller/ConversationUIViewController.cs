using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ConversationUIViewController : MonoBehaviour {

    //TODO: Still maybe, Dialogue collections could be in their own class, maybe View class
    public Text DialogueText;
    public Button NextButton;

    public Guid ConversationId;

    public DialogueController DialogueController;

    void Start() {
        EventSystem.Instance.OnCharSequenceEvent += HandleCharSequenceEvent;
        EventSystem.Instance.OnIniateCharSequenceEvent += HandleInitiateConversationEvent;
        NextButton.gameObject.SetActive(false);
    }

    public void HandleInitiateConversationEvent(object? sender, InitiateCharSequenceEventArgs eventArgs) {
        Debug.Log("initated");
        this.ConversationId = eventArgs.ConversationId;
        DialogueController.ResetConversation(this);
    }

    public void HandleEndConversationEvent(object? sender, CancelCharSequenceEventArgs eventArgs) {

    }

    public void NextLine() {
        DialogueController.NextLine(this);
        DialogueText.text = "";
        AdvanceCharSequenceEventArgs eventArgs = new AdvanceCharSequenceEventArgs(System.Guid.NewGuid(), ConversationId);
        EventSystem.Instance.HandleEvent(this, eventArgs);
    }

    public void HandleCharSequenceEvent(object? sender, CharSequenceEventArgs eventArgs) {
        if (eventArgs.ConversationId == ConversationId) {
            DialogueController.HandleCharSequenceEvent(this, eventArgs);
        }
    }

}
