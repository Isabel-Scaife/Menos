using UnityEngine;

public class SigilDoor : Interactable
{
    // fields
    // sigils should be indexed based on x position, e.g. sigils[0] = leftmost sigil
    [SerializeField]
    private Sigil[] sigils;

    // true represents the corresponding sigil being on, false represents it being off
    [SerializeField]
    private bool[] pattern;
    
    public override void Interact(PlayerControlled player)
    {
        if (!SigilsMatchPattern())
        {
            // TODO: play sound and/or show message saying the pattern doesn't match
            Debug.Log("The door is locked!");
            return;
        }

        // TODO: allow the player to open the door and remove debug log
        Debug.Log("The door is unlocked!");
    }

    /// <summary>
    /// checks if the sigils in the room match the pattern needed to unlock
    /// </summary>
    /// <returns>true if pattern is matched, false otherwise</returns>
    private bool SigilsMatchPattern()
    {
        // makes sure arrays can be compared
        int len = sigils.Length;
        if (len != pattern.Length)
        {
            Debug.Log("Given number of sigils does not match number in pattern");
            return false;
        }

        // returns false if a sigil that should be on is off or one that should be off is on
        for (int i = 0; i < len; i++)
        {
            if (pattern[i] != sigils[i].IsOn) return false;
        }

        // returns true if everything matched
        return true;
    }
}
