using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Image controlPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controlPanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickStart()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClickControls()
    {
        controlPanel.gameObject.SetActive(true);
    }

    public void ExitControls()
    {
        controlPanel.gameObject.SetActive(false);
    }

    public void OnClickExitButton()
    {
        // For Build
        // Later in our actual game, we should double check
        // if the player really wishes to exit from the game
        Application.Quit();

        // For Quitting in Unity Editor
        EditorApplication.isPlaying = false;
    }
}
