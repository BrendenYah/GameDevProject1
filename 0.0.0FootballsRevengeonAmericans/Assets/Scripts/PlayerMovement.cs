
//using System.Numerics;

using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D player;
    public float speed;
    public bool isDashing;
    public float speedMultiplier;
    public float maxSpeed;
    public float moveHorizontal;
    public float moveVertical;


    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    // Logs all input from players and inputs them into appropriate variables for the FixedUpdate 
    // method to adjust physics based off of. 
    void Update()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
        if (Input.GetKeyDown("space"))
        {
            isDashing = true;
            Debug.Log(isDashing);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDashing == false)
        {
            if (moveHorizontal > 0)
            {
                player.velocity = new Vector2(speed, player.velocity.y);
            }
            if (moveHorizontal < 0)
            {
                player.velocity = new Vector2(-speed, player.velocity.y);
            }
            if (moveVertical > 0)
            {
                player.velocity = new Vector2(player.velocity.x, speed);
            }
            if (moveVertical < 0)
            {
                player.velocity = new Vector2(player.velocity.x, -speed);
            }

            if (Mathf.Abs(moveHorizontal) <= 0.1 && Mathf.Abs(moveVertical) <= 0.1)
            {
                Debug.Log("Ending");
                player.velocity = new Vector2(0, 0);
            }
        }

        else if (Mathf.Abs(player.velocity.x) <= maxSpeed || Mathf.Abs(player.velocity.y) <= maxSpeed)
        {
            Debug.Log("Boost speed x: " + (player.velocity.x * speedMultiplier) + "\n Boost speed y: " + (player.velocity.y * speedMultiplier));
            /* player.velocity = new Vector2(player.velocity.x * speedMultiplier,
                 player.velocity.y * speedMultiplier);*/
            player.velocity = new Vector2(Mathf.Sign(player.velocity.x) * maxSpeed, Mathf.Sign(player.velocity.y) * maxSpeed);
            isDashing = false;
        }
        

    }
}
