using UnityEngine;
using UnityEngine.InputSystem;

public class Letter : MonoBehaviour, IRaycast
{

    // UI related fields
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private GameObject initialLetterUI;
    [SerializeField]
    private GameObject scrollingLetterUI;


    /// <summary>
    /// switches to scrollable letter
    /// </summary>
    public void OpenLetter()
    {
        initialLetterUI.SetActive(false);
        scrollingLetterUI.SetActive(true);
    }

    /// <summary>
    /// goes back to minigame without showing scrollable letter UI
    /// </summary>
    public void SkipReading()
    {
        initialLetterUI.SetActive(false);
        spriteRenderer.enabled = true;
    }

    /// <summary>
    /// hides letter UI and goes back to minigame
    /// </summary>
    public void CloseLetter()
    {
        scrollingLetterUI.SetActive(false);
        spriteRenderer.enabled = true;
    }

    public void Interact()
    {
        spriteRenderer.enabled = false;
        initialLetterUI.SetActive(true);
    }
}
