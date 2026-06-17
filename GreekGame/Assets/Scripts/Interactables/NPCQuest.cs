using UnityEngine;

public class NPCQuest : NPC
{
    [SerializeField] private QuestData quest;

    private void Awake()
    {
        IEvent[] eventsTemp = this.GetComponents<IEvent>();

        foreach (IEvent e in eventsTemp)
        {
            Debug.Log("adding event");
            quest.AddEvent(e);
        }
    }

    /// <summary>
    /// Talk to NPC and complete quest 
    /// </summary>
    /// <param name="player"></param>
    public override void Interact(PlayerControlled player)
    {
        Debug.Log("completing quest");
        QuestManager.Instance.CompleteQuest(quest);

        base.Interact(player);
    }
}
