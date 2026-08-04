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

    public int correctCount;

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

            FishSort();
            FishConvert();
        }

        //checks if game has been won (all fish correct)
        if (correctCount >= 5)
        {
            //game win or whatever
            Debug.Log("Game win~!");
        }
    }
    /// <summary>
    /// sorts fish by size and assigns id's in order
    /// </summary>
    private void FishSort()
    {
        //bubble sort to put fih in the right spot
        int n = fishList.Count;

        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (fishList[j].trueSize > fishList[j + 1].trueSize)
                {
                    Fish temp = fishList[j];
                    fishList[j] = fishList[j + 1];
                    fishList[j + 1] = temp;
                }
            }
        }
    }

    /// <summary>
    /// converts fish from fishlist into sortable fish and spwans them
    /// </summary>
    private void FishConvert()
    {
        FishToSort newFish;
        //assigns ID in here based on its location in the array, which was already sorted
        int fishID = 0;
        foreach (Fish pondFish in fishList)
        {

            //---THIS IS WHERE THE FIH ARE TRANSFERRED---

            // create fish and apply data 
            newFish = Instantiate(SortFish);

            // in unity you can not use traditional instantiate, so do this instead 
            newFish.Initialize(pondFish.trueSize, pondFish.colorNumR, pondFish.colorNumG, pondFish.colorNumB);
            newFish.id = fishID;
            newFish.transform.position = new Vector3(22-fishID, 3, 0);
            fishID++;
        }
    }
}
