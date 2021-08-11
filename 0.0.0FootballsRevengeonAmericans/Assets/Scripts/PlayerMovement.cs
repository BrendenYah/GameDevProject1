
//using System.Numerics;

using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D player;
    public float speed;
    public bool isDashing;
    public float dashCooldown;
    public float dashTime = 0.5f;
    public float originalDashTime;
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

        // The player is not dashing and the dash has completed. Resume normal movement.
        if (!isDashing && Time.time > dashTime)
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
        else if ((moveHorizontal != 0 || moveVertical != 0) && isDashing)
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

                // If not at max speed after dash, multiply the player velocity. 
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

                // Sets the dash cooldown
                nextDashTime = Time.time + dashCooldown;
                // Sets for how long the dash is going to be. 
                dashTime = Time.time + originalDashTime;

            }
            isDashing = false;
        }
        // The player is not moving, therefore the dash cannot occur. 
        else
        {
            isDashing = false;
        }

        

    }
}
