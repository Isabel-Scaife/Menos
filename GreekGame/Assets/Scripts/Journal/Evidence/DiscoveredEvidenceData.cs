using NUnit.Framework;
using UnityEngine;

public class DiscoveredEvidenceData : ISaveData<JournalManager>
{
    public EvidenceDataData[] savedEvidence;

    public void CreateSaveData(JournalManager journal)
    {
        Debug.Log("Amount of Evidence to save: " + journal.DiscoveredEvidence.Count);

        if (journal.DiscoveredEvidence.Count != 0)
        {
            savedEvidence = new EvidenceDataData[journal.DiscoveredEvidence.Count];

            for (int i = 0; i < journal.DiscoveredEvidence.Count; i++)
            {
                Debug.Log(journal.DiscoveredEvidence[i]);
                savedEvidence[i].CreateSaveData(journal.DiscoveredEvidence[i]);

                //SaveSystem.SaveData<EvidenceData, EvidenceDataData>
                //    (journal.DiscoveredEvidence[i], ID, "Evidence.json");
            }
        }
        
    }
}
