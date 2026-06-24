
using UnityEngine;
using UnityEngine.SceneManagement;

public class VaseItem : Item
{
    [SerializeField]
    private GameObject vaseGamePrefab;

    [SerializeField]
    private uint stampSetIndex;

    public override void Interact(PlayerControlled player)
    {
        if (!canInteract) return;

        if (SpawnManager.Instance == null)
        {
            Debug.Log("No SpawnManager in scene");
        }
        else
        {
            SpawnManager.Instance.LoadVase(itemID, vaseGamePrefab, stampSetIndex);
        }

        base.Interact(player);
    }
}
