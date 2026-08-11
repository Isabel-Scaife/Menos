using UnityEngine;

[System.Serializable]
public class LogInfo : ScriptableObject
{
    public string description;
    public int requiredAmount;
    public int currentAmount;
}
