using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateSpawner : MonoBehaviour
{
    public LayerMask playerLayer;
    private int row=3, columns = 3;
    public GameObject[] objects;
    public GameObject[] Templates;
    public GameObject playerObject;
    public static bool queriesHitTriggers;
    public LayerMask levelLayer;
    public int listCounter=0;
    public List<GameObject>TemplatesAttached=new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        queriesHitTriggers = true;
        
        int rand = Random.Range(0, objects.Length);
       var newTemp= Instantiate(objects[rand], transform.position, Quaternion.identity);
       //newTemp.transform.parent = this.transform;
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Physics2D.BoxCast(transform.position , new Vector2(20f, 20f), 0f, Vector2.zero,20f, playerLayer)) {

            LevelSpawn();
        }
        Debug.Log(Vector3.Distance(transform.position, playerObject.transform.position));
        if (Vector3.Distance(transform.position,playerObject.transform.position)>100) {
            Despawn();
            if (TemplatesAttached==null) { }
            Destroy(this.gameObject);
        }
    }
   // Physics.CheckSphere(transform.position + sightRangeOffset, sightRange, playerLayer);


    void LevelSpawn() {
        //distances between centres is 25.5 x and y
     //   Debug.Log(Physics.CheckSphere(transform.position + new Vector3(25.5f, 0, 0), 1f, levelLayer, QueryTriggerInteraction.Collide));

        //!Physics2D.BoxCast(transform.position + new Vector3(25.5f, 0, 0), new Vector2(2f, 2f), 0f, Vector2.down, 1f, levelLayer)
      /*  if (!Physics.CheckSphere(transform.position + new Vector3(25.5f, 0, 0), 1f, levelLayer, QueryTriggerInteraction.Collide))
           
        {
            Instantiate(Templates[Random.Range(0, Templates.Length-1)], transform.position + new Vector3(25.5f, 0, 0), Quaternion.identity);
        }

        if (!Physics.CheckSphere(transform.position + new Vector3(25.5f, 25.5f, 0), 1f, levelLayer, QueryTriggerInteraction.Collide))
        {
            Instantiate(Templates[Random.Range(0, Templates.Length-1)], transform.position + new Vector3(25.5f, 25.5f, 0), Quaternion.identity);
        }

        if (!Physics.CheckSphere(transform.position + new Vector3(25.5f, -25.5f, 0), 1f, levelLayer, QueryTriggerInteraction.Collide))
        {
            Instantiate(Templates[Random.Range(0, Templates.Length-1)], transform.position + new Vector3(25.5f, -25.5f, 0), Quaternion.identity);
        }

        if (!Physics.CheckSphere(transform.position + new Vector3(-25.5f, 0, 0), 1f, levelLayer, QueryTriggerInteraction.Collide))
        {
            Instantiate(Templates[Random.Range(0, Templates.Length-1)], transform.position + new Vector3(-25.5f, 0, 0), Quaternion.identity);
        }

        if (!Physics.CheckSphere(transform.position + new Vector3(-25.5f, -25.5f, 0), 1f, levelLayer, QueryTriggerInteraction.Collide))
        {
            Instantiate(Templates[Random.Range(0, Templates.Length-1)], transform.position + new Vector3(-25.5f, -25.5f, 0), Quaternion.identity);
        }

        if (!Physics.CheckSphere(transform.position + new Vector3(-25.5f, 25.5f, 0), 1f, levelLayer, QueryTriggerInteraction.Collide))
        {
            Instantiate(Templates[Random.Range(0, Templates.Length - 1)], transform.position + new Vector3(-25.5f, 25.5f, 0), Quaternion.identity);
        }

        if (!Physics.CheckSphere(transform.position + new Vector3(0, 25.5f, 0), 1f, levelLayer, QueryTriggerInteraction.Collide))
        {
            Instantiate(Templates[Random.Range(0, Templates.Length - 1)], transform.position + new Vector3(0, 25.5f, 0), Quaternion.identity);
        }

        if (!Physics.CheckSphere(transform.position + new Vector3(0, -25.5f, 0), 1f, levelLayer, QueryTriggerInteraction.Collide))
        {
            Instantiate(Templates[Random.Range(0, Templates.Length - 1)], transform.position + new Vector3(0, 25.5f, 0), Quaternion.identity);
        }
      */
        if (!Physics2D.BoxCast(transform.position + new Vector3(25.5f, 0, 0), new Vector2(10f, 10f), 0f, Vector2.down, 10f, levelLayer))
        {
            TemplatesAttached.Add(Instantiate(Templates[Random.Range(0, Templates.Length - 1)], transform.position + new Vector3(25.5f, 0, 0), Quaternion.identity));
            listCounter++;
        }
        if (!Physics2D.BoxCast(transform.position + new Vector3(25.5f, 25.5f, 0), new Vector2(10f, 10f), 0f, Vector2.down, 10f, levelLayer))
        {
            listCounter++;
            TemplatesAttached.Add( Instantiate(Templates[Random.Range(0, Templates.Length - 1)], transform.position + new Vector3(25.5f, 25.5f, 0), Quaternion.identity));

        }
        if (!Physics2D.BoxCast(transform.position + new Vector3(25.5f, -25.5f, 0), new Vector2(10f, 10f), 0f, Vector2.down, 10f, levelLayer))
        {
            listCounter++;
            TemplatesAttached.Add(Instantiate(Templates[Random.Range(0, Templates.Length - 1)], transform.position + new Vector3(25.5f, -25.5f, 0), Quaternion.identity));

        }
        if (!Physics2D.BoxCast(transform.position + new Vector3(-25.5f, 0, 0), new Vector2(10f, 10f), 0f, Vector2.down, 10f, levelLayer))
        {
            listCounter++;
            TemplatesAttached.Add(Instantiate(Templates[Random.Range(0, Templates.Length - 1)], transform.position + new Vector3(-25.5f, 0, 0), Quaternion.identity));

        }
        if (!Physics2D.BoxCast(transform.position + new Vector3(-25.5f, -25.5f, 0), new Vector2(10f, 10f), 0f, Vector2.down, 10f, levelLayer))
        {
            listCounter++;
            TemplatesAttached.Add(Instantiate(Templates[Random.Range(0, Templates.Length - 1)], transform.position + new Vector3(-25.5f, -25.5f, 0), Quaternion.identity));

        }
        if (!Physics2D.BoxCast(transform.position + new Vector3(-25.5f, 25.5f, 0), new Vector2(10f, 10f), 0f, Vector2.down, 10f, levelLayer))
        {
            listCounter++;
            TemplatesAttached.Add(Instantiate(Templates[Random.Range(0, Templates.Length - 1)], transform.position + new Vector3(-25.5f, 25.5f, 0), Quaternion.identity));

        }
        if (!Physics2D.BoxCast(transform.position + new Vector3(0, 25.5f, 0), new Vector2(10f, 10f), 0f, Vector2.down, 10f, levelLayer))
        {
            listCounter++;
            TemplatesAttached.Add(Instantiate(Templates[Random.Range(0, Templates.Length - 1)], transform.position + new Vector3(0, 25.5f, 0), Quaternion.identity));

        }
        if (!Physics2D.BoxCast(transform.position + new Vector3(0, -25.5f, 0), new Vector2(10f, 10f), 0f, Vector2.down, 10f, levelLayer))
        {
            listCounter++;
            TemplatesAttached.Add(Instantiate(Templates[Random.Range(0, Templates.Length - 1)], transform.position + new Vector3(0, -25.5f, 0), Quaternion.identity));

        }






    }
    void Despawn() {
        for (int i = 1; i < listCounter-1; i++) {
            if (TemplatesAttached[i]!=null) {
                Destroy(TemplatesAttached[i]);
            }
        
        }
    
    }
    private void OnDrawGizmosSelected()
    {
       

    }
}
