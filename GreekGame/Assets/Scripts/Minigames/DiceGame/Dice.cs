using UnityEngine;
using UnityEngine.InputSystem;

// one die in a dice game
public class Dice : MonoBehaviour
{
    // fields
    private SpriteRenderer sprRenderer;
    private MaterialPropertyBlock propBlock;
    [SerializeField] private Sprite[] sprites;              // 1-6 pips in order
    
    // properties
    /// <summary>
    /// whether or not the die is allowed to be rerolled
    /// </summary>
    public bool Locked { get; set; }

    /// <summary>
    /// currently shown value of the die (1-6)
    /// </summary>
    public int Value { get; set; }
    
    /// <summary>
    /// whether or not this die is currently selected
    /// </summary>
    public bool Selected { get; private set; }

    private void Awake()
    {
        sprRenderer = GetComponent<SpriteRenderer>();
        propBlock = new MaterialPropertyBlock();
    }

    // TODO: onclick

    /// <summary>
    /// randomizes this die's value 1-6
    /// </summary>
    public void Roll()
    {
        Value = Random.Range(1, 7);
        sprRenderer.sprite = sprites[Value - 1];
    }

    /// <summary>
    /// selects this die as long as it is not locked
    /// </summary>
    public void Select()
    {
        if (!Locked) 
        {
            Selected = true;
            SetHighlight(true);
        }
    }

    /// <summary>
    /// deselects this die
    /// </summary>
    public void Deselect()
    {
        Selected = false;
        SetHighlight(false);
    }

    /// <summary>
    /// attempts to select this die if it is not selected, and to deselect it if it is selected
    /// </summary>
    public void SelectOrDeselect()
    {
        if (Selected) Deselect();
        else Select();
    }

    /// <summary>
    /// resets this die's states
    /// </summary>
    public void Reset()
    {
        Roll();
        Locked = false;
        Deselect();
    }

    /// <summary>
    /// set highlight on or off
    /// </summary>
    /// <param name="enabled">true for on, false for off</param>
    private void SetHighlight(bool enabled)
    {
        sprRenderer.GetPropertyBlock(propBlock);
        float val = 0.0f;
        if (enabled) val = 1.0f;
        propBlock.SetFloat("_OutlineEnabled", val);
        sprRenderer.SetPropertyBlock(propBlock);
    }
}
