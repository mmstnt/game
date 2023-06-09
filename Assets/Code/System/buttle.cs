using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class buttle : MonoBehaviour
{
    public GameObject black;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gameStart()
    {
        Instantiate(black, this.transform.position,Quaternion.Euler(0,0,0));
        Invoke("goGameStart",1);
        
    }

    public void goGameStart()
    {
        SceneManager.LoadScene(4);
    }

    public void backToTitle()
    {
        Instantiate(black, this.transform.position,Quaternion.Euler(0,0,0));
        Invoke("goBackToTitle",1);
    }

    public void goBackToTitle()
    {
        SceneManager.LoadScene(0);
    }
}
