using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogueController : MonoBehaviour {

    void Start() {
    }

    public void ResetConversation(ConversationUIViewController controller) {
        controller.NextButton.gameObject.SetActive(true);
        controller.EndButton.gameObject.SetActive(false);
        controller.DialogueText.text = "";
    }

    public void NextLine(ConversationUIViewController controller) {
        controller.DialogueText.text = "";
    }

    public void HandleCharSequenceEvent(ConversationUIViewController controller, CharSequenceEventArgs eventArgs) {
        string charData = eventArgs.CharData;
        if (eventArgs.SequenceType.Equals(CharSequenceEventArgs.Type.End)) {
            controller.NextButton.gameObject.SetActive(false);
            controller.EndButton.gameObject.SetActive(true);
            controller.DialogueText.text = "ToDo: Don't have a new dialogue line for ending dialogue!!";
        }
        if (eventArgs.SequenceType.Equals(CharSequenceEventArgs.Type.NewLine)) {
            controller.NextButton.gameObject.SetActive(true);
            controller.EndButton.gameObject.SetActive(false);
            return;
        }

        if (eventArgs.SequencePrintMode.Equals(CharSequenceEventArgs.PrintMode.FullLine)) {
            controller.DialogueText.text = charData;
            controller.NextButton.gameObject.SetActive(false);
            controller.EndButton.gameObject.SetActive(false);
        } else if (charData.Length > 1) {
            // Currently whole line is just for debug
            return;
        } else {
            controller.DialogueText.text += charData;
            controller.NextButton.gameObject.SetActive(false);
            controller.EndButton.gameObject.SetActive(false);
        }
    }

}
