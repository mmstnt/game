using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossDoor : MonoBehaviour
{
    private GameObject camera;
    public GameObject boss;
    public GameObject bossDoorEntity;
    public GameObject player;
    public bool bossOpen;
    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("Main Camera");
        player = GameObject.Find("Player");
        bossOpen = true;
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
                if(bossOpen)
                {
                    bossOpen = false;
                    mainCamera.site = new Vector3(13,36.5f,camera.transform.position.z);
                    mainCamera.status = mainCamera.Status.move;
                    playerController.cutSceneSite = -10.5f;
                    playerController.cutScene = true;
                    Invoke("bossDoorClose",1.25f);
                } 
            break;
        }
    }

    void bossDoorClose()
    {
        Instantiate(boss,new Vector3(21,32.0f,0),Quaternion.Euler(0,0,0));
        Instantiate(bossDoorEntity,this.gameObject.transform.position,Quaternion.Euler(0,0,0));
        Destroy(this.gameObject);
    }

    
}
