using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Person that can be alerted by player's movements
/// </summary>
public class Guard : MonoBehaviour
{
    //---CHANGE THESE FROM GUARD TO GUARD----
    //guard travels between these two positions. 

    //position one
    private float min = 0f;
    //position two
    private float max = 3f;

    // fields
    [SerializeField] //what does serialize feed do....
    public bool playerDetectable = true;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer coneRenderer;

    // movement components
    [SerializeField]
    protected int speed = 5;
    protected Vector2 position;

    //vision cone
    [SerializeField]
    private Object visionCone;

    //player and bird references
    [SerializeField]
    private PlayerControlled player;
    [SerializeField]
    private Player Player;
    [SerializeField]
    private Bird Bird;

    //ping pong...
    //needed for guard walking later on
    float pingPong = 0;
    float oldpingPong = 0;

    //animation
    private Animator animator;

    //GUARD TO DO:

    //HIGH PRIORITY-- Vision --  DONE
    //HIGH PRIORITY-- Bird Trigger-- DONE
    //MED PRIORITY-- Respawn
    //MED PRIORITY-- Hide Box-- DONE + FIXED
    //LOW PRIORITY-- Walk Cycles -- Halfway done-- need to make these more modular

    // run on start
    void Start()
    {
        // Get the SpriteRenderer component attached to this GameObject
        //we have this for debug purposes so it can change color
        spriteRenderer = GetComponent<SpriteRenderer>();

        //get the sprite rendered attached to the vision cone
        coneRenderer = visionCone.GetComponent<SpriteRenderer>();
        coneRenderer.enabled = true;

        //get animating component
        animator = GetComponent<Animator>();

        //figures out where the object is
        position = gameObject.transform.position;

        //min and max for guard
        min = transform.position.x;
        max = transform.position.x + 3;
    }

    void Update()
    {
        oldpingPong = pingPong;
        pingPong = Mathf.PingPong(Time.time * 2, max - min);
        //moves guard back and forth
        transform.position =
            new Vector3(pingPong + min,
            transform.position.y,
            transform.position.x);

        //compares new ping pong value to old
        //this tells us which direction the guard is walking
        //and we can put the sprite in there accordingly
        if (oldpingPong>pingPong)
        {
            //walking left
            animator.Play("GuardWalkLeft");
        }
        else if (pingPong>oldpingPong)
        {
            //walking right
            animator.Play("GuardWalkRight");
        }

        VisionCheck(player);
    }

    /// <summary>
    /// checks if player should be able to see vision
    /// </summary>
    /// <param name="player"></param>
    private void VisionCheck(PlayerControlled player)
    {
        //detects whether player can see vision cone or not
        //this only thinks the player is a birddd
        if (player is Bird)
        {
            coneRenderer.enabled = true;
        }
        else if (player is Player)
        {
            coneRenderer.enabled = false;
        }
    }
    /// <summary>
    /// When player enters vision cone
    /// </summary>
    /// <param name="collision"><
    /// /param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerDetectable) //makes sure player isn't hiding
        {
            spriteRenderer.color = Color.red;
            //run respawn/fail state code here
        }
    }

    /// <summary>
    /// When player exits vision cone
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        spriteRenderer.color = Color.white;
    }
    
}
