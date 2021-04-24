using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CancelCharSequenceEventArgs : AckableEventArgs {

    public readonly Guid ConversationId;
    public CancelCharSequenceEventArgs(Guid eventId, Guid conversationId) : base(eventId) {
        this.ConversationId = conversationId;
    }

    public override void Accept(object? sender, EventSystemVisitor visitor) {
        visitor.Visit(sender, this);
    }

}