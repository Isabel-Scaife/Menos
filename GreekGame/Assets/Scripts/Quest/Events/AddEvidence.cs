using UnityEngine;

public class AddEvidence : MonoBehaviour, IEvent
{
    [SerializeField]
    public EvidenceData evidenve;

    public void OnQuestComplete()
    {
        if (evidenve != null)
        {
            evidenve.discovered = true;
        }
    }
}
