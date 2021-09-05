using System;
using System.Collections;
using System.Collections.Generic;

using UnityEditor.Sprites;

using UnityEngine;
using UnityEngine.Serialization;


public class PlayerControl : MonoBehaviour
{
    protected bool canHide;

    [SerializeField] protected float moving = 1;
    [SerializeField] protected float moveSpeed = 7;
    [SerializeField] protected float jumpHeight = 10;
    protected Rigidbody2D playerRB;

    private Collider2D playerColl;

    //[SerializeField] protected Collider2D wallCol; // you have to set up the collider in the inpsector
    //[SerializeField] protected Collider2D groundCol; // you have to set up the collider in the inpsector
    [SerializeField]private  GameObject hideGo; // you have to set up the collider in the inpsector
    public SpriteRenderer playerSprite;

    private bool isMoving = false;

    //[SerializeField] protected bool isGrounded = false;

    //[SerializeField] protected Transform raycastPos;
    [SerializeField] private LayerMask lPlatMask;
    [SerializeField] private bool caught = false;
    [FormerlySerializedAs("gameEnded")] public bool wonGame = false;
    

    public GameObject game;
    
    //private Collider2D playerCol;


    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
       // raycastPos = GetComponent<Transform>();
        playerColl = GetComponent<Collider2D>();
        playerSprite = GetComponent<SpriteRenderer>();
       // hidingCol = GetComponentInChildren<Collider2D>();
        lPlatMask = LayerMask.GetMask("Platform");

    }

    // Update is called once per frame
    void Update()
    {
        playerRB.velocity = new Vector2(moving, playerRB.velocity.y);
        Movement();
        Hide();
        

        
        // if you get caught-
        
        if(caught)
            Destroy(gameObject);
    }

    /// <summary>
    /// Handles player movement
    /// </summary>
    protected void Movement()
    {
        moving = moveSpeed * (Input.GetAxisRaw("Horizontal"));

        if(moving >= 1)
            isMoving = true;
        //speed up the player after a few seconds of moving

        if(moving <= 15 && isMoving)
        {
            // moving += 0.5f * Time.deltaTime;
            moving += Mathf.Lerp(0, moveSpeed * Input.GetAxisRaw("Horizontal"), 0.2f);

        }

        // horizontal movement



        //jumping
        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            playerRB.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
    }


    protected void Hide()
    {
        
       if(canHide)
       {
           if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
           {
               playerSprite.color = new Color(1, 1, 1, 0.25f);
               Debug.Log("changed alpha");
               hideGo.SetActive(false);
               

           }
           else if(Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
           {
               playerSprite.color = new Color(1, 1, 1, 1f);
               hideGo.SetActive(true);
               
           }
       }
       else
       {
           hideGo.SetActive(true);
           playerSprite.color = new Color(1, 1, 1, 1f);
       }

    }
    
    public bool IsGrounded()
    {
        float heightLenience = .9f;
        //using the thing below with the raycast, if you're too exact, just like using == instead of >= or <= it could cause problems
        RaycastHit2D raycastHit = Physics2D.Raycast(playerColl.bounds.center, Vector2.down, playerColl.bounds.extents.y + heightLenience, lPlatMask);
        //pColl.Raycast(Vector2.down, RaycastHit2D);
        Color rayColor;
        if(raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(playerColl.bounds.center, Vector2.down * (playerColl.bounds.extents));

        // this sets the bool for IsGrounded. If the raycast hits a collider that isn't null (or if the ray cast isn't touching a collider at the moment.
        return raycastHit.collider != null;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        Color playerColor = playerSprite.color;

        if(other.CompareTag("HidingSpot"))
            canHide = true;
        else
        {
            canHide = false;
            playerColor.a = 1;
        }
            
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        Color playerColor = playerSprite.color;
        if(other.CompareTag("HidingSpot"))
        {
            canHide = false;
            playerColor.a = 1;
        }
    }

    public void OnTriggerEnter2D(Collider2D _collision)
    {
        if(_collision.CompareTag("Cheese"))
        {
            wonGame = true;
        }
        

    }
}



