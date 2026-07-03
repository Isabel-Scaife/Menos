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
    private bool readyToStart;

    private void Awake()
    {
        Instance = this;
        currentTaskIndex = 0;
        taskActive = false;
        readyToStart = false;
    }

    private void Update()
    {
        // don't update if tutorial shouldn't start yet
        if (!readyToStart) return;

        // hides popup and disables task script when task is completed
        else if (taskActive && tasks[currentTaskIndex].Completed())
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

    /// <summary>
    /// start checking for tutorial tasks to show
    /// </summary>
    public void Begin()
    {
        readyToStart = true;
    }
}
