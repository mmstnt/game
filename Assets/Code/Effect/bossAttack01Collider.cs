using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAttack01Collider : MonoBehaviour
{
    public BoxCollider2D box;
    public Vector2 size;
    public Vector2 offset;
    public float damagaTime;
    // Start is called before the first frame update
    void Start()
    {
        box = this.gameObject.GetComponent<BoxCollider2D>();
        box.size = new Vector2(0,0);
        box.offset = new Vector2(0,0);
        Invoke("colliderAppear",damagaTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void colliderAppear()
    {
        box.size = size;
        box.offset = offset;
    }
}
