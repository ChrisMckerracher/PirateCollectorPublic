using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AdvanceCharSequenceEventArgs : AckableEventArgs {

    public readonly Guid ConversationId;
    public AdvanceCharSequenceEventArgs(Guid eventId, Guid conversationId) : base(eventId) {
        this.ConversationId = conversationId;
    }

    public override void Accept(object? sender, EventSystemVisitor visitor) {
        visitor.Visit(sender, this);
    }

}