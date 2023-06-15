using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class time : MonoBehaviour
{
    public TMP_Text textMesh;
    public static float spreedTime;
    // Start is called before the first frame update
    void Start()
    {
        if(mainCamera.endTime<spreedTime || spreedTime == 0)
        {
            spreedTime = mainCamera.endTime;
        }
        textMesh = this.gameObject.GetComponent<TMP_Text>();
        textMesh.text = "本次用時:"+mainCamera.endTime+"\n最佳時間:"+spreedTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
