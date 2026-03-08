using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Person that can be alerted by player's movements
/// </summary>
public class Guard : MonoBehaviour
{
    // fields
    [SerializeField] //what does serialize feed do....
    bool playerDetected = false;
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    //GUARD TO DO:

    //HIGH PRIORITY-- Vision + Trigger
    //LOW PRIORITY-- Walk Cycles

    // run on start
    void Start()
    {
        // Get the SpriteRenderer component attached to this GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// When player enters vision cone
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //for debug purposes changes it to red if player detected
        spriteRenderer.color = Color.red; 
        playerDetected = true;
        
        //QUESTION FOR NEXT MEETING; Do they want it to expand
        //upon first detection>?
        //How long do they want the player to be able to stay here?
        //What do they want fail state to look like?

    }

    /// <summary>
    /// When player exits vision cone
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        spriteRenderer.color = Color.white;
        playerDetected = false;
    }

    public void Alerted()
    {

    }
    
}
