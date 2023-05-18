using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossDoorEntity : MonoBehaviour
{
    public GameObject bossDoor;
    private SpriteRenderer sp;
    private int time;
    // Start is called before the first frame update
    void Start()
    {
        bossDoor = this.gameObject.GetComponent<GameObject>();
        sp = this.gameObject.GetComponent<SpriteRenderer>();
        sp.color = new Color32(255,255,255,0);
        time = 0;
        Invoke("colorChange",0.1f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void colorChange() {
        if(time < 10)
        {
            sp.color += new Color32(0,0,0,25);
            time--;
            Invoke("colorChange",0.1f);
        }
    }
}
