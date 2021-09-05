using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObjectSpawner : MonoBehaviour
{
    public GameObject[] objects;
    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range(0, objects.Length);
        Instantiate(objects[rand], transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

        //use a physics check to see if player is inside area, if the player is inside area, check 6  points around and spawn  levels if they do not exist already, use a physics check to see for other level elements

        
    }
}
