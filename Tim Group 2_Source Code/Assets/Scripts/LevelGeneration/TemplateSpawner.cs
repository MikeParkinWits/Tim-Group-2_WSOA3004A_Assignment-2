using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateSpawner : MonoBehaviour
{
    public LayerMask playerLayer;
    private int row=3, columns = 3;
    public GameObject[] objects;
    public GameObject[] Templates;
    public LayerMask levelLayer;
    // Start is called before the first frame update
    void Start()
    {
        
        int rand = Random.Range(0, objects.Length);
        Instantiate(objects[rand], transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.CheckSphere(transform.position,20f, playerLayer, QueryTriggerInteraction.Collide)) {
            LevelSpawn();
        }
    }
   // Physics.CheckSphere(transform.position + sightRangeOffset, sightRange, playerLayer);


    void LevelSpawn() {
        //distances between centres is 25.5 x and y
        Debug.Log(Physics.CheckSphere(transform.position + new Vector3(25.5f, 0, 0), 1f, levelLayer, QueryTriggerInteraction.Collide));
     
        
        if (!Physics.CheckSphere(transform.position +new Vector3(25.5f,0,0) ,1f, levelLayer,QueryTriggerInteraction.Collide))
        {
            Instantiate(objects[Random.Range(0, Templates.Length)], transform.position + new Vector3(25.5f, 0, 0), Quaternion.identity);
        }

        if (!Physics.CheckSphere(transform.position + new Vector3(25.5f, 25.5f, 0), 1f, levelLayer, QueryTriggerInteraction.Collide))
        {
            Instantiate(objects[Random.Range(0, Templates.Length)], transform.position + new Vector3(25.5f, 25.5f, 0), Quaternion.identity);
        }

        if (!Physics.CheckSphere(transform.position + new Vector3(25.5f, -25.5f, 0), 1f, levelLayer, QueryTriggerInteraction.Collide))
        {
            Instantiate(objects[Random.Range(0, Templates.Length)], transform.position + new Vector3(25.5f, -25.5f, 0), Quaternion.identity);
        }

        if (!Physics.CheckSphere(transform.position + new Vector3(-25.5f, 0, 0), 1f, levelLayer, QueryTriggerInteraction.Collide))
        {
            Instantiate(objects[Random.Range(0, Templates.Length)], transform.position + new Vector3(-25.5f, 0, 0), Quaternion.identity);
        }

        if (!Physics.CheckSphere(transform.position + new Vector3(-25.5f, -25.5f, 0), 1f, levelLayer, QueryTriggerInteraction.Collide))
        {
            Instantiate(objects[Random.Range(0, Templates.Length)], transform.position + new Vector3(-25.5f, -25.5f, 0), Quaternion.identity);
        }

        if (!Physics.CheckSphere(transform.position + new Vector3(-25.5f, 25.5f, 0), 1f, levelLayer, QueryTriggerInteraction.Collide))
        {
            Instantiate(objects[Random.Range(0, Templates.Length)], transform.position + new Vector3(-25.5f, 25.5f, 0), Quaternion.identity);
        }

        if (!Physics.CheckSphere(transform.position + new Vector3(0, 25.5f, 0), 1f, levelLayer, QueryTriggerInteraction.Collide))
        {
            Instantiate(objects[Random.Range(0, Templates.Length)], transform.position + new Vector3(0, 25.5f, 0), Quaternion.identity);
        }

        if (!Physics.CheckSphere(transform.position + new Vector3(0, -25.5f, 0), 1f, levelLayer, QueryTriggerInteraction.Collide))
        {
            Instantiate(objects[Random.Range(0, Templates.Length)], transform.position + new Vector3(0, 25.5f, 0), Quaternion.identity);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position+ new Vector3(25f,0,0),1f);

    }
}
