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
	public enum Status{idle,actionModeChange,attack01,attack02,attack03,move,death};
    public int bossMode;
    public GameObject bossAttack01Effect;
    public float bossAttack01CD;
    public GameObject bossAttack02Cast;
    public float bossAttack02CD;
    public int bossAttack02AmountMin;
    public int bossAttack02AmountMax;
    public static int bossAttack02Amount;
    public GameObject bossAttack03Cast;
    public float bossAttack03CD;
    public int bossAttack03AmountMin;
    public int bossAttack03AmountMax;
    public static int bossAttack03Amount;
    public GameObject bossMoveEffect;
    public float bossMoveCD;
    private int bossMoveTime;


    private void actionMode(){
		switch(status){
            case Status.idle:
			break;
            case Status.actionModeChange:
                Invoke("actionModeChange",UnityEngine.Random.Range(2.0f,3.0f));
                status = Status.idle;
            break;
            case Status.attack01:
                Invoke("bossAttack01",0.1f);
                status = Status.idle;
            break;
            case Status.attack02:
                bossAttack02Amount = UnityEngine.Random.Range(bossAttack02AmountMin,(bossAttack02AmountMax+1));
                Invoke("bossAttack02",0.1f);
                status = Status.idle;
            break;
            case Status.attack03:
                bossAttack03Amount = UnityEngine.Random.Range(bossAttack03AmountMin,(bossAttack03AmountMax+1));
                Invoke("bossAttack03",0.1f);
                status = Status.idle;
            break;
            case Status.move:
                Invoke("bossMove",0.1f);
                status = Status.idle;
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
        bossMode = UnityEngine.Random.Range(1,5);
        switch(bossMode)
        {
            case 1:
                status = Status.attack01;
            break;
            case 2:
                status = Status.attack02;
            break;
            case 3:
                status = Status.attack03;
            break;
            case 4:
                status = Status.move;
            break;
        }
    }

    private void actionModePrepare()
    {
        status = Status.actionModeChange;
    }

    private void bossAttack01()
    {
        Instantiate(bossAttack01Effect,this.gameObject.transform.position,Quaternion.Euler(-90,0,0));
        Invoke("actionModePrepare",bossAttack01CD);
    }

    private void bossAttack02()
    {
        Instantiate(bossAttack02Cast,this.gameObject.transform.position,Quaternion.Euler(0,0,0));
        Invoke("actionModePrepare",bossAttack02CD);
    }

    private void bossAttack03()
    {
        Instantiate(bossAttack03Cast,this.gameObject.transform.position,Quaternion.Euler(0,0,0));
        Invoke("actionModePrepare",bossAttack03CD);
    }

    private void bossMove()
    {
        canInjuried = false;
        Instantiate(bossMoveEffect,this.gameObject.transform.position,Quaternion.Euler(0,0,0));
        bossMoveTime = 10;
        sp.color = new Color32(255,255,255,255);
        Invoke("bossMoveDisappear",0.1f);
    }

    private void bossMoveDisappear()
    {
        if(bossMoveTime > 0)
        {
            bossMoveTime--;
            sp.color -= new Color32(0,0,0,25);
            Invoke("bossMoveDisappear",0.1f);
        }
        else
        {
            Vector3 v = this.gameObject.transform.position;
            v.x = UnityEngine.Random.Range(-10.0f,23.0f);
            Instantiate(bossMoveEffect,v,Quaternion.Euler(0,0,0));
            sp.color = new Color32(255,255,255,5);
            Invoke("bossMoveAppear",0.1f);
            this.gameObject.transform.position = v;
            canInjuried = true;
        }
    }

    private void bossMoveAppear()
    {
        if(bossMoveTime < 10)
        {
            bossMoveTime++;
            sp.color += new Color32(0,0,0,25);
            Invoke("bossMoveAppear",0.1f);
        }
        else
        {
            Invoke("actionModePrepare",bossMoveCD);
        }
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
