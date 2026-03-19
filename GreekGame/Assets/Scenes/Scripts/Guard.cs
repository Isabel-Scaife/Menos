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

    //position one
    private float min = 0f;
    //position two
    private float max = 3f;

    // fields
    [SerializeField] //what does serialize feed do....
    public bool playerDetectable = true;
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    // movement components
    [SerializeField]
    protected int speed = 5;
    protected Vector2 position;

    //GUARD TO DO:

    //HIGH PRIORITY-- Vision --  DONE
    //HIGH PRIORITY-- Bird Trigger
    //MED PRIORITY-- Respawn
    //MED PRIORITY-- Hide Box
    //LOW PRIORITY-- Walk Cycles -- Halfway done-- need to make these more modular

    // run on start
    void Start()
    {
        // Get the SpriteRenderer component attached to this GameObject
        //we have this for debug purposes so it can change color
        spriteRenderer = GetComponent<SpriteRenderer>();

        //figures out where the object is
        position = gameObject.transform.position;

        //min and max for guard
        min = transform.position.x;
        max = transform.position.x + 3;
    }

    void Update()
    {
        transform.position =
            new Vector3(Mathf.PingPong(Time.time * 2, max - min) + min, transform.position.y, transform.position.x);
    }

    /// <summary>
    /// When player enters vision cone
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerDetectable)
        {
            spriteRenderer.color = Color.red;
        }
        //for debug purposes changes it to red if player detected

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
