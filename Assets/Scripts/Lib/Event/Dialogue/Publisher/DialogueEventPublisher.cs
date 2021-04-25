using System;

public class DialogueEventPublisher :  
    EventPublisher<CharSequenceEventArgs>,
    EventPublisher<InitiateCharSequenceEventArgs>,
    EventPublisher<CancelCharSequenceEventArgs> {
    
    private Guid LastAcked;
    private Guid LastOutgoing;

    public DialogueEventPublisher() {
        EventSystem.Instance.OnAckableEvent += HandleAck;
    }

    public void PublishEvent(CharSequenceEventArgs eventArgs) {
        LastOutgoing = eventArgs.EventId;
        EventSystem.Instance.HandleEvent(this, eventArgs);
    }

    public void PublishEvent(InitiateCharSequenceEventArgs eventArgs) {
        LastOutgoing = eventArgs.EventId;
        EventSystem.Instance.HandleEvent(this, eventArgs);
    }

    public void PublishEvent(CancelCharSequenceEventArgs eventArgs) {
        LastOutgoing = eventArgs.EventId;
        EventSystem.Instance.HandleEvent(this, eventArgs);
    }

    #nullable enable
    public void HandleAck(object? from, AckableEventArgs eventArgs) {
        LastAcked = eventArgs.EventId;
    }

}
