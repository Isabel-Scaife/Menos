using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName = "Evidence", menuName = "Events/Evidence")]
public class AddEvidence : QuestEvent
{
    [SerializeField]
    public EvidenceData evidenve;

    public override void PlayEvent()
    {
        if (evidenve != null)
        {
            evidenve.discovered = true;
        }
    }
}
