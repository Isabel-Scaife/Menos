using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName = "UpdateStats", menuName = "Events/UpdateStats")]
public class UpdateStats : QuestEvent
{
    [SerializeField] private int[] changeBy; 

    public override void PlayEvent()
    {
        if(GameStateManager.Instance == null)
        {
            Debug.Log("Missing GameStateManager");
        }
        else
        {
            GameStateManager.Instance.ChangeStats(statChanges);
        }
    }
}
