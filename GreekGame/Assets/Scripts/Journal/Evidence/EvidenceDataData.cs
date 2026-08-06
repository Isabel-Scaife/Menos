using UnityEngine;

/// <summary>
/// Saving individual Evidence Data
/// </summary>
public class EvidenceDataData : ISaveData<EvidenceData>
{
    public string evidenceID;
    public string[] relatedRelationID;

    public void CreateSaveData(EvidenceData data)
    {
        // Save evidence ID
        evidenceID = data.entryID;


        if(data.possibleRelations.Count != 0)
        {
            // Set count of relations array
            relatedRelationID = new string[data.possibleRelations.Count];

            for (int i = 0; i < data.possibleRelations.Count; i++)
            {
                relatedRelationID[i] = data.possibleRelations[i].entryID;
            }
        }

        // * I don't think save boolean discovered value is necessarily needed
        //   bc we are only saving discovered evidence and reloading it (same logic for relationship saving)
    }
}
