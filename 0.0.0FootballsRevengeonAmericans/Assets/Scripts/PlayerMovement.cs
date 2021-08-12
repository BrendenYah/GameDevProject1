
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
    public float rotationSpeed;

    Animator anim;



    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        nextDashTime = Time.time;
        anim = GetComponent<Animator>();
    }

    // Logs all input from players and inputs them into appropriate variables for the FixedUpdate 
    // method to adjust physics based off of. 
    void Update()
    {

        // Controls the movement and abilities of this player by collecting input. 
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
        if (Input.GetKeyDown("space") && (Time.time > nextDashTime))
        {
            isDashing = true;
            Debug.Log(isDashing);
        }


        PlayerAnimation();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        /*
         * This method will allow the Player to move around using the arrow keys. A vector is given
         * to the player to determine movement. 
         * 
         */

        // The player is not attempting to dash and the dash has completed. Resume normal movement.
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
            Dash();
        }
        // The player is not moving, therefore the dash cannot occur. 
        else
        {
            isDashing = false;
        }


        // Handles the rotation of the player
        if (player.velocity != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, player.velocity);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        

    }

    void Dash()
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

    // Controls the animation triggers for this player.
    void PlayerAnimation()
    { 
        if (player.velocity.x != 0 || player.velocity.y != 0)
        {
            // anim.SetBool("isMoving", true);

            // This if/else statement determines which axis is moving the fastest
            // and then adjusts the animation speed to the higher of the two speed. 
            bool xFast = Mathf.Abs(player.velocity.x) >= Mathf.Abs(player.velocity.y);
            if (xFast)
            {
                anim.SetFloat("speedMultiplier", Mathf.Abs(player.velocity.x) / speed);
            }
            else
            {
                anim.SetFloat("speedMultiplier", Mathf.Abs(player.velocity.y) / speed);
            }

        }
        else
        {
            // anim.SetBool("isMoving", false);
            anim.SetFloat("speedMultiplier", 0);
        }
    }
}
