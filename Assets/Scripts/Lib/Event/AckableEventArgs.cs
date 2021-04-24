using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AckableEventArgs : EventArgs, EventSystemVisitable {
    public readonly Guid EventId;

    public AckableEventArgs(Guid eventId) {
        this.EventId = eventId;
    }

    public virtual void Accept(object? sender, EventSystemVisitor visitor) {
        visitor.Visit(sender, this);
    }
    
}
