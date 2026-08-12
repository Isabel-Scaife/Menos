using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestUIToggle : MonoBehaviour
{
    [SerializeField] private GameObject questLog;

    public void OnOpen()
    {
        questLog.SetActive(true);
    }

    public void OnClose()
    {
        questLog.SetActive(false);
    }
}
