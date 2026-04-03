using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NoteEntryDatabase", menuName = "Scriptable Objects/NoteEntryDatabase")]
public class NoteEntryDatabase : ScriptableObject
{
    public string[] noteEntries;
}
