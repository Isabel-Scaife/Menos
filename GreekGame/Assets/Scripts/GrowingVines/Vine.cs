using UnityEngine;

public class Vine : Interactable
{
    private Leaf[] leaves;

    private int currentLeaf = 0;

    private int size = 0;


    private void Awake()
    {
        // add all leaf child to vine
        size = transform.childCount;
        leaves = new Leaf[size];

        for (int i = 0; i < size; i++)
        {
            leaves[i] = transform.GetChild(i).GetComponent<Leaf>();
        }
    }
    private void FixedUpdate()
    {
        // after reaching certain size, beign next leaf
        if (currentLeaf < size && leaves[currentLeaf].Grow())
        {
            currentLeaf++;

            // turn on next leaf if one exists
            if (currentLeaf + 1 < size)
            {
                leaves[currentLeaf].TurnOn();
            }
        }
    }

    public override void Interact(PlayerControlled player)
    {
        Debug.Log("cutting");

        // reset leaves
        for(int i = 0; i < size; i++)
        {
            leaves[i].TurnOff();
        }

        currentLeaf = 0;
    }
}