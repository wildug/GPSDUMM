using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mover : MonoBehaviour
{
    public LocationInfo pos;
    public float breitengrad;
    public float längengrad;

    public GameObject großeKugel;

    public float dist; 

    Camera camera ;

    // Start is called before the first frame update
    void Start()
    {

        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        längengrad = Input.location.lastData.longitude;
        breitengrad= Input.location.lastData.latitude;

        this.transform.position = großeKugel.transform.position + new Vector3(Mathf.Sin((längengrad/180)*Mathf.PI)*Mathf.Cos((breitengrad/180)*Mathf.PI)
                                                                             ,Mathf.Sin((breitengrad/180)*Mathf.PI)
                                                                             ,-Mathf.Cos((längengrad/180)*Mathf.PI)*Mathf.Cos((breitengrad/180)*Mathf.PI))
            * großeKugel.transform.localScale.x/2;

        camera.transform.position = (transform.position - großeKugel.transform.position) * dist + großeKugel.transform.position;
        camera.transform.forward = großeKugel.transform.position - transform.position;
    }
}
