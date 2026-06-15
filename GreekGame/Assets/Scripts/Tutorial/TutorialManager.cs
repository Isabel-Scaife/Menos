using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    // singleton
    public static TutorialManager Instance { get; private set; }
    
    // fields
    [SerializeField]
    private RectTransform popupRect;

    [SerializeField]
    private TMP_Text popupTMP;

    private TutorialTask currentTask;

    private void Awake()
    {
        Instance = this;
        currentTask = null;

        Debug.Log($"TutorialManager Awake on {name}");
        Debug.Log($"popupRect = {popupRect}");
    }

    private void Update()
    {
        // hides popup when task is completed        
        if (currentTask != null && currentTask.Completed())
        {
            popupRect.gameObject.SetActive(false);
            popupTMP.text = "";
            currentTask = null;
        }
    }

    private void LateUpdate()
    {
        // update popup position on the screen after any camera movement
        if (currentTask == null) return;
        popupRect.position = Camera.main.WorldToScreenPoint(currentTask.anchor.position);
    }

    public void ShowTask(TutorialTask task)
    {
        // do not show if already showing a task
        if (currentTask != null) return;
        
        currentTask = task;
        popupTMP.text = currentTask.text;
        popupRect.position = Camera.main.WorldToScreenPoint(currentTask.anchor.position);
        popupRect.gameObject.SetActive(true);
    }
}
