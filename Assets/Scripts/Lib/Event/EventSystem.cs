using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventSystem : MonoBehaviour {

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

    #nullable enable
    public void HandleEvent(object? sender, AckableEventArgs eventArgs) {
        Debug.Log("We have recieved an AckableEventArg");
        OnAckableEvent(sender, eventArgs);
    }

    public void HandleEvent(object? sender, CharSequenceEventArgs eventArgs) {
        Debug.Log("We have recieved a CharSequenceEventArgs");
        if (OnCharSequenceEvent != null) {
            OnCharSequenceEvent(sender, eventArgs);
        }
        OnAckableEvent(sender, eventArgs);
    }

    public void HandleEvent(object? sender, AdvanceCharSequenceEventArgs eventArgs) {
        Debug.Log("We have recieved a AdvanceCharSequenceEventArgs");
        if (OnCharSequenceEvent != null) {
            OnAdvanceCharSequenceEvent(sender, eventArgs);
        }
        OnAckableEvent(sender, eventArgs);
    }

    public void HandleEvent(object? sender, CancelCharSequenceEventArgs eventArgs) {
    }

}
