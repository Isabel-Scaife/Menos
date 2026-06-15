//using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] SaveSlotMenu saveSlotMenu;
    public Image controlPanel;

    void Start()
    {
        controlPanel.gameObject.SetActive(false);
    }

    public void ActivateMenu()
    {
        this.gameObject.SetActive (true);
    }

    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }

    public void OnStartClicked()
    {
        DeactivateMenu();
        saveSlotMenu.ActivateMenu();
    }

    public void OnControlsClicked()
    {
        controlPanel.gameObject.SetActive(true);
    }

    public void ExitControls()
    {
        controlPanel.gameObject.SetActive(false);
    }

    public void OnExitClicked()
    {
        // For Build
        // Later in our actual game, we should double check
        // if the player really wishes to exit from the game
        Application.Quit();

        // For Quitting in Unity Editor
        // EditorApplication.isPlaying = false;
    }
}
