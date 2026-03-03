using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    // singleton
    public static PopupManager Instance;

    // fields
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private Image image;
    [SerializeField]
    private PlayerInput popupInput;
    [SerializeField]
    private PlayerControlled player;

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// switches input controls and sprite then shows
    /// </summary>
    /// <param name="sprite">new sprite to show</param>
    public void ShowPopup(Sprite sprite)
    {
        player.PauseInputControls();
        image.sprite = sprite;
        image.SetNativeSize();
        popupInput.enabled = true;
        panel.SetActive(true);
    }

    /// <summary>
    /// hides image on canvas and switches input control back to player
    /// </summary>
    /// <param name="context">input callback context</param>
    public void HidePopup(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            popupInput.enabled = false;
            panel.SetActive(false);
            player.ResumeInputControls();
        }
    }
}
