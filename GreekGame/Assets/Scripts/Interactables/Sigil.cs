using UnityEngine;

/// <summary>
/// Switch that can be turned on or off when interacted with and checked by other scripts
/// </summary>
public class Sigil : MonoBehaviour
{
    // fields
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite sprOn;
    [SerializeField] private Sprite sprOff;
    [SerializeField] private bool isOn;

    // properties
    public bool IsOn
    {
        get { return isOn; }
    }

    // functions
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (isOn) spriteRenderer.sprite = sprOn;
        else spriteRenderer.sprite = sprOff;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isOn) spriteRenderer.sprite = sprOff;
        else spriteRenderer.sprite = sprOn;
        isOn = !isOn;
    }
}
