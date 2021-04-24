using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


/// This class will probably need a refactor lol
public class DialogueCreator : MonoBehaviour, EventPublisher<CharSequenceEventArgs> {

    private Guid LastAcked;
    private Guid LastOutgoing;

    private EventSystem EventSystem;

    private Coroutine ActiveDialogue;

    private Conversation Conversation;
    private IEnumerator<string> ConversationEnumerator;

    void Start() {
        Debug.Log(EventSystem.Instance);
        EventSystem.Instance.OnAckableEvent += HandleAck;
        EventSystem.Instance.OnAdvanceCharSequenceEvent += HandleAdvanceConversation;
        //NextLine("this is a test");
        string[] dialogues = {"thisis a test", "here is the next line"};
        Conversation Convo = new Conversation() {
            Dialogue = dialogues,
            Id = System.Guid.NewGuid()
        };

        BeginConversation(Convo);
    }

    void FixedUpdate() {
        return;
    }

    #nullable enable
    public void HandleAck(object? from, AckableEventArgs eventArgs) {
        LastAcked = eventArgs.EventId;
        //Debug.Log("It worked! " + LastAcked + " " + LastOutgoing + " " + eventArgs.EventId);
    }

    // TODO: Make into a setter? or extend functionality?
    public void BeginConversation(Conversation conversation) {
        this.Conversation = conversation;
        ConversationEnumerator = Conversation.ReadConversation();
        PublishEvent(new InitiateCharSequenceEventArgs(System.Guid.NewGuid(), conversation.Id));
        NextLine();
    }

    public void PublishEvent(CharSequenceEventArgs eventArgs) {
        LastOutgoing = eventArgs.EventId;
        EventSystem.Instance.HandleEvent(this, eventArgs);
    }

    public void PublishEvent(InitiateCharSequenceEventArgs eventArgs) {
        LastOutgoing = eventArgs.EventId;
        EventSystem.Instance.HandleEvent(this, eventArgs);
    }

    public void HandleAdvanceConversation(object? from, AdvanceCharSequenceEventArgs eventArgs) {
        Debug.Log("Convo ids: " + eventArgs.ConversationId + ": " + this.Conversation.Id);
        if (eventArgs.ConversationId == this.Conversation.Id) {
            NextLine();
        }
    }

    public void NextLine() {
        Debug.Log("yes new line");
        if (ActiveDialogue != null)
            StopCoroutine(ActiveDialogue);

        if (ConversationEnumerator != null && ConversationEnumerator.MoveNext()) {
            Debug.Log("I am here in NextLine: " + ConversationEnumerator.Current + " " + Conversation.Id);
            ActiveDialogue = StartCoroutine(SendDialogue(ConversationEnumerator.Current));
        }
        else
            StopCoroutine(ActiveDialogue);
            PublishEvent(new CharSequenceEventArgs("END", System.Guid.NewGuid(), CharSequenceEventArgs.Type.End, CharSequenceEventArgs.PrintMode.SingleChar, Conversation.Id));
    }

    public void CancelDialogue() {
        if (ActiveDialogue != null)
            StopCoroutine(ActiveDialogue);
        
        PublishEvent(new CharSequenceEventArgs("endDialogue", System.Guid.NewGuid(), CharSequenceEventArgs.Type.End, CharSequenceEventArgs.PrintMode.FullLine, Conversation.Id));
    }

    private IEnumerator SendDialogue(string dialogueLine) {
        // send the full String for other use;
        PublishEvent(new CharSequenceEventArgs(dialogueLine, System.Guid.NewGuid(), CharSequenceEventArgs.Type.CharSequence, CharSequenceEventArgs.PrintMode.SingleChar, Conversation.Id));

        foreach(char ch in dialogueLine.ToCharArray()) {
      
            char[] character = {ch};
            PublishEvent(new CharSequenceEventArgs(new string(character), System.Guid.NewGuid(), CharSequenceEventArgs.Type.CharSequence, CharSequenceEventArgs.PrintMode.SingleChar, Conversation.Id));
            Debug.Log("Sending: " + new string(character));
            yield return new WaitForSeconds(0.1f);
        }

        PublishEvent(new CharSequenceEventArgs("\n", System.Guid.NewGuid(), CharSequenceEventArgs.Type.NewLine, CharSequenceEventArgs.PrintMode.SingleChar, Conversation.Id));
        yield break;
    }

}
