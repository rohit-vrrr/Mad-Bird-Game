using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeController : MonoBehaviour
{
    public float speed;                 // x-axis speed
    public float upDownSpeed;           // y-axis speed

    // public bool gameOver;            // For Debugging Purposes(Stopping Pipes)

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        /*gameOver = GameManager.instance.gameOver;         // For Debugging Purposes(Stopping Pipes)
        Debug.Log(gameOver);*/

        MovePipe();
    }

    private void MovePipe()         // Moving pipe in 'x' & 'y' direction
    {
        rb.velocity = new Vector2(-speed, 0);
        InvokeRepeating("StopMovement", 0.1f, 0.2f);
    }

    private void StopMovement()
    {
        if(GameManager.instance.gameOver)
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PipeRemover")
        {
            Destroy(gameObject);
        }
    }
}
