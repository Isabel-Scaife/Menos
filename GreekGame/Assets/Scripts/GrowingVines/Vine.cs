using UnityEngine;

public class Vine : MonoBehaviour
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
        // if current leaf is proper size start growing next leaf
        if (currentLeaf < size && leaves[currentLeaf].Grow())
        {
            currentLeaf++;
        }
    }
}