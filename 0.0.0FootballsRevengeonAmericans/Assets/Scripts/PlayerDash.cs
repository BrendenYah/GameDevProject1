using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private Rigidbody2D player;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        speed = 100f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
     //   player.velocity = new Vector2(player.velocity.x + speed)
    }
}
