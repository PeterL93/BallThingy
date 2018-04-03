using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class Pool : MonoBehaviour
{
    public GameObject prefab;                                 //The column game object.
    public int poolSize = 5;                                  //How many columns to keep on standby.

    private GameObject[] objects;                                   //Collection of pooled columns.
    public int currentObject = -1;                                  //Index of the current column in the collection.

    private Vector2 objectPoolPosition = new Vector2(-15, -25);     //A holding position for our unused columns offscreen.

    void Start()
    {
        //      timeSinceLastSpawned = 0f;

        //Initialize the columns collection.
        objects = new GameObject[poolSize];
        //Loop through the collection... 
        for (int i = 0; i < poolSize; i++)
        {
            //...and create the individual columns.

            objects[i] = (GameObject)Instantiate(prefab, objectPoolPosition, Quaternion.identity);
            //NetworkServer.Spawn(objects[i]);
            objects[i].SetActive(false);
        }
    }


    //This returns the next object
    public GameObject GetObject()
    {
        //Increase the value of currentColumn. If the new size is too big, set it back to zero
        currentObject++;

        if (currentObject >= poolSize)
        {
            currentObject = 0;
        }
        if (objects[currentObject].activeInHierarchy)
        {
            DoublePool();
            currentObject = poolSize / 2;
        }
        return objects[currentObject];
    } 

    void DoublePool()
    {
        int currentSize = poolSize;
        poolSize *= 2;
        GameObject[] newObjects = new GameObject[poolSize];
        for (int i = 0; i<objects.Length; i++)
        {
            newObjects[i] = objects[i];
        }
        for (int i = currentSize; i<poolSize; i++)
        {
            newObjects[i] = (GameObject)Instantiate(prefab, objectPoolPosition, Quaternion.identity);
            //NetworkServer.Spawn(objects[i]);
            newObjects[i].SetActive(false);
        }
        objects = newObjects;
    }
}