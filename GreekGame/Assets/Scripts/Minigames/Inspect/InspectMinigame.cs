using UnityEngine;
using System;

public class InspectMinigame : MonoBehaviour
{
    public event Action OnComplete;

    public void Complete()
    {
        // play animation or sound effect 

        // run listening methods 
        if (OnComplete != null) OnComplete();
    }

}
