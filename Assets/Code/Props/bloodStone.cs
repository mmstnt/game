using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bloodStone : MonoBehaviour
{
    private SpriteRenderer sp;
    private Rigidbody2D rigid2D;
    public GameObject bloodStoneEffect;
    // Start is called before the first frame update
    void Start()
    {
        sp = this.gameObject.GetComponent<SpriteRenderer>();
        rigid2D = this.gameObject.GetComponent<Rigidbody2D>();
        blood = true;
        color = 255;
        disappearTime = 10;
    }

    // Update is called once per frame
    void Update()
    {
        sp.color = new Color32(255,255,255,color);
    }

    private bool blood;
    private int disappearTime;
    private byte color;


    void OnTriggerEnter2D(Collider2D coll)
    {
        switch(coll.gameObject.tag)
        {
            case "Player":
                if(blood)
                {
                    playerController.hp++;
                    blood = false;
                    Instantiate(bloodStoneEffect,coll.transform.position,Quaternion.Euler(0,0,0),coll.transform);
                    Invoke("disappear",0.05f);
                }
            break;
        }
    }

    public void disappear() {
        if(disappearTime > 0)
        {
            color -= 25;
            disappearTime--;
            Invoke("disappear",0.05f);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    
}
