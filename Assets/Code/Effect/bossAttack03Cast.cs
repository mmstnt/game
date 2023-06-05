using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAttack03Cast : MonoBehaviour
{
    private GameObject player;
    private Transform playerSite;
    private GameObject bossGameObject;
    private Transform bossSite;
    private int bossAttackAmount;
    public GameObject bossAttack03Effect;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerSite = player.transform;
        bossGameObject = GameObject.Find("boss(Clone)");
        bossSite = bossGameObject.transform;
        bossAttackAmount = boss.bossAttack03Amount;
        Invoke("bossAttack03",0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void bossAttack03()
    {
        Vector3 v = bossSite.position;
        v.x = playerSite.position.x;
        Instantiate(bossAttack03Effect,v,Quaternion.Euler(0,0,0));
        bossAttackAmount--;
        if(bossAttackAmount > 0)
        {
            Invoke("bossAttack03",0.5f);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
