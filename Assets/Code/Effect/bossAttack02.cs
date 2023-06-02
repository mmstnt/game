using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAttack02 : MonoBehaviour
{
    public GameObject player;
    public Transform playerSite;
    public float attackWait;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerSite = player.transform;
        Invoke("attackDirection",attackWait);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void attackDirection()
    {
        Vector3 dir = playerSite.position - this.gameObject.transform.position;
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        this.gameObject.transform.rotation = Quaternion.AngleAxis(angle,Vector3.forward);
    }
}
