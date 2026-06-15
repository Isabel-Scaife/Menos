using UnityEngine;

public class CollectionBox : MonoBehaviour
{
    [SerializeField]
    private int oliveMax;

    private int currentOliveCt;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentOliveCt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (oliveMax == currentOliveCt)
        {
            print("game win");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Olive")
        {
            //adds to olive count
            currentOliveCt++;
            //sends it into the ether so it cant be recounted
            //and gives appearance of entering crate
            collision.gameObject.transform.position = new Vector3(500,500,0);
        }
        print("Current Olives: " + currentOliveCt);
    }
}
