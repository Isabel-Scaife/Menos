using UnityEngine;

[CreateAssetMenu(fileName = "RelationshipsData", menuName = "Scriptable Objects/RelationshipsData")]
public class RelationshipsData : JournalEntries
{
    // If needed, additional information we want to add to relationship will be added here

    public override void OpenPopup()
    {
        RelationshipsPopup.Instance.Show(this);
    }
}
