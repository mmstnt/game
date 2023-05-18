using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class blackScreen : MonoBehaviour
{
    public GameObject black;
    public GameObject mainCamera; 
    private SpriteRenderer sp;
    private byte color;
    private int blackTime;
    // Start is called before the first frame update
    void Start()
    {
        black = this.gameObject.GetComponent<GameObject>();
        sp = this.gameObject.GetComponent<SpriteRenderer>();
        blackTime = 20;
        Invoke("colorChange",0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        mainCamera = GameObject.Find("Main Camera");
        Vector3 v = mainCamera.transform.position;
        this.gameObject.transform.position = new Vector3(v.x,v.y,0);
        sp.color = new Color32(255,255,255,color);
    }

    void Awake () {
        DontDestroyOnLoad(this.gameObject);
    }

    public void colorChange() {
        if(blackTime > 10)
        {
            color += 25;
            blackTime--;
            Invoke("colorChange",0.1f);
        }
        else if(blackTime > 0)
        {
            color -= 25;
            blackTime--;
            Invoke("colorChange",0.1f);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    
}
