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
            if(item.ItemCooldown == 0)
            {
                img.color = Color.green;
                text.text = "";
            }
            else
            {
                img.color = Color.red;
                text.text = ""+item.ItemCooldown;
            }
        }
    }

    private void Update()
    {
        ChangeState();
    }
}
