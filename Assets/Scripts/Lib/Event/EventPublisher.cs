using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

interface EventPublisher<T> : AckListener where T : AckableEventArgs {
    void PublishEvent(T eventArgs);

}
