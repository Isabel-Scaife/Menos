using UnityEngine;

public class Exit : MonoBehaviour
{ 
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
