using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Person that can be alerted by player's movements
/// </summary>
public class BirdTrigger : MonoBehaviour
{

    // fields
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Player player;
    [SerializeField]
    private Bird bird;

    // run on start
    void Start()
    {
        // Get the SpriteRenderer component attached to this GameObject
        //we have this for debug purposes so it can change color
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// When player enters trigger zone
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Bird")
        {
            //for debug purposes changes it to red if bird detected
            spriteRenderer.color = Color.red;

            //swaps the controlling object
            player.SwapControlledObject();
            bird.SwapControlledObject();
        }
        else if (collision.gameObject.name == "Player")
        {
            //for debug purposes changes it to red if bird detected
            spriteRenderer.color = Color.green;

            //swaps the controlling object
            player.SwapControlledObject();
            bird.SwapControlledObject();
        }

    }
    
}
