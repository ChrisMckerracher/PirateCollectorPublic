using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


// I ran into problems running character update logic into update, and thus may want to consider keeping it in fixed update
// this was due to the fact that sometimes multiple fixed update loops happened between updates, making the npc move more than expected
public class NpcController : MonoBehaviour {
    public Animator Animator;
    public CharacterMovement Movement;

    void Start() {
    }

    void Update() {

    }

}
