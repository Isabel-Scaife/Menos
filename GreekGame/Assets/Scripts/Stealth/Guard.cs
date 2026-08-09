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
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer coneRendererL;
    private SpriteRenderer coneRendererR;

    [SerializeField]
    private bool renderCone;

    // movement components
    [SerializeField]
    protected int speed = 5;
    protected Vector2 position;
    [SerializeField]
    private int distance = 10;
    [SerializeField]
    private bool horizontal = true;
    [SerializeField]
    private bool stationary = false;

    //vision cone
    [SerializeField]
    private Object visionConeL;
    [SerializeField]
    private Object visionConeR;

    //player and bird references
    [SerializeField]
    private PlayerControlled player;
    [SerializeField]
    private Player personPlayer;
    [SerializeField]
    private Bird Bird;

    //ping pong...
    //needed for guard walking later on
    float pingPong = 0;
    float oldpingPong = 0;

    //animation
    private Animator animator;

    //counts frames that player has been in guard circle
    int frameCount = 0;

    //if player is in sightline or not
    bool inView;

    //GUARD TO DO:

    //Respawn function
    //Bird...

    // run on start
    void Start()
    {
        // Get the SpriteRenderer component attached to this GameObject
        //we have this for debug purposes so it can change color
        spriteRenderer = GetComponent<SpriteRenderer>();

        //get the sprite rendered attached to the vision cone
        coneRendererL = visionConeL.GetComponent<SpriteRenderer>();
        coneRendererR = visionConeR.GetComponent<SpriteRenderer>();

        //get animating component
        animator = GetComponent<Animator>();

        //figures out where the object is
        position = gameObject.transform.position;

        //changes start position based on if its hoirzontal or vertical
        //if we use the same one for both it will start wonky
        if (horizontal)
        {
            min = transform.position.x;
            max = transform.position.x + distance;
        }
        else
        {
            min = transform.position.y;
            max = transform.position.y + distance;
        }
       
    }

    void Update()
    {
        oldpingPong = pingPong;
        pingPong = Mathf.PingPong(Time.time * 2, max - min);
        //moves guard back and forth
        if (horizontal && !stationary)
        {
            transform.position =
                new Vector3(pingPong + min,
                transform.position.y,
                0);
        }
        else if (!stationary)
        {
            transform.position =
                new Vector3(transform.position.x,
                pingPong + min,
                0);
        }

        //compares new ping pong value to old
        //this tells us which direction the guard is walking
        //and we can put the sprite in there accordingly
        if (oldpingPong > pingPong)
        {
            //walking left
            //animator.Play("GuardWalkLeft");
        }
        else if (pingPong > oldpingPong)
        {
            //walking right
            //animator.Play("GuardWalkRight");
        }

        VisionCheck(player);

        //if player inview
        if (inView)
        {
            frameCount += 1;
        }

        //counts up
        if (frameCount >= 500)
        {
            //respawn at post office
            player.Respawn(new Vector3(34, -35, 0));
        }
    }

    /// <summary>
    /// checks if player should be able to see vision
    /// </summary>
    /// <param name="player"></param>
    private void VisionCheck(PlayerControlled player)
    {
        if (player.controlBird == true)
        {
            coneRendererL.enabled = true;
            coneRendererR.enabled = true;
        }
        else
        {
            coneRendererL.enabled = false;
            coneRendererR.enabled = false;
        }
    }

    /// <summary>
    /// When player enters vision cone
    /// </summary>
    /// <param name="collision"><
    /// /param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        spriteRenderer.color = Color.red;
        inView = true;
    }


    /// <summary>
    /// When player exits vision cone
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        spriteRenderer.color = Color.white;

        inView = false;
        //resets frame count
        frameCount = 0;
    }
    
}
