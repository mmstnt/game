using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerEffect : MonoBehaviour
{
    public enum Direction {right,left,none};
    public Direction direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        effectDirection();
        move();
    }

    private void effectDirection()
    {
        switch(direction)
        {
            case Direction.right:
                this.gameObject.transform.rotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);
            break;
            case Direction.left:
                this.gameObject.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
            break;
            case Direction.none:
                this.gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            break;
        }
    }

    private void move()
    {
        if(Input.GetKey(KeyCode.D))
        {
            direction = Direction.right;
        }
        else if(Input.GetKey(KeyCode.A))
        {
            direction = Direction.left;
        }
        else
        {
            direction = Direction.none;
        }
    }
}
