using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopControl : MonoBehaviour
{
    public GameObject shop;
    private float time;

    public ScrollRect scroll;
    public Transform grid;
    public GameObject baseItem;
    public Sprite[] allImageItems;

    public GameObject BuySystem;

    private void Start()
    {
        BuySystem.SetActive(false);
        allImageItems = Resources.LoadAll<Sprite>("Frutas");

    }

    public void ActiveShop(List<ItemsProperties> _items) 
    {
        for (int i = grid.childCount - 1; i >= 0; i--)
        {
            Destroy(grid.GetChild(i).gameObject);
        }

        for (int i = 0; i < _items.Count; i++)
        {
            
            GameObject newItem = Instantiate(baseItem, grid); 
            newItem.transform.Find("Name").GetComponent<Text>().text = _items[i].name[0];
            newItem.transform.Find("Price").GetComponent<Text>().text = _items[i].buyPrice + "$";
            newItem.transform.Find("Icon").GetComponent<Image>().sprite = allImageItems[_items[i].imageId];

            ItemsProperties currentItem = _items[i];
            newItem.GetComponent<Button>().onClick.AddListener(delegate { GetItem(currentItem); });
        }
    }
    public void GetItem(ItemsProperties _item)
    {
        BuySystem.SetActive(true);
        ItemsProperties currentItem = _item;
        BuySystem.transform.Find("Description").GetComponent<Text>().text = _item.description[0];
        Text amount = BuySystem.transform.Find("ItemAmount").GetComponent<Text>();
        Text price = BuySystem.transform.Find("FinalPrice").GetComponent<Text>();
        Slider slider = BuySystem.transform.Find("Amount").GetComponent<Slider>();
        int basePrice = _item.buyPrice;
        BuySystem.transform.Find("Icon").GetComponent<Image>().sprite = allImageItems[_item.imageId];

        slider.minValue = _item.minAmount;
        slider.maxValue = _item.maxAmount;
        slider.onValueChanged.AddListener(delegate { AmountChange(amount, price, slider, basePrice); });
        slider.value = _item.minAmount;
        BuySystem.transform.Find("FinalPrice").GetComponent<Text>().text = (currentItem.buyPrice * slider.value) + "$";
        BuySystem.transform.Find("Buy").GetComponent<Button>().onClick.RemoveAllListeners();
        BuySystem.transform.Find("Buy").GetComponent<Button>().onClick.AddListener(delegate { BuyItem(currentItem, slider); });
    }

    public void BuyItem(ItemsProperties _item, Slider _slider)
    {
        GetComponent<InventoryControl>().AddItem(ItemsManager.CloneItem(_item), (int)_slider.value);
        print("Has comprado "+ _slider.value + " "+_item.name[0]+" , por un precio de: " + _item.buyPrice * _slider.value + "$");
    }
    private void AmountChange(Text _amount, Text _priceAmount, Slider _slider, int _price)
    {
        _amount.text = _slider.value.ToString();
        _priceAmount.text = (_slider.value * _price).ToString() + "$";
    }
    public void CloseItem()
    {
        BuySystem.SetActive(false);
    }
}
