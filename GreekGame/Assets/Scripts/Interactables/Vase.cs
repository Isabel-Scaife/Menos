
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vase : Interactable
{
    [SerializeField]
    private uint vaseID;

    public override void Interact(PlayerControlled player)
    {
        ItemManager.Instance.LoadVase(vaseID);

    }
}
