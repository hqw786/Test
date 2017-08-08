using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PackItem : MonoBehaviour
{

    Text text;

    Image img;

    void Awake()
    {
        this.text = this.transform.Find("Text").GetComponent<Text>();
        this.img = this.transform.Find("GoodImg").GetComponent<Image>();
    }

    private PackModel model;

    public PackModel Model
    {
        get { return model; }
        set
        {
            model = value;
            if (model.GoodId != 0)
            {
                this.img.enabled = true;
                this.text.text = model.Count.ToString();
                this.img.sprite = Resources.Load<Sprite>(model.good.Src);
               
            }
            else
            {
                this.img.enabled = false;
                this.text.text = "0";
                this.img.sprite = null;
            }
	      
           
        }
    }



}
