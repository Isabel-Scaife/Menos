using UnityEngine;
using System;
using Unity.VisualScripting;


public class VineDragMinigame : MonoBehaviour
{
    public event Action OnComplete;

    [SerializeField] private int amtToWin;
    [SerializeField] private int count = 0;
    [SerializeField] private RectTransform coveredObject;

    public Vector3[] coveredCorners;
    public static VineDragMinigame Instance { get; private set; }

    void Awake()
    {
        if (Instance!= null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        coveredCorners = new Vector3[4];
        coveredObject.GetWorldCorners(coveredCorners);
    }

    public void Complete()
    {
        // play animation or sound effect 

        // run listening methods 
        if (OnComplete != null) OnComplete();
    }

    public void VineOff()
    {
        count++;
        Debug.Log("Moved off count: " + count);

        // check win condition
        if (count >= amtToWin)
        {
            Complete();
        }
    }

    public void VineOn()
    {
        count--;
    }
}
