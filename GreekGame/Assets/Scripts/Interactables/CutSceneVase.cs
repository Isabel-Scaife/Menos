using UnityEngine;

public class CutSceneVase : Vase
{
    [SerializeField] private GameObject cutSceneParent;

    protected override void HandleComplete()
    {
        base.HandleComplete();
        cutSceneParent.SetActive(true);
    }
}
