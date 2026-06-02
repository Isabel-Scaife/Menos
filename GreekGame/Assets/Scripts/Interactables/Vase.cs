
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vase : Interactable
{
    [SerializeField]
    private uint vaseId;

    [SerializeField]
    private uint stampSetIndex;

    private void Start()
    {
        // delete vase if manager can't find it 
        if(!SpawnManager.Instance.VaseExist(vaseId))
        {
            Destroy(this.gameObject);
        }
    }

    public override void Interact(PlayerControlled player)
    {
        SpawnManager.Instance.LoadVase(vaseId, stampSetIndex);
    }
}
