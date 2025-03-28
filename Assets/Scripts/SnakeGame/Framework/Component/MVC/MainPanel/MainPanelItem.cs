using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanelItem : MonoBehaviour
{
    public Image imgBG;
    public Text level_num;
    public Image down_imgBG;
    public Text down_text;

    public void Init(ItemData item, int money)
    {
        imgBG = gameObject.transform.GetChild(0).GetComponent<Image>();
        level_num = gameObject.transform.GetChild(4).GetComponent<Text>();

        down_imgBG = gameObject.transform.GetChild(5).GetComponent<Image>();
        down_text = gameObject.transform.GetChild(6).GetComponent<Text>();

        level_num.text = item.level.ToString();

        if (item.sum < money)
        {
            imgBG.sprite = item.item.bg_true;
            down_imgBG.sprite = item.item.down_icon_true;
            down_text.text = item.sum.ToString();
        }
        else
        {
            imgBG.sprite = item.item.bg_false;
            down_imgBG.sprite = item.item.down_icon_false;
            down_text.text = "FREE";
        }
    }
}
