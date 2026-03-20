using UnityEngine;

/// <summary>
/// Switch that can be turned on or off when interacted with and checked by other scripts
/// </summary>
public class Sigil : Interactable
{
    // fields
    [SerializeField]
    private Sprite sprOn;
    [SerializeField]
    private Sprite sprOff;
    [SerializeField]
    private SpriteRenderer sprRenderer;
    [SerializeField]
    private bool isOn;

    // properties
    public bool IsOn
    {
        get { return isOn; }
    }

    // functions
    private void Start()
    {
        if (isOn) sprRenderer.sprite = sprOn;
        else sprRenderer.sprite = sprOff;
    }

    public override void Interact(PlayerControlled player)
    {
        if (isOn) sprRenderer.sprite = sprOff;
        else sprRenderer.sprite = sprOn;
        isOn = !isOn;
    }
}
