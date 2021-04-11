using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mover : MonoBehaviour
{
    public LocationInfo pos;
    public float breitengrad;
    public float längengrad;

    public GameObject großeKugel;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = großeKugel.transform.position + new Vector3(Mathf.Sin((längengrad/180)*Mathf.PI)*Mathf.Cos((breitengrad/180)*Mathf.PI)
                                                                             ,Mathf.Sin((breitengrad/180)*Mathf.PI)
                                                                             ,-Mathf.Cos((längengrad/180)*Mathf.PI)*Mathf.Cos((breitengrad/180)*Mathf.PI))
            * großeKugel.transform.localScale.x/2;
         
    }
}
