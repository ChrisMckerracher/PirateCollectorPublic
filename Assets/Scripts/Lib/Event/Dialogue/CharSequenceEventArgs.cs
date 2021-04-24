using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharSequenceEventArgs : AckableEventArgs {
    public readonly Type SequenceType;

    public readonly PrintMode SequencePrintMode;

    public readonly string CharData;

    public readonly Guid ConversationId;

    public CharSequenceEventArgs(string charData, Guid eventId, Type sequenceType, PrintMode sequencePrintMode, Guid conversationId) : base(eventId) {
        this.CharData = charData;
        this.SequenceType = sequenceType;
        this.SequencePrintMode = sequencePrintMode;
        this.ConversationId = conversationId;
    }

    public enum Type {
        CharSequence, NewLine, End
    }
    public enum PrintMode {
        SingleChar, FullLine
    }

    public override void Accept(object? sender, EventSystemVisitor visitor) {
        visitor.Visit(sender, this);
    }

}
