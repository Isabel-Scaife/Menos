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
        //propBlock = new MaterialPropertyBlock();
    }

    public abstract void Interact(PlayerControlled player);

    /// <summary>
    /// set highlight on or off
    /// </summary>
    /// <param name="enabled">true for on, false for off</param>
    /// <param name="thickness">outline thickness</param>
    public void SetHighlight(bool enabled, float thickness)
    {
        sprRenderer.GetPropertyBlock(propBlock);
        float lineW = 0.0f;
        if (enabled) lineW = thickness;
        propBlock.SetFloat("OutlineSize", lineW);
        sprRenderer.SetPropertyBlock(propBlock);
    }
}