using System;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "RelationshipsDatabase", menuName = "Scriptable Objects/RelationshipsDatabase")]
public class RelationshipsDatabase : ScriptableObject
{
    public RelationshipsData[] Relationships;

    public RelationshipsData FindRelationshipByID(string entryID)
    {
        foreach(RelationshipsData relation in Relationships)
        {
            if (relation.entryID == entryID)
            {
                return relation;
            }
        }

        return null;    // We have found nothing
    }
}
