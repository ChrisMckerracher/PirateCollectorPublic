using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AckableEventArgs : EventArgs {
    public readonly Guid EventId;

    public AckableEventArgs(Guid eventId) {
        this.EventId = eventId;
    }
    
}
