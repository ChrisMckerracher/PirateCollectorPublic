using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// By introducing an abstraction of a conversation early, refactors and new functionality are easy to handle
public class Conversation {
    public string[] Dialogue;
    public Guid Id;

    public IEnumerator<string> ReadConversation() {
        foreach (string dialogueLine in Dialogue) {
            yield return dialogueLine;
        }

        yield return null;
    }

}
