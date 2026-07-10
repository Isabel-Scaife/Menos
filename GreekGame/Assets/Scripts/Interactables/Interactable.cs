using UnityEngine;

/// <summary>
/// Anything or anyone the player can interact with in the overworld
/// </summary>
public abstract class Interactable : MonoBehaviour
{
    // fields
    protected SpriteRenderer sprRenderer;
    private MaterialPropertyBlock propBlock;
    
    protected virtual void Awake()
    {
        sprRenderer = GetComponent<SpriteRenderer>();
        propBlock = new MaterialPropertyBlock();
    }

    public abstract void Interact(PlayerControlled player);

    /// <summary>
    /// set highlight on or off
    /// </summary>
    /// <param name="enabled">true for on, false for off</param>
    public void SetHighlight(bool enabled)
    {
        sprRenderer.GetPropertyBlock(propBlock);
        float val = 0.0f;
        if (enabled) val = 1.0f;
        propBlock.SetFloat("_OutlineEnabled", val);
        sprRenderer.SetPropertyBlock(propBlock);
    }
}