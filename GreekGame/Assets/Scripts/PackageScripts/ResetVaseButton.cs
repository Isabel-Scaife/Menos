using UnityEngine;

public class ResetVaseButton : MonoBehaviour
{
    public void ResetVase()
    {
        VaseMinigame.Instance.ResetImage();

        Debug.Log(VaseMinigame.Instance.gameObject.name);
    }
}
