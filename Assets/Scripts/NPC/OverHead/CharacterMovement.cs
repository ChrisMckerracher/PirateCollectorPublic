using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {
    public Animator Animator;
    public Rigidbody2D rigidBody2D;
    public float MoveSpeed = 5f;

    public ChainableCoroutine chainable;

    private Vector2 Movement;
    private bool running = false;
    // Update is called once per frame

    void Start() {
        Movement.x = 0;
        Movement.y = 0;
    }

    void Update() {
        if (!running) {
            running = true;
            resetCycle();
        }

        float sqrMagnitude = Movement.sqrMagnitude;

        if (sqrMagnitude > 0) {
            Animator.SetFloat("Horizontal", Movement.x);
            Animator.SetFloat("Vertical", Movement.y);
        }

        Animator.SetFloat("Speed", sqrMagnitude);
    }

    private void FixedUpdate() {
        rigidBody2D.MovePosition(rigidBody2D.position + Movement * MoveSpeed * Time.deltaTime);
    }

    private bool isStopped = false;
    public bool isPaused = false;

    public int movementInterval = 10;
    private IEnumerator WalkLeft() {
        for (int i = 0; i < movementInterval; i++) {
            Debug.Log(i);
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
        Debug.Log("done");
    }

    private IEnumerator WalkRight() {
        for (int i = 0; i < movementInterval; i++) {
            Debug.Log(i);
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

    }

    private Coroutine resetCycle() {
        Func<Coroutine> walkLeft = () => { return StartCoroutine(WalkLeft()); };
        Func<Coroutine> walkRight = () => { return StartCoroutine(WalkRight()); };
        //TimeoutCoroutineRequest walkLeftRequest = new TimeoutCoroutineRequest(10f, 5, walkLeft);
        //TimeoutCoroutineRequest walkRightRequest = new TimeoutCoroutineRequest(10f, 5, walkRight);
        //TimeoutCoroutineRequest[] requests = {walkLeftRequest, walkRightRequest, walkRightRequest, walkLeftRequest};
        Func<Coroutine>[] requests = {walkLeft, walkRight, walkRight, walkLeft};

        return StartCoroutine(chainable.chain(requests));
    }

    private void PlayerMovement() {
        Movement.x = Input.GetAxisRaw("Horizontal");
        Movement.y = Input.GetAxisRaw("Vertical");
    }
}
