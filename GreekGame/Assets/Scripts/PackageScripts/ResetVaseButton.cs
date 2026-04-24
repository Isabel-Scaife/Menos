using UnityEngine;

public class ResetVaseButton : MonoBehaviour
{
    public void ResetVase()
    {
        VasePackage.Instance.ResetImage();
    }
}
