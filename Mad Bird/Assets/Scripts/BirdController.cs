using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    Rigidbody2D rb;                                  // Accessing Rigidbody component

    public float upForce;                            // Jumping Force
    bool started;
    public bool gameOver;
    bool isHitAudioPlayed;

    public GameObject blackscreenFlash;              // On Death Screen Flash
    public GameObject quad;                          // For Background Scrolling
    public GameObject particle;                      // Blood Effect

    private Animator anim;
    // bool isFallPlayed;

    // public float rotation;                        // Adding rotation for the bird

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        particle.SetActive(false);

        started = false;
        gameOver = false;
        isHitAudioPlayed = false;
    }

    void Update()
    {
        OnFirstClick();
        HandleFlyUp();
    }

    public void OnFirstClick()
    {
        if (!started)
        {
            if (Input.GetMouseButton(0))
            {
                rb.isKinematic = false;                                 // Bird is set to Dynamic
                rb.gravityScale = 3f;

                started = true;

                GameManager.instance.StartGame();                       // Calling GameManager to START GAME

                GetComponent<Animator>().Play("BirdFlapping");          // Playing Bird Flapping Animation
                Fly();

                quad.GetComponent<BackgroundScroll>().StartScrolling();         // Background Scrolling
            }
        }
        else if(started && !gameOver)
        {
            Fly();
        }
    }

    private void Fly()                                          // Velocity upwards in y-axis
    {
        // InvokeRepeating("SwitchRotation", 2f, 1f);

        if(Input.GetMouseButtonDown(0))
        {
            rb.velocity = new Vector2(0, 0);

            rb.AddForce(new Vector2(0, upForce));

            GameObject.Find("Audio-BirdFlap").GetComponent<AudioSource>().Play();           // FlappingAudio
        }
    }

    /*private void SwitchRotation()
    {
        rotation = -rotation;
        transform.Rotate(0, 0, rotation);
    }*/

    private void OnCollisionEnter2D(Collision2D collision)                      // If Bird is colliding with anything
    {
        gameOver = true;
        rb.velocity = new Vector2(0, 0);

        GameManager.instance.GameOver();

        if(!isHitAudioPlayed)
        {
            GameObject.Find("Audio-Hit").GetComponent<AudioSource>().Play();        // HitAudio
            isHitAudioPlayed = true;
        }

        blackscreenFlash.GetComponent<Animator>().Play("BlackScreenFlash");     // BlackScreenFlash Animation
        anim.Play("BirdDead");                                                  // Playing Bird Dead Animation

        quad.GetComponent<BackgroundScroll>().StopScrolling();                  // Stop Scrolling bg
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Pipe" && !gameOver)
        {
            gameOver = true;

            particle.SetActive(true);                                               // Particle effect

            GameManager.instance.GameOver();                                        // Calling GameManager to END GAME

            if (!isHitAudioPlayed)
            {
                GameObject.Find("Audio-Hit").GetComponent<AudioSource>().Play();        // HitAudio
                isHitAudioPlayed = true;
            }

            blackscreenFlash.GetComponent<Animator>().Play("BlackScreenFlash");     // BlackScreenFlash Animation
            anim.Play("BirdDead");                                                  // Playing Bird Dead Animation

            quad.GetComponent<BackgroundScroll>().StopScrolling();                  // Stop Scrolling bg
        }

        if(col.gameObject.tag == "ScoreChecker" && !gameOver)
        {
            GameObject.Find("Audio-Point").GetComponent<AudioSource>().Play();      // PointAudio
            ScoreManager.instance.IncrementScore();
        }
    }

    private void HandleFlyUp()
    {
        // isFallPlayed = false;

        if(started)
        {
            if(Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger("FlyUp");
            }
        }
    }
}
