using UnityEngine;

// one die in a dice game
public class Dice : MonoBehaviour
{
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

    /// <summary>
    /// randomizes this die's value 1-6
    /// </summary>
    public void Roll()
    {
        Value = Random.Range(1, 7);
        // TODO: change visuals
    }

    /// <summary>
    /// selects this die as long as it is not locked
    /// </summary>
    public void Select()
    {
        if (!Locked) Selected = true;
    }

    /// <summary>
    /// deselects this die
    /// </summary>
    public void Deselect()
    {
        Selected = false;
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
        Selected = false;
    }
}
