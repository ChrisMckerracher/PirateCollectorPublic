using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


// I ran into problems running character update logic into update, and thus may want to consider keeping it in fixed update
// this was due to the fact that sometimes multiple fixed update loops happened between updates, making the npc move more than expected
public class CharacterMovement : MonoBehaviour {
    public Animator Animator;
    public Rigidbody2D rigidBody2D;
    public float MoveSpeed = 5f;

    public ChainableIEnumerable chainable;

    private Vector2 Movement;
    public bool running = true;
    // Update is called once per frame

    private float howLongIgnore;
    private IEnumerable test;
    private IEnumerator testEnum;

    void Start() {
        Movement.x = 0;
        Movement.y = 0;
    }

    void Update() {

        float sqrMagnitude = Movement.sqrMagnitude;

        if (sqrMagnitude > 0) {
            Animator.SetFloat("Horizontal", Movement.x);
            Animator.SetFloat("Vertical", Movement.y);
        }

        Animator.SetFloat("Speed", sqrMagnitude);
    }

    private void FixedUpdate() {
        
        if (test == null || !testEnum.MoveNext()) {
            Debug.Log("resetting");
            test = resetCycle()();
            testEnum = test.GetEnumerator();
        }
        rigidBody2D.MovePosition(rigidBody2D.position + Movement * MoveSpeed * Time.deltaTime);
    }

    private bool isStopped = false;
    public bool isPaused = false;

    public int movementInterval = 10;
    private IEnumerable WalkLeft() {
        Debug.Log("Walk Left");
        for (int i = 0; i < movementInterval; i++) {
            if (isStopped) {
                Movement.x = 0;
                Movement.y = 0;
                break;
            }
            if (isPaused) {
                Debug.Log("I am paused");
                Movement.x = 0;
                Movement.y = 0;
                i--;
                // obviously assuming that this will correctly correlate with FixedUpdate. Could cause bug dummy
                yield return new WaitForSeconds(Time.fixedDeltaTime);
                continue;
            }
            Movement.x = -1;
            Movement.y = 0;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        Movement.x = 0;
        Movement.y = 0;
        Debug.Log(rigidBody2D.position);

    }

    private IEnumerable WalkRight() {
        Debug.Log("Walk Right");
        for (int i = 0; i < movementInterval; i++) {
            if (isStopped) {
                Movement.x = 0;
                Movement.y = 0;
                break;
            }
            if (isPaused) {
                Movement.x = 0;
                Movement.y = 0;
                i--;
                // obviously assuming that this will correctly correlate with FixedUpdate. Could cause bug dummy
                yield return new WaitForSeconds(Time.fixedDeltaTime);
                continue;
            }
            Movement.x = 1;
            Movement.y = 0;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }

        Movement.x = 0;
        Movement.y = 0;
        Debug.Log(rigidBody2D.position);


    }

    public bool left = false;
    private Func<IEnumerable> resetCycle() {

        Func<IEnumerable> walkLeft = () => { return WalkLeft(); };
        Func<IEnumerable> walkRight = () => { return WalkRight(); };
        Func<IEnumerable>[] requests = {walkRight, walkLeft};

        return () => {return chainable.chain(requests);};
    }

    private void PlayerMovement() {
        Movement.x = Input.GetAxisRaw("Horizontal");
        Movement.y = Input.GetAxisRaw("Vertical");
    }
}
