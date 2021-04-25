using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


/// This class will probably need a refactor still lol
public class ConversationController : MonoBehaviour {

    private DialogueEventPublisher DialogueEventPublisher;
    private EventSystem EventSystem;

    private Coroutine ActiveDialogue;

    private Conversation Conversation;
    private IEnumerator<string> ConversationEnumerator;

    void Start() {
        DialogueEventPublisher = new DialogueEventPublisher();
        EventSystem.Instance.OnAdvanceCharSequenceEvent += HandleAdvanceConversation;
        DemoConversation();
    }

    private void DemoConversation() {
        string[] dialogues = {"this is a test", "here is the next line"};
        Conversation Convo = new Conversation() {
            Dialogue = dialogues,
            Id = System.Guid.NewGuid()
        };

        BeginConversation(Convo);
    }

    public void BeginConversation(Conversation conversation) {
        this.Conversation = conversation;
        ConversationEnumerator = Conversation.ReadConversation();
        DialogueEventPublisher.PublishEvent(new InitiateCharSequenceEventArgs(System.Guid.NewGuid(), conversation.Id));
        NextLine();
    }

    public void HandleAdvanceConversation(object? from, AdvanceCharSequenceEventArgs eventArgs) {
        Debug.Log("Convo ids: " + eventArgs.ConversationId + ": " + this.Conversation.Id);
        if (eventArgs.ConversationId == this.Conversation.Id) {
            NextLine();
        }
    }

    public void NextLine() {
        if (ActiveDialogue != null)
            StopCoroutine(ActiveDialogue);

        if (ConversationEnumerator != null && ConversationEnumerator.MoveNext()) {
            ActiveDialogue = StartCoroutine(SendDialogue(ConversationEnumerator.Current));
        }
        else
            StopCoroutine(ActiveDialogue);
            DialogueEventPublisher.PublishEvent(new CharSequenceEventArgs("END", System.Guid.NewGuid(), CharSequenceEventArgs.Type.End, CharSequenceEventArgs.PrintMode.SingleChar, Conversation.Id));
    }

    public void CancelDialogue() {
        if (ActiveDialogue != null)
            StopCoroutine(ActiveDialogue);
        
        DialogueEventPublisher.PublishEvent(new CharSequenceEventArgs("endDialogue", System.Guid.NewGuid(), CharSequenceEventArgs.Type.End, CharSequenceEventArgs.PrintMode.FullLine, Conversation.Id));
    }

    private IEnumerator SendDialogue(string dialogueLine) {
        // send the full String for other use;
        DialogueEventPublisher.PublishEvent(new CharSequenceEventArgs(dialogueLine, System.Guid.NewGuid(), CharSequenceEventArgs.Type.CharSequence, CharSequenceEventArgs.PrintMode.SingleChar, Conversation.Id));

        foreach(char ch in dialogueLine.ToCharArray()) {
      
            char[] character = {ch};
            DialogueEventPublisher.PublishEvent(new CharSequenceEventArgs(new string(character), System.Guid.NewGuid(), CharSequenceEventArgs.Type.CharSequence, CharSequenceEventArgs.PrintMode.SingleChar, Conversation.Id));
            Debug.Log("Sending: " + new string(character));
            yield return new WaitForSeconds(0.1f);
        }

        DialogueEventPublisher.PublishEvent(new CharSequenceEventArgs("\n", System.Guid.NewGuid(), CharSequenceEventArgs.Type.NewLine, CharSequenceEventArgs.PrintMode.SingleChar, Conversation.Id));
        yield break;
    }

}
