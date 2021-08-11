
//using System.Numerics;

using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D player;
    public float speed;
    public bool isDashing;
    public float dashCooldown;
    public float nextDashTime;
    public float speedMultiplier;
    public float maxSpeed;
    public float moveHorizontal;
    public float moveVertical;



    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        nextDashTime = Time.time;
    }

    // Logs all input from players and inputs them into appropriate variables for the FixedUpdate 
    // method to adjust physics based off of. 
    void Update()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
        if (Input.GetKeyDown("space") && (Time.time > nextDashTime))
        {
            isDashing = true;
            Debug.Log(isDashing);
            nextDashTime = Time.time + dashCooldown;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        /*
         * This method will allow the Player to move around using the arrow keys. A vector is given
         * to the player to determine movement. 
         * 
         */

        // The player is not dashing
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
        }
        // The player is dashing and moving in a direction.
        else if (moveHorizontal != 0 || moveVertical != 0)
        {
            // If the dash is not on cooldown, try to dash.
            if (nextDashTime < Time.time)
            {
                bool maxSpeedx = false;
                bool maxSpeedy = false;

                maxSpeedx = Mathf.Abs(player.velocity.x * speedMultiplier) > maxSpeed;
                maxSpeedy = Mathf.Abs(player.velocity.y * speedMultiplier) > maxSpeed;
                
                // If already at maxSpeed, set the velocity to max speed again
                if (maxSpeedx)
                {
                    player.velocity = new Vector2(maxSpeed, player.velocity.y);
                }
                if (maxSpeedy)
                {
                    player.velocity = new Vector2(player.velocity.x, maxSpeed);
                }
                // 
                if (!maxSpeedx)
                {
                    player.velocity = new Vector2(player.velocity.x * speedMultiplier,
                        player.velocity.y);
                }
                if (!maxSpeedy)
                {
                    player.velocity = new Vector2(player.velocity.x,
                        player.velocity.y * speedMultiplier);
                }
                isDashing = false;
            }
        }
        else
        {
            isDashing = false;
        }

        

    }
}
