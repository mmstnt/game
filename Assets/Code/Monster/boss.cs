using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour
{
    public GameObject bossAppear;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(bossAppear,this.gameObject.transform.position,Quaternion.Euler(-90,0,0));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
