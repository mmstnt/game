using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectDisappear : MonoBehaviour
{
    public float disapperTime;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("end",disapperTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void end() {
        Destroy(this.gameObject);
    }
}
