using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manager for simple UI popops where the only interaction is closing
/// </summary>
public class PopupManager : MonoBehaviour
{
    // singleton
    public static PopupManager Instance { get; private set; }

    // fields
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private Image image;
    private PlayerControlled interactingPlayer;

    private void Awake()
    {
        // destroy duplicate instance if one of this singleton already exists
        if (Instance != null && Instance != this)
        {
            Debug.Log("Destroyed duplicate PopupManager object");
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        Instance = this;
    }

    /// <summary>
    /// disables player movement, switches sprite, and shows
    /// </summary>
    /// <param name="sprite">new sprite to show</param>
    /// <param name="player">player who interacted with the item that 
    /// caused the popup to show</param>
    public void ShowPopup(Sprite sprite, PlayerControlled player)
    {
        interactingPlayer = player;
        interactingPlayer.PauseInputControls();
        image.sprite = sprite;
        image.SetNativeSize();
        panel.SetActive(true);
    }

    /// <summary>
    /// hides image reenables player movement
    /// </summary>
    public void HidePopup()
    {
        panel.SetActive(false);
        if (interactingPlayer != null)
        {
            interactingPlayer.ResumeInputControls();
            interactingPlayer = null;
        }
    }
}
