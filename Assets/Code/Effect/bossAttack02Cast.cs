using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAttack02Cast : MonoBehaviour
{
    private GameObject player;
    private Transform playerSite;
    private int bossAttackAmount;
    public GameObject bossAttack02Effect;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerSite = player.transform;
        bossAttackAmount = boss.bossAttack02Amount;
        Invoke("bossAttack02",0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void bossAttack02()
    {
        Vector3 v = playerSite.position;
        v.x += UnityEngine.Random.Range(-8.0f,8.0f);
        v.y += UnityEngine.Random.Range(2.0f,8.0f);
        Instantiate(bossAttack02Effect,v,Quaternion.Euler(0,0,0));
        bossAttackAmount--;
        if(bossAttackAmount > 0)
        {
            Invoke("bossAttack02",0.5f);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
