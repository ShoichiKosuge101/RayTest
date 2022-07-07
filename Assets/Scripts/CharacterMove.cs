using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    private float speed = 3.0f;

    private Rigidbody rb;
    private Vector3 move;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move = new Vector3(Input.GetAxis("Horizontal"), this.rb.velocity.y, Input.GetAxis("Vertical"));
        //moveDirection = transform.TransformDirection(moveDirection);

        // GameOver
        if (this.transform.position.y <= -20)
        {
            Debug.Log("Game Over");
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = move * speed;
    }
}
