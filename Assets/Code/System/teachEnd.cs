using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class teachEnd : MonoBehaviour
{
    public GameObject black;
    private SpriteRenderer sp;
    private bool teach;
    // Start is called before the first frame update
    void Start()
    {
        sp = this.gameObject.GetComponent<SpriteRenderer>();
        sp.color = new Color32(255,255,255,128);
        teach = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        switch(coll.gameObject.tag)
        {
            case "Player":
                if(true)
                {
                    Instantiate(black, this.transform.position,Quaternion.Euler(0,0,0));
                    Invoke("gameStart",1);
                    teach = false;
                }
            break;
        }
    }

    public void gameStart()
    {
        SceneManager.LoadScene(1);
    }
}
