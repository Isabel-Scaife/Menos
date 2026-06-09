using UnityEngine;

public class ResetVaseButton : MonoBehaviour
{
    public void ResetVase()
    {
        Vase.Instance.ResetImage();

        Debug.Log(Vase.Instance.gameObject.name);
    }
}
