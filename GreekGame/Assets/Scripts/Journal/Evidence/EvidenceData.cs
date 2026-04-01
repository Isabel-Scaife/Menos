using UnityEditor.U2D.Animation;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EvidenceData", menuName = "Scriptable Objects/EvidenceData")]
public class EvidenceData : JournalEntries
{
    // [System.NonSerialized] is for testing purpose

    public List<RelationshipsData> possibleRelations;       // If we want more suspects this list makes it scalable.
                                                            // However, having more suspects.. need to fix the evidence popup button options as well

    //[Range(0, 19)]
    //[System.NonSerialized] public int buttonNum;

    public override void OpenPopup()
    {
        EvidencePopup.Instance.Show(this);

    }

    // Need to initialize lists to use them :(
    public void Initialize()
    {
        possibleRelations = new List<RelationshipsData>();
    }


    // Need reset method (Currently manually gonna reset the things, reminder for Amy)
}
