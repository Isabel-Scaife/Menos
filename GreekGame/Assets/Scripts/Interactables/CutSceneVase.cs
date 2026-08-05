using UnityEngine;

public class CutSceneVase : Vase
{
    [SerializeField] private EvidenceData evidence;
    [SerializeField] private GameObject cutSceneParent;

    protected override void HandleComplete()
    {
        base.HandleComplete();

        JournalManager.Instance.UnlockEvidence(evidence);
        cutSceneParent.SetActive(true);
    }
}
