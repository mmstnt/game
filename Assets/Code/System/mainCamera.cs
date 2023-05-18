using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainCamera : MonoBehaviour
{
    public GameObject player;
    public static bool follow;
    public static Vector3 site;
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        follow = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(follow)
        {
            Vector3 v = player.transform.position;
            this.gameObject.transform.position = new Vector3(v.x,v.y,-10);
        }
        else
        {
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position,site,moveSpeed * Time.deltaTime);
        }
    }
}
