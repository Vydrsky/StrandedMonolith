using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveUI : MonoBehaviour
{
    [SerializeField] private ActiveItem item;
    [SerializeField] private Image img;
    [SerializeField] private Text text;

    public void SetCurrentActive(ActiveItem current)
    {
        item = current;
    }

    private void ChangeState()
    {
        if(item != null)
        {
            img.sprite = item.sprite;
            if (item.ItemCooldown == 0)
            {
                img.color = new Color(img.color.r, img.color.g, img.color.b, 1f);
                text.text = "";
            }
            else
            {
                img.color = new Color(img.color.r, img.color.g, img.color.b, 0.25f);
                text.text = ""+item.ItemCooldown;
            }
        }
    }

    private void Update()
    {
        ChangeState();
    }
}
