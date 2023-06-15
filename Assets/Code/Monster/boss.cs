using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class boss : MonoBehaviour
{
    private float timer;
    public GameObject bossAppear;
    private SpriteRenderer sp;
    private GameObject player;
    private Transform playerSite;
    public GameObject black;
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
        bossViolent = 0;
        bossViolent01 = true;
        bossViolent02 = true;
        callAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer = Time.deltaTime*1000;
        actionMode();
        violent();
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
    public GameObject bossViolentEffect;
    private int bossViolent;
    private bool bossViolent01;
    private bool bossViolent02;
    public GameObject callEffect;
    private int callAmount;
    public GameObject bloodStone;
    public GameObject bossDeathEffect;

    private void violent()
    {
        if(hp < hpMax * 0.3f)
        {
            bossViolent = 2;
            if(bossViolent02)
            {
                Vector3 v = this.gameObject.transform.position;
                Instantiate(bossViolentEffect,v,Quaternion.Euler(0,0,0));
                v.y += 4;
                Instantiate(bloodStone,v,Quaternion.Euler(0,0,0));
                callAmount += bossViolent * 2;
                Invoke("call",0.1f);
                bossViolent02 = false;
            }
        }
        else if (hp < hpMax * 0.6f)
        {
            bossViolent = 1;
            if(bossViolent01)
            {
                Vector3 v = this.gameObject.transform.position;
                Instantiate(bossViolentEffect,v,Quaternion.Euler(0,0,0));
                v.y += 4;
                Instantiate(bloodStone,v,Quaternion.Euler(0,0,0));
                callAmount += bossViolent * 2;
                Invoke("call",0.1f);
                bossViolent01 = false;
            }
        }
        else
        {
            bossViolent = 0;
        }
    }

    private void call()
    {
        if(callAmount > 0)
        {
            Instantiate(callEffect,this.gameObject.transform.position,Quaternion.Euler(0,0,0));
            callAmount--;
            Invoke("call",0.5f);
        }
    }

    private void actionMode(){
		switch(status){
            case Status.idle:
			break;
            case Status.actionModeChange:
                Invoke("actionModeChange",UnityEngine.Random.Range(2.0f-(bossViolent*0.5f),3.0f-(bossViolent*0.5f)));
                status = Status.idle;
            break;
            case Status.attack01:
                Invoke("bossAttack01",0.1f);
                status = Status.idle;
            break;
            case Status.attack02:
                bossAttack02Amount = UnityEngine.Random.Range(bossAttack02AmountMin+bossViolent,(bossAttack02AmountMax+1)+bossViolent);
                Invoke("bossAttack02",0.1f);
                status = Status.idle;
            break;
            case Status.attack03:
                bossAttack03Amount = UnityEngine.Random.Range(bossAttack03AmountMin+bossViolent,(bossAttack03AmountMax+1)+bossViolent);
                Invoke("bossAttack03",0.1f);
                status = Status.idle;
            break;
            case Status.move:
                Invoke("bossMove",0.1f);
                status = Status.idle;
            break;
			case Status.death:
                Instantiate(bossDeathEffect,this.gameObject.transform.position,Quaternion.Euler(0,0,0));
                InvokeRepeating("deathAnimation", 0.5f, 0.05f);
                Invoke("death", 1.5f);
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
            callAmount += bossViolent;
            Invoke("call",0.1f);
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
        Instantiate(black, this.gameObject.transform.position,Quaternion.Euler(0,0,0));
        mainCamera.end = true;
        Destroy(this.gameObject);
    }
}
