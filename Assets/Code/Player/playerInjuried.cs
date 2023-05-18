using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerInjuried : MonoBehaviour
{
    public Image image;
    public static Color injuried;
    // Start is called before the first frame update
    void Start()
    {
        image = this.gameObject.GetComponent<Image>();
        injuried = new Color(255,255,255,0);
    }

    // Update is called once per frame
    void Update()
    {
        image.GetComponent<Image>().color = injuried;
    }
}
