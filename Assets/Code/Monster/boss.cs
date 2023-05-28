using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour
{
    private float timer;
    public GameObject bossAppear;
    private SpriteRenderer sp;
    private GameObject player;
    private Transform playerSite;
    // Start is called before the first frame update
    void Start()
    {
        sp = this.gameObject.GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
        playerSite = player.transform;
        Instantiate(bossAppear,this.gameObject.transform.position,Quaternion.Euler(-90,0,0));
        hp = hpMax;
        canInjuried = true;
        status = Status.actionModeChange;
    }

    // Update is called once per frame
    void Update()
    {
        timer = Time.deltaTime*1000;
        actionMode();
    }

    [Header("生命")]
    public int hpMax;
    public int hp;
    private bool canInjuried;
    public GameObject playerAttackEffect;

    void OnTriggerEnter2D(Collider2D coll)
    {
        switch(coll.gameObject.tag)
        {
            case "PlayerAttack":
            if(canInjuried)
            {
                Vector3 v = (coll.gameObject.transform.position + this.gameObject.transform.position)/2;
                canInjuried = false;
                Instantiate(playerAttackEffect, v,Quaternion.Euler(0,0,0));
                sp.color = new Color32(255,255,255,160);
                hp--;
                if(hp < 1)
                {
                    status = Status.death;
                }
                else
                {
                    Invoke("Injuried",0.35f);
                }
            }
            break;
        }
    }

    private void Injuried()
    {
        canInjuried = true;
        sp.color = new Color32(255,255,255,255);
    }

    [Header("狀態")]  
    public Status status;
	public enum Status{idle,actionModeChange,bossAttack01,death};
    public int bossMode;
    public GameObject bossAttack01Effect;

    private void actionMode(){
		switch(status){
            case Status.idle:
			break;
            case Status.actionModeChange:
                Invoke("actionModeChange",UnityEngine.Random.Range(2.0f,3.0f));
                status = Status.idle;
            break;
            case Status.bossAttack01:
                bossAttack01();
            break;
			case Status.death:
                InvokeRepeating("deathAnimation", 0, 0.05f);
                Invoke("death", 0.5f);
                status = Status.idle;
			break;
		}
	}

    private void actionModeChange()
    {
        bossMode = UnityEngine.Random.Range(1,1);
        switch(bossMode)
        {
            case 1:
                status = Status.bossAttack01;
            break;
            case 2:

            break;
        }
    }

    private void bossAttack01()
    {
        status = Status.idle;
        Instantiate(bossAttack01Effect,this.gameObject.transform.position,Quaternion.Euler(-90,0,0));
        status = Status.actionModeChange;
    }

    private void deathAnimation()
    {
        sp.color -= new Color32(0,0,0,16);
    }


    private void death()
    {
        Destroy(this.gameObject);
    }
}
