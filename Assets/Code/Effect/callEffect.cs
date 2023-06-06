using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class callEffect : MonoBehaviour
{
    public GameObject cub;
    public GameObject flyCub;
    private GameObject summonWho;
    // Start is called before the first frame update
    void Start()
    {
        call();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void call()
    {
        Vector3 v = this.gameObject.transform.position;
        int who = UnityEngine.Random.Range(1,3);
        switch(who)
        {
            case 1:
            v.x += UnityEngine.Random.Range(-3.0f,3.0f);
            v.y -= 2;
            this.gameObject.transform.position = v;
            summonWho = cub;
            Invoke("summon",1);
            break;
            case 2:
            v.x += UnityEngine.Random.Range(-3.0f,3.0f);
            v.y += UnityEngine.Random.Range(2.0f,4.0f);
            this.gameObject.transform.position = v;
            summonWho = flyCub;
            Invoke("summon",1);
            break;
        }
    }

    private void summon()
    {
        Instantiate(summonWho,this.gameObject.transform.position,Quaternion.Euler(0,0,0));
    }
}
