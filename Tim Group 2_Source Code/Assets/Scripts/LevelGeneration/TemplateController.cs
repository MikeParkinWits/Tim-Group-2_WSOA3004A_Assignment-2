using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateController : MonoBehaviour
{
    public LayerMask playerLayer;
    private int row = 3, columns = 3;
    public GameObject[] SpawnPoints;
    public GameObject[] objects;
    public GameObject[] Templates;
    public GameObject playerObject;
    public static bool queriesHitTriggers;
    public LayerMask levelLayer;
    // Start is called before the first frame update
    void Start()
    {
        queriesHitTriggers = true;

        
        // newTemp.transform.parent = this.transform;
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

       
       
        if (Vector3.Distance(transform.position, playerObject.transform.position) > 100)
        {

            Destroy(transform.parent.gameObject);
        }
    }


    /*void spawnObjects() {
        for (int i=0;i<SpawnPoints.Length-1;i++) {

            int rand = Random.Range(0, objects.Length);
            var newTemp = Instantiate(objects[rand], SpawnPoints[i].transform.position, Quaternion.identity);
            newTemp.transform.parent = SpawnPoints[i].transform;
        }
    
    }

    void LevelSpawn()
    {
        //distances between centres is 25.5 x and y
       

        if (!Physics2D.BoxCast(transform.position + new Vector3(25.5f, 0, 0), new Vector2(10f, 10f), 0f, Vector2.down, 10f, levelLayer))
        {
            Instantiate(Templates[Random.Range(0, Templates.Length - 1)], transform.position + new Vector3(25.5f, 0, 0), Quaternion.identity);

        }
        if (!Physics2D.BoxCast(transform.position + new Vector3(25.5f, 25.5f, 0), new Vector2(10f, 10f), 0f, Vector2.down, 10f, levelLayer))
        {
            Instantiate(Templates[Random.Range(0, Templates.Length - 1)], transform.position + new Vector3(25.5f, 25.5f, 0), Quaternion.identity);

        }
        if (!Physics2D.BoxCast(transform.position + new Vector3(25.5f, -25.5f, 0), new Vector2(10f, 10f), 0f, Vector2.down, 10f, levelLayer))
        {
            Instantiate(Templates[Random.Range(0, Templates.Length - 1)], transform.position + new Vector3(25.5f, -25.5f, 0), Quaternion.identity);

        }
        if (!Physics2D.BoxCast(transform.position + new Vector3(-25.5f, 0, 0), new Vector2(10f, 10f), 0f, Vector2.down, 10f, levelLayer))
        {
            Instantiate(Templates[Random.Range(0, Templates.Length - 1)], transform.position + new Vector3(-25.5f, 0, 0), Quaternion.identity);

        }
        if (!Physics2D.BoxCast(transform.position + new Vector3(-25.5f, -25.5f, 0), new Vector2(10f, 10f), 0f, Vector2.down, 10f, levelLayer))
        {
            Instantiate(Templates[Random.Range(0, Templates.Length - 1)], transform.position + new Vector3(-25.5f, -25.5f, 0), Quaternion.identity);

        }
        if (!Physics2D.BoxCast(transform.position + new Vector3(-25.5f, 25.5f, 0), new Vector2(10f, 10f), 0f, Vector2.down, 10f, levelLayer))
        {
            Instantiate(Templates[Random.Range(0, Templates.Length - 1)], transform.position + new Vector3(-25.5f, 25.5f, 0), Quaternion.identity);

        }
        if (!Physics2D.BoxCast(transform.position + new Vector3(0, 25.5f, 0), new Vector2(10f, 10f), 0f, Vector2.down, 10f, levelLayer))
        {
            Instantiate(Templates[Random.Range(0, Templates.Length - 1)], transform.position + new Vector3(0, 25.5f, 0), Quaternion.identity);

        }
        if (!Physics2D.BoxCast(transform.position + new Vector3(0, -25.5f, 0), new Vector2(10f, 10f), 0f, Vector2.down, 10f, levelLayer))
        {
            Instantiate(Templates[Random.Range(0, Templates.Length - 1)], transform.position + new Vector3(0, -25.5f, 0), Quaternion.identity);

        }






    }
    */
}
