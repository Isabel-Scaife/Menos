using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Object that becomes semitransparent when certain colliders are behind it
/// </summary>
public class SeeThrough : MonoBehaviour
{
    // fields
    [SerializeField]
    private float seeThroughAlpha;

    [SerializeField]
    private SpriteRenderer sprRenderer;

    [SerializeField]
    private List<Collider2D> objectsThatTriggerTransparency;

    private HashSet<Collider2D> triggers;
    private HashSet<Collider2D> triggersCurrentlyOverlapping;

    private void Awake()
    {
        // init hash sets
        triggers = new HashSet<Collider2D>(objectsThatTriggerTransparency);
        triggersCurrentlyOverlapping = new HashSet<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // track collision as overlapping and become semitransparent if not already
        if (triggers.Contains(collision))
        {
            triggersCurrentlyOverlapping.Add(collision);

            if (triggersCurrentlyOverlapping.Count == 1)
            {
                Color c = sprRenderer.color;
                c.a = seeThroughAlpha;
                sprRenderer.color = c;
            }            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // untack collider that left and become opaque if all triggers left
        if (triggersCurrentlyOverlapping.Contains(collision))
        {
            triggersCurrentlyOverlapping.Remove(collision);
            
            if (triggersCurrentlyOverlapping.Count < 1)
            {
                Color c = sprRenderer.color;
                c.a = 1;
                sprRenderer.color = c;
            }
        }
    }
}
