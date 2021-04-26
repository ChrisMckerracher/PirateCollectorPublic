using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class NpcController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float speed;

    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.velocity = Vector2.zero;
        rb2d.angularVelocity = 0f;

        rb2d.AddForce(movement * speed);
    }

    void OnTriggerEnter2D(Collider2D other) {
    }

}
