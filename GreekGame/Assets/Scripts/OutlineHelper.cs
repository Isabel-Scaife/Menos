using UnityEngine;

/// <summary>
/// highlight that can be turned on for an interactable without a proper sprite
/// </summary>
public class OutlineHelper : MonoBehaviour
{
    // fields
    private SpriteRenderer sprRenderer;
    private MaterialPropertyBlock propBlock;

    private void Awake()
    {
        sprRenderer = GetComponent<SpriteRenderer>();
        propBlock = new MaterialPropertyBlock();
    }

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
