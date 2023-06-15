using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interfaceMusic : MonoBehaviour
{
    public AudioSource audios;
    public AudioClip[] audiosClip;
    // Start is called before the first frame update
    void Start()
    {
        audios = this.gameObject.GetComponent<AudioSource>();
        audios.clip = audiosClip[1];
        audios.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
