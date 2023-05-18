using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossDoor : MonoBehaviour
{
    private GameObject camera;
    public GameObject bossAppearance;
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
                    mainCamera.site = new Vector3(0,36.5f,camera.transform.position.z);
                    mainCamera.follow = false;
                    playerController.cutSceneSite =new Vector3(-10.5f,player.transform.position.y,player.transform.position.z);
                    playerController.cutScene = true;
                    Invoke("bossDoorClose",1);
                } 
            break;
        }
    }

    void bossDoorClose()
    {
        Instantiate(bossAppearance,new Vector3(8,32.3f,0),Quaternion.Euler(-90,0,0));
        Instantiate(bossDoorEntity,this.gameObject.transform.position,Quaternion.Euler(0,0,0));
        Destroy(this.gameObject);
    }

    
}
