using UnityEngine;

/// <summary>
/// Anything or anyone the player can interact with in the overworld
/// </summary>
public abstract class Interactable : MonoBehaviour
{
    public abstract void Interact(PlayerControlled player);
}