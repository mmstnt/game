using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAttackCollider : MonoBehaviour
{
    public BoxCollider2D box;
    public Vector2 startOffset;
    public Vector2 offset;
    public Vector2 size;
    public float damagaTime;
    // Start is called before the first frame update
    void Start()
    {
        box = this.gameObject.GetComponent<BoxCollider2D>();
        box.size = new Vector2(0,0);
        box.offset = startOffset;
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
