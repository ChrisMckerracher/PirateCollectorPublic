using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventSystem : MonoBehaviour, EventSystemVisitor {

    public event EventHandler<AckableEventArgs> OnAckableEvent;
    public event EventHandler<CharSequenceEventArgs> OnCharSequenceEvent;
    public event EventHandler<AdvanceCharSequenceEventArgs> OnAdvanceCharSequenceEvent;

    private static EventSystem _instance;

    public static EventSystem Instance {
        get {
            if (_instance == null) {
                // The double loop is to avoid calling find object when not needed
                _instance = FindObjectOfType<EventSystem>();
                if (_instance == null) {
                    _instance = new EventSystem();
                }
            }

            return _instance;
        }
    }

    /// In hindsight this method was pointless, the functionality could be moved to the overloaded visit methods
    // but I am leaving here so I can remember why I would use the visitor pattern
    #nullable enable
    public void HandleEvent<T>(object? sender, T eventArgs) where T: AckableEventArgs {

        // likely not performant
        if (typeof(T) == typeof(AckableEventArgs)) {
            Debug.Log("We have recieved an AckableEventArg");
            OnAckableEvent(sender, eventArgs);
        }

        if (typeof(T) == typeof(CharSequenceEventArgs)) {
            // This would be a good situation to use a visitor pattern, but simplifying the code is better
            Debug.Log("We have recieved a CharSequenceEventArgs");
            eventArgs.Accept(sender, this);
            OnAckableEvent(sender, eventArgs);
        }

        if (typeof(T) == typeof(AdvanceCharSequenceEventArgs)) {
            Debug.Log("We have recieved a AdvanceCharSequenceEventArgs");
            eventArgs.Accept(sender, this);
            OnAckableEvent(sender, eventArgs);
        }
    }

    public void Visit(object? sender, CharSequenceEventArgs eventArgs) {
        Debug.Log("Calling Char Sequence handlers");
        if (OnCharSequenceEvent != null) {
            OnCharSequenceEvent(sender, eventArgs);
        }
    }

    
    public void Visit(object? sender, AdvanceCharSequenceEventArgs eventArgs) {
        Debug.Log("Calling Advance Char Sequence handlers");
        if (OnCharSequenceEvent != null) {
            OnAdvanceCharSequenceEvent(sender, eventArgs);
        }
    }

    public void Visit(object? sender, CancelCharSequenceEventArgs eventArgs) {
        Debug.Log("Calling Cancel Char Sequence handlers");
    }

    public void Visit(object? sender, AckableEventArgs eventArgs) {
        Debug.Log("we don't want this to happen");
    }

}
