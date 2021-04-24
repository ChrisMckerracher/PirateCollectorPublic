using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogueController : MonoBehaviour {

    // A separate class should likely hold these 3 things as a single abstraction
    public Text DialogueText;
    public Button NextButton;

    public Guid ConversationId;

    void Start() {
        EventSystem.Instance.OnCharSequenceEvent += HandleCharSequenceEvent;
        NextButton.gameObject.SetActive(false);
    }

    public void NextLine() {
        DialogueText.text = "";
        AdvanceCharSequenceEventArgs eventArgs = new AdvanceCharSequenceEventArgs(System.Guid.NewGuid(), ConversationId);
        EventSystem.Instance.HandleEvent(this, eventArgs);
    }

    public void HandleCharSequenceEvent(object? sender, CharSequenceEventArgs eventArgs) {
        ConversationId = eventArgs.ConversationId;
        string charData = eventArgs.CharData;
        if (eventArgs.SequenceType.Equals(CharSequenceEventArgs.Type.NewLine)) {
            Debug.Log("Recieved newline");
            NextButton.gameObject.SetActive(true);
            return;
        }

        if (eventArgs.SequencePrintMode.Equals(CharSequenceEventArgs.PrintMode.FullLine)) {
            return;
            DialogueText.text = charData;
            NextButton.gameObject.SetActive(false);
        } else if (charData.Length > 1) {
            // Currently whole line is just for debug and clearing lines
            return;
        } else {
            DialogueText.text += charData;
            NextButton.gameObject.SetActive(false);
        }
    }

}
