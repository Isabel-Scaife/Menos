using UnityEngine;
using System.Collections.Generic;
using TMPro;

/// <summary>
/// manages task flow and popups for early-game tutorial
/// </summary>
public class TutorialManager : MonoBehaviour
{
    // singleton
    public static TutorialManager Instance { get; private set; }
    
    // fields
    [SerializeField]
    private RectTransform popupRect;

    [SerializeField]
    private TMP_Text popupTMP;

    [SerializeField]
    private List<TutorialTask> tasks;

    private int currentTaskIndex;
    private bool taskActive;

    private void Awake()
    {
        Instance = this;
        currentTaskIndex = 0;
        taskActive = false;
    }

    private void Update()
    {
        // hides popup and disables task script when task is completed
        if (taskActive && tasks[currentTaskIndex].Completed())
        {
            tasks[currentTaskIndex].enabled = false;
            taskActive = false;
            popupRect.gameObject.SetActive(false);
            popupTMP.text = "";
            currentTaskIndex++;

            // disable this script if all tasks are completed
            if (currentTaskIndex >= tasks.Count)
            {
                this.enabled = false;
            }

            // else enables next task
            else
            {
                tasks[currentTaskIndex].enabled = true;
            }
        }

        // shows next task when ready
        else if (!taskActive && tasks[currentTaskIndex].Ready())
        {
            taskActive = true;
            popupTMP.text = tasks[currentTaskIndex].text;
            popupRect.position = Camera.main.WorldToScreenPoint(tasks[currentTaskIndex].anchor.position);
            popupRect.gameObject.SetActive(true);
        }
    }

    private void LateUpdate()
    {
        // update popup position on the screen after any camera movement
        if (!taskActive) return;
        popupRect.position = Camera.main.WorldToScreenPoint(tasks[currentTaskIndex].anchor.position);
    }
}
