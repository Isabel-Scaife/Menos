using UnityEngine;

public class NPCLoadCutscene : NPCEvent
{
    [SerializeField] private GameObject cutSceneParent;

    protected override void AddAfterDialogueEvent()
    {
        cutSceneParent.SetActive(true);
    }
}
