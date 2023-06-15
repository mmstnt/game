using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainCamera : MonoBehaviour
{
    public enum Status {idle,follow,move,back,boss};
    public static Status status;
    public GameObject player;
    public static Vector3 site;
    public float moveSpeed;
    public AudioSource audios;
    public AudioClip[] audiosClip;
    public static float endTime;
    public static bool end;
    // Start is called before the first frame update
    void Start()
    {
        end = false;
        endTime = 0;
        player = GameObject.Find("Player");
        audios = this.gameObject.GetComponent<AudioSource>();
        audios.clip = audiosClip[0];
        audios.Play();
        status = Status.follow;
    }

    // Update is called once per frame
    void Update()
    {
        endTime += Time.deltaTime;
        if(end)
        {
            Invoke("cut",1.0f);
            end = false;
        }
        switch(status)
        {
            case Status.idle:
            break;
            case Status.follow:
                Vector3 v = player.transform.position;
                this.gameObject.transform.position = new Vector3(v.x,v.y,-10);
			break;
			case Status.move:
                this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position,site,moveSpeed * Time.deltaTime);
                if(this.gameObject.transform.position == site)
                {
                    Invoke("back",1.5f);
                    status = Status.idle;
                }
            break;
			case Status.back:
                Vector3 v2 = new Vector3(player.transform.position.x,this.gameObject.transform.position.y,-10);
				this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position,v2,moveSpeed * Time.deltaTime);
				if(this.gameObject.transform.position == v2)
                {
                    status = Status.boss;
                }
            break;
            case Status.boss:
                Vector3 v3 = new Vector3(player.transform.position.x,this.gameObject.transform.position.y,-10);
                this.gameObject.transform.position = v3;
			break;
        }
    }

    public void back()
    {
        status = Status.back;
    }

    private void cut()
    {
        SceneManager.LoadScene(3);
    }
}
