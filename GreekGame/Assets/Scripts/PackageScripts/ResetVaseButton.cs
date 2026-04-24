using UnityEngine;

public class ResetVaseButton : MonoBehaviour
{
    public void ResetVase()
    {
        VasePackage.Instance.ResetImage();

        Debug.Log(VasePackage.Instance.gameObject.name);
    }
}
