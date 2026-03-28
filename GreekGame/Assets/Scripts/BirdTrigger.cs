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
    private PlayerControlled player;
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
        //for debug purposes changes it to red if player detected
        spriteRenderer.color = Color.red;

        //swaps the controlling object
        player.SwapControlledObject();
    }
    
}
