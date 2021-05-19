using System;
using System.Collections;
using Lib.Movement;
using UnityEngine;


// I ran into problems running character update logic into update, and thus may want to consider keeping it in fixed update
// this was due to the fact that sometimes multiple fixed update loops happened between updates, making the npc move more than expected
public class CharacterMovement : MonoBehaviour
{
    public Animator Animator;
    public Rigidbody2D rigidBody2D;
    public float MoveSpeed = 5f;

    public ChainableIEnumerable chainable;
    public bool running = true;
    public bool isPaused;

    public int movementInterval = 10;
    public string[] directions;

    private readonly bool isStopped = false;

    private Vector2 Movement;

    private IEnumerable movementLoop;
    private IEnumerator movementLoopENum;

    private void Start()
    {
        Movement.x = 0;
        Movement.y = 0;
    }

    private void Update()
    {
        var sqrMagnitude = Movement.sqrMagnitude;

        if (sqrMagnitude > 0)
        {
            Animator.SetFloat("Horizontal", Movement.x);
            Animator.SetFloat("Vertical", Movement.y);
        }

        Animator.SetFloat("Speed", sqrMagnitude);
    }

    private void FixedUpdate()
    {
        if (movementLoop == null || !movementLoopENum.MoveNext())
        {
            Debug.Log("resetting");
            movementLoop = resetCycle()();
            movementLoopENum = movementLoop.GetEnumerator();
        }

        rigidBody2D.MovePosition(rigidBody2D.position + Movement * MoveSpeed * Time.deltaTime);
    }

    private IEnumerable WalkLeft()
    {
        Debug.Log("Walk Left");
        for (var i = 0; i < movementInterval; i++)
        {
            if (isStopped)
            {
                Movement.x = 0;
                Movement.y = 0;
                break;
            }

            if (isPaused)
            {
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

    private IEnumerable WalkRight()
    {
        Debug.Log("Walk Right");
        for (var i = 0; i < movementInterval; i++)
        {
            if (isStopped)
            {
                Movement.x = 0;
                Movement.y = 0;
                break;
            }

            if (isPaused)
            {
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

    private IEnumerable Walk(string[] directions)
    {
        for (var i = 0; i < directions.Length; i++)
        {
            if (isStopped)
            {
                Movement.x = 0;
                Movement.y = 0;
                break;
            }

            if (isPaused)
            {
                Movement.y = 0;
                i--;
                // obviously assuming that this will correctly correlate with FixedUpdate. Could cause bug dummy
                yield return new WaitForSeconds(Time.fixedDeltaTime);
                continue;
            }
            // ToDo: Possibly rethink this
            string direction = directions[i];
            MoveEnum directionEnum = MoveEnum.N.ParseFrom(direction);
            Debug.Log(direction);
            switch (directionEnum)
            {
                case MoveEnum.N:
                    Movement.x = 0;
                    Movement.y = 1;
                    break;
                case MoveEnum.S:
                    Movement.x = 0;
                    Movement.y = -1;
                    break;
                case MoveEnum.E:
                    Movement.x = 1;
                    Movement.y = 0;
                    break;
                case MoveEnum.W:
                    Movement.x = -1;
                    Movement.y = 0;
                    break;
            }

            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        
        Movement.x = 0;
        Movement.y = 0;
        yield return null;
    }

    private Func<IEnumerable> resetCycle()
    {
        Func<IEnumerable> walkLeft = () => { return WalkLeft(); };
        Func<IEnumerable> walkRight = () => { return WalkRight(); };
        Func<IEnumerable> walkGroup = () => { return Walk(directions); };
        Func<IEnumerable>[] requests = {walkGroup};

        return () => { return chainable.chain(requests); };
    }

    private void PlayerMovement()
    {
        Movement.x = Input.GetAxisRaw("Horizontal");
        Movement.y = Input.GetAxisRaw("Vertical");
    }
}