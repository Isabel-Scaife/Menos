using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// Manager for simple UI popops where the only interaction is closing
/// </summary>
public class PopupManager : MonoBehaviour
{
    // singleton
    public static PopupManager Instance;

    // fields
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private Image image;

    //[SerializeField]
    //private PlayerInput popupInput;

    [SerializeField]
    private PlayerControlled player;

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// disables player movement, switches sprite, and shows
    /// </summary>
    /// <param name="sprite">new sprite to show</param>
    public void ShowPopup(Sprite sprite)
    {
        player.PauseInputControls();
        image.sprite = sprite;
        image.SetNativeSize();
        //popupInput.enabled = true;
        panel.SetActive(true);
    }

    /// <summary>
    /// hides image and switches input control back to player
    /// </summary>
    /// <param name="context">input callback context</param>
    public void HidePopup(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            //popupInput.enabled = false;
            panel.SetActive(false);
            player.ResumeInputControls();
        }
    }
}
