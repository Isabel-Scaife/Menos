using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FishSpawnManager : MonoBehaviour
{
    //counts every frame to know how often to spawn fish
    int frameCount;

    //fish prefab
    [SerializeField]
    private GameObject PondFish;

    [SerializeField]
    private FishToSort SortFish;

    [SerializeField]
    private GameObject fishParent;

    //list of fish caught
    public List<Fish> fishList;

    //true if player is in the water
    private bool catchingFish;

    //fish prefab
    [SerializeField]
    private Camera camera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //total list of fih
        List<Fish> fishList = new List<Fish>();

        catchingFish = true;
    }

    // Update is called once per frame
    void Update()
    {
        //adds for each frame called
        frameCount++;

        //if 500 frames have passed, spawns a fish
        if (frameCount >= 500 && catchingFish == true)
        {
            Debug.Log("Fish spawned");

            Instantiate(PondFish);

            //resets frame count
            frameCount = 0;
        }

        //checks if 5 fish have been caught
        //only runs once bc catching fish turns off
        ///runs upon switching from fish catching to fish sorting
        if (fishList.Count>=5 && catchingFish == true)
        {
            catchingFish = false;

            camera.transform.position = new Vector3(23, 0, -10);

            //FishSort();
            FishConvert();
        }
    }

    /// <summary>
    /// sorts fish by size and assigns id's in order
    /// </summary>
    private void FishSort()
    {

    }

    /// <summary>
    /// converts fish from fishlist into sortable fish and spwans them
    /// </summary>
    private void FishConvert()
    {
        FishToSort newFish;
        foreach (Fish pondFish in fishList)
        {
            newFish = Instantiate(SortFish, fishParent.transform);
            newFish.AddComponent<FishToSort>().colorNumR = pondFish.colorNumR;
            newFish.AddComponent<FishToSort>().colorNumG = pondFish.colorNumG;
            newFish.AddComponent<FishToSort>().colorNumB = pondFish.colorNumB;
            newFish.AddComponent<FishToSort>().size = pondFish.trueSize;
            newFish.transform.position = new Vector3(22, 5, 0);
        }
    }
}
