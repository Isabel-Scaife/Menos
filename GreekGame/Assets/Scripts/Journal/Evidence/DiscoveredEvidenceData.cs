using NUnit.Framework;
using UnityEngine;

public class DiscoveredEvidenceData : ISaveData<JournalManager>
{
    public EvidenceDataData[] savedEvidence;

    public void CreateSaveData(JournalManager journal)
    {
        savedEvidence = new EvidenceDataData[journal.DiscoveredEvidence.Count];

        for(int i = 0; i < journal.DiscoveredEvidence.Count; i++)
        {
            savedEvidence[i].CreateSaveData(journal.DiscoveredEvidence[i]);
        }
    }
}
