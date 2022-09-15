using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public List<int> itemsInShop;
    public bool sellAllItems;
    
    public List<ItemsProperties> shop;

    public ShopControl control;

    public RectTransform[] allButons;
    public RectTransform arrow;
    public int currentPage;
    public GameObject P0, P1, P2;


    void Start()
    {
        
        
        int day = System.DateTime.Today.Day; //saber el dia teimpo real
        string week = System.DateTime.Today.DayOfWeek.ToString(); //saber el dia de la semana a tiempo real
        if(day % 2 ==0) //Si el dia es par
        {
            for (int i = 0; i < ItemsManager.items.Count; i++)
            {
                shop.Add(ItemsManager.CloneItem(ItemsManager.items[i]));
            }
            //Tienda Completa
        }
        else
        {
            for (int i = 0; i < itemsInShop.Count; i++)
            {
                shop.Add(ItemsManager.GetItemById(itemsInShop[i]));
            }
            //Tienda personalizada
        }
        control.ActiveShop(shop);
    }
    

    void Update()
    {
        Vector2 finalPos = new Vector2(allButons[currentPage].anchoredPosition.x, -33);
        arrow.anchoredPosition = Vector2.Lerp(arrow.anchoredPosition, finalPos, 5 * Time.deltaTime);
    }

    private void OnMouseDown()
    {
        //control.ActiveShop(shop);
    }
    public void SetPage(int _page)
    {
        currentPage = _page;
        if(currentPage == 0)
        {
            P0.SetActive(true);
            P1.SetActive(false);
            P2.SetActive(false);
        }
        if (currentPage == 1)
        {
            P0.SetActive(false);
            P1.SetActive(true);
            P2.SetActive(false);
        }
        if (currentPage == 2)
        {
            P0.SetActive(false);
            P1.SetActive(false);
            P2.SetActive(true);
        }
    }
}
