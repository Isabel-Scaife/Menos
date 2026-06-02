
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vase : Item
{
    [SerializeField]
    private GameObject vaseGamePrefab;

    [SerializeField]
    private uint stampSetIndex;

    public override void Interact(PlayerControlled player)
    {
        SpawnManager.Instance.LoadVase(itemID, vaseGamePrefab, stampSetIndex);
    }
}
