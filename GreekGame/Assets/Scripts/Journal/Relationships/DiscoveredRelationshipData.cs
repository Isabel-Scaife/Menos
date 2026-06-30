using UnityEngine;

public class DiscoveredRelationshipData : ISaveData<JournalManager>
{
    public string[] discoveredRelations;

    public void CreateSaveData(JournalManager journal)
    {
        discoveredRelations = new string[journal.UnlockedRelations.Count];

        for(int i = 0; i < journal.UnlockedRelations.Count; i++)
        {
            discoveredRelations[i] = journal.UnlockedRelations[i].entryID;
        }
    }
}
