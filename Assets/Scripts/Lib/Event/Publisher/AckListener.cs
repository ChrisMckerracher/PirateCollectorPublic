using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

interface AckListener {

    #nullable enable
    void HandleAck(object? from, AckableEventArgs eventArgs);

}
