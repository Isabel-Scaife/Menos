using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EvidenceDatabase", menuName = "Scriptable Objects/EvidenceDatabase")]
public class EvidenceDatabase : ScriptableObject
{
    public EvidenceData[] Evidences;

    // Retrieves the index of certain data
    // Later used for actual gameplay to place tags in evidence tab controller
    public int ReturnIndex(EvidenceData evidence)
    {
        return Array.IndexOf(Evidences, evidence);
    }

    public void InitializeAllEvidence()
    {
        for(int i = 0; i < Evidences.Length; i++)
        {
            Evidences[i].Initialize();
        }
    }

    public EvidenceData FindEvidenceByID(string entryID)
    {

    }
}
