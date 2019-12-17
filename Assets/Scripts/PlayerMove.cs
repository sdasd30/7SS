using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public int playerSpeed = 10;
    public bool facingRight = true;
    public int jumpPower = 1250;
    private float moveX;
	
	// Update is called once per frame
	void Update ()
    {
		MovePlayer();	
	}

    void MovePlayer()
    {
        //CONTROL
        moveX = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown ("Jump"))
        {
            Jump();
        }
        //ANIMATION
        //PLAYER DIRECTION
        if (moveX > 0.0f && facingRight == false)
        {
            FlipPlayer();
        } else if (moveX < 0.0f && facingRight == true)
        {
            FlipPlayer();
        }
        //PHYSICS
		gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerSpeed * Time.deltaTime, gameObject.GetComponent<Rigidbody2D>().velocity.y);

    }

    void Jump()
    {
		GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpPower * Time.deltaTime); 
    }

    void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
