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

    public void Init(bool flag, Item item, int level)
    {
        imgBG = gameObject.transform.GetChild(0).GetComponent<Image>();
        level_num = gameObject.transform.GetChild(4).GetComponent<Text>();

        down_imgBG = gameObject.transform.GetChild(5).GetComponent<Image>();
        down_text = gameObject.transform.GetChild(6).GetComponent<Text>();

        level_num.text = level.ToString();

        if (flag)
        {
            imgBG.sprite = item.bg_true;
            down_imgBG.sprite = item.down_icon_true;
        }
        else
        {
            imgBG.sprite = item.bg_false;
            down_imgBG.sprite = item.down_icon_false;
        }
    }
}
