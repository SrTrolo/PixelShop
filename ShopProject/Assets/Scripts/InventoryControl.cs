using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryControl : MonoBehaviour
{
    public Transform grid;
    public GameObject baseInventory;
    public List<InventoryProperties> inventory;
    public GameObject SellSystem;
    public int TotalPrice;

    public void Update()
    {
        if(inventory.Count <= 0)
        {
            SellSystem.transform.Find("BuyPrice").GetComponent<Text>().text = "0$";
        }
        FinalPrice();
        SellSystem.transform.Find("Buy").GetComponent<Button>().onClick.AddListener(delegate { BuyItem(); });

    }
    public void AddItem(ItemsProperties _item, int _amount)
    {
        if (HasItem(_item, _amount) == false)
        {
            inventory.Add(new InventoryProperties(_amount, _item));

        }
        
        PrintInventory();
    }
    public void PrintInventory()
    {
        for (int i = grid.childCount - 1; i >= 0; i--)
        {
            Destroy(grid.GetChild(i).gameObject);
        }

        for (int i = 0; i < inventory.Count; i++)
        {
            InventoryProperties tempItem = inventory[i];
            GameObject newItem = Instantiate(baseInventory, grid);
            newItem.transform.Find("Name").GetComponent<Text>().text = inventory[i].item.name[0];
            newItem.transform.Find("Price").GetComponent<Text>().text = "x " + inventory[i].amount.ToString();
            
            newItem.transform.Find("Icon").GetComponent<Image>().sprite = GetComponent<ShopControl>().allImageItems[inventory[i].item.imageId];

            newItem.GetComponent<Button>().onClick.AddListener(delegate { GetItem(tempItem); });

            //----Para Vender-----

        }
        FinalPrice();

    }

    public void GetItem(InventoryProperties _inventoryItem)
    {
        if (_inventoryItem != null)
        {
            InventoryProperties currentItem = _inventoryItem;
            Slider slider = SellSystem.transform.Find("Amount").GetComponent<Slider>();
            slider.maxValue = currentItem.amount;
            slider.minValue = 1;
            slider.value = slider.minValue;
            SellSystem.transform.Find("FinalPrice").GetComponent<Text>().text = (/*currentItem.item.buyPrice * slider.maxValue-*/  currentItem.item.buyPrice * slider.value) + "$";

            Text finalPrice = SellSystem.transform.Find("FinalPrice").GetComponent<Text>();
            Text itemAmount = SellSystem.transform.Find("ItemAmount").GetComponent<Text>();
            itemAmount.text = ("x") + slider.value.ToString();

            int buyPrice = currentItem.item.buyPrice;

            slider.onValueChanged.AddListener(delegate { UpdateSlider(slider, finalPrice, itemAmount, buyPrice); });

            SellSystem.transform.Find("Sell").GetComponent<Button>().interactable = true;
            SellSystem.transform.Find("Sell").GetComponent<Button>().onClick.RemoveAllListeners();
            SellSystem.transform.Find("Sell").GetComponent<Button>().onClick.AddListener(delegate { RemoveItem(currentItem.item, slider); });
        }
        else
        {
            Slider slider = SellSystem.transform.Find("Amount").GetComponent<Slider>();
            slider.maxValue = 1;
            slider.minValue = 1;
            SellSystem.transform.Find("FinalPrice").GetComponent<Text>().text = 0 + "$";
            SellSystem.transform.Find("Sell").GetComponent<Button>().onClick.RemoveAllListeners();
            SellSystem.transform.Find("Sell").GetComponent<Button>().interactable = false;
            SellSystem.transform.Find("FinalPrice").GetComponent<Text>().text = "0$";
            SellSystem.transform.Find("ItemAmount").GetComponent<Text>().text = "x0";
        }
    }

    private void UpdateSlider(Slider _slider, Text _priceText, Text _amountText, int _sellPrice)
    {
        _amountText.text = ("x") + _slider.value.ToString();
        _priceText.text = (_sellPrice * _slider.value) + "$";
    }
    public bool HasItem(ItemsProperties _item, int _amount)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].item.id == _item.id)
            {
                inventory[i].amount += _amount;
                return true;
                
            }
            
        }
        return false;
    }
    public void RemoveItem(ItemsProperties _item, Slider _amount)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].item.id == _item.id)
            {
                inventory[i].amount -= (int)_amount.value;
                if (inventory[i].amount <= 0)
                {
                    inventory.RemoveAt(i);
                }
                break;
            }
        }

        GetItem(null);
        PrintInventory();
    }
    public void FinalPrice()
    {
        TotalPrice = 0;
        for (int i = 0; i < inventory.Count; i++)
        {
            TotalPrice += inventory[i].item.buyPrice * inventory[i].amount;
        }
        SellSystem.transform.Find("BuyPrice").GetComponent<Text>().text = TotalPrice.ToString() + "$";
    }
    public void BuyItem()
    {
        inventory.Clear();
        for (int i = 0; i < grid.childCount; i++)
        {
            Destroy(grid.GetChild(i).gameObject);      
        }
        GetItem(null);
        SellSystem.transform.Find("BuyPrice").GetComponent<Text>().text = "0$";
        
    }
    
   
}

[System.Serializable]
public class InventoryProperties
{
    public int amount;
    public ItemsProperties item;
    public InventoryProperties(int _amount, ItemsProperties _item)
    {
        amount = _amount; item = _item;
    }
}