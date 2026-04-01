using UnityEngine;

[CreateAssetMenu(fileName = "JournalEntries", menuName = "Scriptable Objects/JournalEntries")]
public abstract class JournalEntries : ScriptableObject
{
    public string entryID;
    public string entryName;

    [TextArea]
    public string description;

    public Sprite icon;

    public abstract void OpenPopup();
}
