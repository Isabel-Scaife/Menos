using System;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCutscene : MonoBehaviour
{
    [SerializeField] private GameObject cutsceneParent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        cutsceneParent.SetActive(true);
    }
}
