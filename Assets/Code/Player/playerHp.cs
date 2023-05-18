using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        hp = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        hpChange();
    }

    public int hpNumber;
    public Animator hp;

    private void hpChange() {
        int n =(playerController.hp-((hpNumber-1)*2));
        n = n>0?n:0;
        switch(n)
        {
            case 0:
            hp.SetInteger("hp",0);
            break;
            case 1:
            hp.SetInteger("hp",1);
            break;
            default:
            hp.SetInteger("hp",2);
            break;
        }
	}
}
