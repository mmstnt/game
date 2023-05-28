using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterCub : MonoBehaviour
{
    private float timer;
    private SpriteRenderer sp;
    private Rigidbody2D rigid2D;
    private GameObject player;
    private Transform playerSite;
    // Start is called before the first frame update
    void Start()
    {
        sp = this.gameObject.GetComponent<SpriteRenderer>();
        rigid2D = this.gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        playerSite = player.transform;
        hp = hpMax;
        canInjuried = true;
        status = Status.warn;
    }

    // Update is called once per frame
    void Update()
    {
        timer = Time.deltaTime*1000;
        actionMode();
        cubDirection();
    }
    
    [Header("生命")]
    public int hpMax;
    public int hp;
    private bool canInjuried;
    public float repel;
    public GameObject playerAttackEffect;
    
    void OnTriggerEnter2D(Collider2D coll)
    {
        switch(coll.gameObject.tag)
        {
            case "PlayerAttack":
            if(canInjuried)
            {
                canInjuried = false;
                Instantiate(playerAttackEffect, this.transform.position,Quaternion.Euler(0,0,0));
                sp.color = new Color32(255,255,255,128);
                rigid2D.velocity = new Vector2(0, rigid2D.velocity.y);
                if(this.gameObject.transform.position.x > playerSite.position.x)
                {
                    rigid2D.AddForce(new Vector2(repel,0),ForceMode2D.Impulse);
                }
                if(this.gameObject.transform.position.x < playerSite.position.x)
                {
                    rigid2D.AddForce(new Vector2(-repel,0),ForceMode2D.Impulse);
                }
                hp--;
                if(hp < 1)
                {
                    status = Status.death;
                }
                else
                {
                    Invoke("Injuried",0.5f);
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

    
    private enum Direction {right,left};
    private Direction direction;

    private void cubDirection()
    {
        switch(direction)
        {
            case Direction.right:
                this.gameObject.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
            break;
            case Direction.left:
                this.gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            break;
        }
    }

	[Header("狀態")]  
    public Status status;
	public enum Status{idle,warn,move,death};

    private void actionMode(){
		switch(status){
            case Status.idle:
			break;
			case Status.warn:
				warn();
			break;
			case Status.move:
				move();
                status = Status.warn;
			break;
			case Status.death:
                InvokeRepeating("deathAnimation", 0, 0.05f);
                Invoke("death", 0.5f);
                status = Status.idle;
			break;
		}
	}

    [Header("移動")]
    public float moveSpeed;
    public float moveSpeedMax;
    public float warnRangeX;
    public float warnRangeY;

    private void move(){
        Vector3 v =  player.transform.position - this.gameObject.transform.position;
		if (v.x > 0){
			rigid2D.AddForce(new Vector2(moveSpeed*timer,0),ForceMode2D.Force);
            direction = Direction.right;
		}
        else if (v.x < 0){
            rigid2D.AddForce(new Vector2(-moveSpeed*timer,0),ForceMode2D.Force);
            direction = Direction.left;
		}
        if (rigid2D.velocity.x > moveSpeedMax) {
            rigid2D.velocity = new Vector2(moveSpeedMax, rigid2D.velocity.y);
        }
        else if (rigid2D.velocity.x < -moveSpeedMax) {
            rigid2D.velocity = new Vector2(-moveSpeedMax, rigid2D.velocity.y);
        }
	}

    private void warn()
    {
        Vector3 v = player.transform.position - this.gameObject.transform.position;
        if(v.y < warnRangeY && v.y > -warnRangeY)
        {
            if((direction == Direction.right && v.x < warnRangeX) || (direction == Direction.left && v.x > -warnRangeX))
            {
                status = Status.move;
            }
        }
    }

    private void deathAnimation()
    {
        sp.color -= new Color32(0,0,0,16);
    }

    public GameObject bloodStone;

    private void death()
    {
        Instantiate(bloodStone, this.transform.position,Quaternion.Euler(0,0,0));
        Destroy(this.gameObject);
    }
}
