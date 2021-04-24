using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InitiateCharSequenceEventArgs : AckableEventArgs {
    public readonly Guid ConversationId;

    public InitiateCharSequenceEventArgs(Guid eventId, Guid conversationId) : base(eventId) {
        this.ConversationId = conversationId;
    }

    public enum Type {
        CharSequence, NewLine, End
    }
    public enum PrintMode {
        SingleChar, FullLine
    }

}

