using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("end",0.32f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void end() {
        Destroy(this.gameObject);
    }
}
