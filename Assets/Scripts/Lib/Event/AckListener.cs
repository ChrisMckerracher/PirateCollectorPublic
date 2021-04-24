using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface AckListener {

    #nullable enable
    void HandleAck(object? from, AckableEventArgs eventArgs);

}
