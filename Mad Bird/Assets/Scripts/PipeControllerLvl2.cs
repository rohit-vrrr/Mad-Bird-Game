using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeControllerLvl2 : MonoBehaviour
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
        InvokeRepeating("SwitchUpDown", 0.1f, 1f);          // (func name, repeating loop, start after)
    }

    private void MovePipe()         // Moving pipe in 'x' & 'y' direction
    {
        rb.velocity = new Vector2(-speed, upDownSpeed);
    }

    private void SwitchUpDown()         // Switching 'y' directions
    {
        if (!GameManager.instance.gameOver)
        {
            upDownSpeed = -upDownSpeed;
            rb.velocity = new Vector2(-speed, upDownSpeed);
        }
        else if (GameManager.instance.gameOver)
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
