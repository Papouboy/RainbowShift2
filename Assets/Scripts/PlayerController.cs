﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool jump = false;
	public float moveForce;
	public float maxSpeed;
	public float jumpForce;
    public Transform groundCheck;

	//saut
	[Range(1,30)] public float jumpVelocity;

    private bool grounded = false;
    //private Animator anim;
    private Rigidbody2D rb2d;

    private ColorManager.IColor iColorCourant; //stratégie courante
    // Use this for initialization
    void Awake()
    {
       // anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position);

        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

       // anim.SetFloat("Speed", Mathf.Abs(h));

        if (h * rb2d.velocity.x < maxSpeed)
            rb2d.AddForce(Vector2.right * h * moveForce);

        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();

        if (jump)
        {
          //  anim.SetTrigger("Jump");
			rb2d.velocity = Vector2.up * jumpVelocity;
			//rb2d.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
    }


    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    void OnTriggerEnter2D(Collider2D other)
    {

        string typeStrategie = other.gameObject.tag;
        #region Strategy
        switch (typeStrategie)
        {

            case "GemBlue":
                other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                iColorCourant = new ColorManager.GemBlue();
                iColorCourant.Colorize();

                break;

            case "GemRed":
                other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                iColorCourant = new ColorManager.GemRed();
                iColorCourant.Colorize();

                break;

            case "GemGreen":
                other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                iColorCourant = new ColorManager.GemGreen();
                iColorCourant.Colorize();

                break;

            case "GemYellow":
                other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                break;

            default:
                Debug.Log("Aucun Tag");
                break;
        }
        #endregion
    }

    }