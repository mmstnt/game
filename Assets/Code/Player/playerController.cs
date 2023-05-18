using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    private Rigidbody2D rigid2D;
    private enum Status {idle,move,atk};
    private Status status;
    public float timer;
    public GameObject black;
    public static bool cutScene;
    public static Vector3 cutSceneSite;

    // Start is called before the first frame update
    void Start()
    {
        rigid2D = this.gameObject.GetComponent<Rigidbody2D>();
        foot = this.gameObject.GetComponent<BoxCollider2D>();
        hpMax = 6;
        hp = hpMax;
        canInjuried = true;
        attackPreparation = true;
        attackMove = true;
        sprint = true;
        sprintCD = true;
        sprintTime = true;
        survive = true;
        cutScene = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer = Time.deltaTime*1000;
        if(cutScene)
        {
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position,cutSceneSite,moveSpeed * Time.deltaTime);
            if(this.gameObject.transform.position == cutSceneSite)
            {
                cutScene = false;
            }
        }
        else
        {
            playerMove();
            playerJump();
            platerAttack();
        } 
        playerDirection();
        playerHp();
        checkGround();
    }

    [Header("移動")]
    public float moveSpeed;
    public float moveSpeedMax;
    private bool sprint;
    private bool sprintCD;
    private bool sprintTime;
    public float sprintSpeed;
    public GameObject sprintEffect;

    private void playerMove()
    {
        if(Input.GetKey(KeyCode.D) && attackMove)
        {
            rigid2D.AddForce(new Vector2(moveSpeed*timer,0),ForceMode2D.Force);
            direction = Direction.right;
        }
        else if(Input.GetKey(KeyCode.A) && attackMove)
        {
            rigid2D.AddForce(new Vector2(-moveSpeed*timer,0),ForceMode2D.Force);
            direction = Direction.left;
        }
        if(Input.GetKeyDown(KeyCode.L) && attackMove && sprintCD && sprint)
        {
            sprint = false;
            sprintCD = false;
            sprintTime = false;
            switch(direction)
            {
                case Direction.right:
                    rigid2D.AddForce(new Vector2(sprintSpeed*timer,0),ForceMode2D.Impulse);
                    Instantiate(sprintEffect,this.transform.position,Quaternion.Euler(0,-90,0),this.transform);
                break;
                case Direction.left:
                    rigid2D.AddForce(new Vector2(-sprintSpeed*timer,0),ForceMode2D.Impulse);
                    Instantiate(sprintEffect,this.transform.position,Quaternion.Euler(0,90,0),this.transform);
                break;
            }
            Invoke("sprintTimeEnd",0.1f);
            Invoke("sprintCoolDown",0.25f);
        }
        if (rigid2D.velocity.x > moveSpeedMax && sprintTime)
        {
            rigid2D.velocity = new Vector2(moveSpeedMax, rigid2D.velocity.y);
        }
        else if (rigid2D.velocity.x < -moveSpeedMax && sprintTime)
        {
            rigid2D.velocity = new Vector2(-moveSpeedMax, rigid2D.velocity.y);
        }
    }

    public void sprintCoolDown()
    {
        sprintCD = true;
    }

    public void sprintTimeEnd()
    {
        sprintTime = true;
    }

    [Header("跳躍")]
    public float jumpSpeed;
    private BoxCollider2D foot;
    private bool isground;
    private bool doubleJump;

    private void playerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (isground || doubleJump))
        {
            Vector2 Jumpvel = new Vector2(0, jumpSpeed);
            rigid2D.velocity = Vector2.up * Jumpvel;
            if(!isground)
            {
                doubleJump = false;
            }
            sprint = true;
        }
    }

    void checkGround()
    {
        isground = foot.IsTouchingLayers(LayerMask.GetMask("Ground"));
        if(isground)
        {
            doubleJump = true;
            sprint = true;
        }
    }

    private enum Direction {right,left};
    private Direction direction;

    private void playerDirection()
    {
        switch(direction)
        {
            case Direction.right:
                this.gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            break;
            case Direction.left:
                this.gameObject.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
            break;
        }
    }

    
    
    [Header("生命")]
    private bool canInjuried;
    private float injuriedCD;
    private bool survive;
    public static int hp;
    public static int hpMax;
    public float repel;

    public void playerHp()
    {
        if(hp <= 0 && survive)
        {
            hp = 0;
            survive = false;
            Instantiate(black, this.transform.position,Quaternion.Euler(0,0,0));
            Invoke("gameGG",1);
        }
        if(hp > hpMax)
        {
            hp = hpMax;
        }
    }

    public void gameGG()
    {
        SceneManager.LoadScene(2);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        switch(coll.gameObject.tag)
        {
            case "Monster":
            if(canInjuried)
            {
                hp--;
                canInjuried = false;
                playerInjuried.injuried = new Color(255,255,255,0.9f);
                injuriedCD = 1;
                Invoke("Injuried",0.05f);
            }
            Transform c = coll.gameObject.transform;
            if(this.gameObject.transform.position.x > c.position.x)
            {
                rigid2D.AddForce(new Vector2(repel*timer,0),ForceMode2D.Impulse);
            }
            if(this.gameObject.transform.position.x < c.position.x)
            {
                rigid2D.AddForce(new Vector2(-repel*timer,0),ForceMode2D.Impulse);
            }
            break;
        }
    }

    private void Injuried()
    {
        if(injuriedCD <= 0)
        {
            canInjuried = true;
        }
        else
        {
            playerInjuried.injuried -= new Color(0,0,0,0.09f);
            injuriedCD -= 0.1f;
            Invoke("Injuried",0.1f);
        }
    }

    [Header("攻擊")]
    public bool attackPreparation;
    public bool attackMove;
    public GameObject playerAtk;
    public float attackDistance;
    public float attackCD;


    public void platerAttack()
    {
        if (Input.GetKeyDown(KeyCode.J) && attackPreparation)
        {
            rigid2D.velocity = new Vector2(0, rigid2D.velocity.y);
            Vector3 attackSite = this.gameObject.transform.position;
            float attackDirection;
            if(Input.GetKey(KeyCode.W))
            {
                attackSite.y += attackDistance;
                attackDirection = 45.0f;
            }
            else if(Input.GetKey(KeyCode.S))
            {
                attackSite.y -= attackDistance;
                attackDirection = -45.0f;
            }
            else
            {
                attackDirection = 0;
            }
            switch(direction)
            {
                case Direction.right:
                    attackSite.x += attackDistance;
                    Instantiate(playerAtk, attackSite,Quaternion.Euler(0,0,attackDirection));
                break;
                case Direction.left:
                    attackSite.x -= attackDistance;
                    Instantiate(playerAtk, attackSite,Quaternion.Euler(0,180.0f,attackDirection));
                break;
            }
            attackPreparation = false;
            attackMove = false;
            Invoke("attackCoolDown",attackCD);
            Invoke("attackMoveCookDown",(attackCD/4));
        }
    }

    public void attackCoolDown()
    {
        attackPreparation = true;
    }

    public void attackMoveCookDown()
    {
        attackMove = true;
    }
}
