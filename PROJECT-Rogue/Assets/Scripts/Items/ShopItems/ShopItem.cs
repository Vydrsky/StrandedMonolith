using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    private GameObject item;
    private int price;
    [SerializeField] private Text priceDisplay;
    void Start()
    {
        List<GameObject> itemList = Level.GetAllItems();
        AssignGoods(itemList[Random.Range(0,itemList.Count)],15);
    }

    private void AssignGoods(GameObject _item,int _price)
    {
        price = _price;
        item=Instantiate(_item, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
        item.transform.parent = this.transform;
        item.layer = 3;
        priceDisplay.GetComponent<Text>().text = _price.ToString();
    }

    public int Buy()
    {
        return price;
    }

    public void Sell()
    {
        item.layer = 2;
        item.transform.parent = null;
        Destroy(gameObject);
        Destroy(this);
    }
}
