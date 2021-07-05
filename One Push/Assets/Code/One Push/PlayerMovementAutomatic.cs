using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovementAutomatic : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform playerTransform;
    public float moveSpeed = 3;
    public float jumpForce = 100;
    public enum State { None, MoveLeft, MoveRight, Jump, }
    public State state;
    
    public int leftAmmo = 2;
    public int rightAmmo = 2;
    public int jumpAmmo = 2;
    public int shootAmmo = 0;

    public GameObject playerGun;

    public bool facingRight = true;

    public LayerMask layerMask;
    public LayerMask groundLayers;

    private GroundCheck groundcheck;

    public TMPro.TMP_Text textMeshPro;

    public AudioSource jumpSound;
    public AudioSource pickupSound;
    public AudioSource shootSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GetComponent<Transform>();

        groundcheck = gameObject.transform.Find("GroundCheck").GetComponent<GroundCheck>();

        if (facingRight == false)
        {
            Flip();
        }

        //groundcheck = GameObject.Find("").GetComponent<playerController>(); 
    }

    void Update()
    {
        //Key Inputs
        if (Input.GetKeyDown(KeyCode.A) && state != State.MoveLeft && groundcheck.isGrounded && leftAmmo > 0)
        {
            leftAmmo--;
            state = State.MoveLeft;
        }
        if (Input.GetKeyDown(KeyCode.D) && state != State.MoveRight && groundcheck.isGrounded && rightAmmo > 0)
        {
            rightAmmo--;
            state = State.MoveRight;
        }
        if (Input.GetKeyDown(KeyCode.Space) /*&& state != State.Jump*/ && groundcheck.isGrounded && jumpAmmo > 0)
        {
            jumpAmmo--;
            //state = State.Jump;
            rb.AddForce(new Vector2(0, jumpForce));
            jumpSound.Play();

            groundcheck.isGrounded = false;
        }

        //Put current Ammos as text in screen
        textMeshPro.text = $"{jumpAmmo}\n{rightAmmo}\n{leftAmmo}";

        //Set gun visual on/off when there is/isn't ammo available
        if (shootAmmo > 0)
        {
            playerGun.SetActive(true);
        } else
        {
            playerGun.SetActive(false);
        }

        //Restart level
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }

        //What to do when in pressed state
        switch (state)
        {
            case State.MoveLeft:
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
                break;
            case State.MoveRight:
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
                break;
        }

        //Raycast for bouncing against walls, checks for Layer with "Wall"
        //Because colliders don't work well with tilemaps, this was the only way I could think of
        float laserLength = .7f;
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, laserLength, layerMask);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, -Vector2.right, laserLength, layerMask);

        if (hitLeft.collider != null)
        {
            state = State.MoveRight;
        }
        if (hitRight.collider != null)
        {
            state = State.MoveLeft;
        }
        
        //Flipping player based on which way they face
        if (state == State.MoveLeft && facingRight)
        {
            Flip();
            facingRight = false;
        }
        else if (state == State.MoveRight && !facingRight)
        {
            Flip();
        }

        //Level restart when Player is out of screen
        if (Camera.main.transform.position.y >= playerTransform.position.y + 10)
        {
            RestartLevel();
        }       

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        //Finish
        if (col.gameObject.tag == "Finish")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        //Death colliders
        if(col.gameObject.tag == "Spikes" || col.gameObject.tag == "Enemy")
        {
            RestartLevel();
        }

        //Sounds for pickup
        if (col.gameObject.tag == "Apple" || col.gameObject.tag == "Gun")
        {
            pickupSound.Play();
        }
        if (Input.GetKeyDown(KeyCode.S) && shootAmmo > 0)
        {
            shootSound.Play();
        }

        //JumpPad (NOT WORKING PROPERLY)
        /*if (col.gameObject.tag == "JumpPad")
        {
            //rb.velocity = new Vector2(0, 0);
            rb.AddForce(new Vector2(0, 400));
        }*/
    }

    private void Flip()
	{
        facingRight = true;
        transform.Rotate(0f, 180f, 0f);
	}

    private void RestartLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

}