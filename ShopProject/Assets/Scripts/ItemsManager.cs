using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    static public List<ItemsProperties> items = new List<ItemsProperties>() //static para acceder desde todas las escenas del juego 
    {

        new ItemsProperties()
        {
            debugName ="Banana",
            id =0,
            name =new string[]{"Banana", " Platano"},
            description = new string[]{"Sweet and easy to eat.", "Dulce y fácil de comer."},
            buyPrice =5, sellPrice=2, minAmount=1, maxAmount=99,
            imageId=0,
        },
        new ItemsProperties()
        {
            debugName ="Pear",
            id =1,
            name =new string[]{"Pear", "Pera"},
            description = new string[]{ "Sweet and juicy.", "Dulce y jugosa."},
            buyPrice =3, sellPrice=2, minAmount=1, maxAmount=99,
            imageId=1,
        },
        new ItemsProperties()
        {
            debugName ="Orange",
            id =2,
            name =new string[]{"Orange", "Naranja"},
            description = new string[]{ "It goes well when you are sick.", "Va bien cuando estás resfriado."},
            buyPrice =3, sellPrice=1, minAmount=1, maxAmount=99,
            imageId=2,
        },
        new ItemsProperties()
        {
            debugName ="Apple",
            id =3,
            name =new string[]{"Apple", "Manzana"},
            description = new string[]{ "Sweet and with a great texture.", "Dulce y con una gran textura."},
            buyPrice =5, sellPrice=3, minAmount=1, maxAmount=99,
            imageId=3,
        },
        new ItemsProperties()
        {
            debugName ="Lemon",
            id =4,
            name =new string[]{"Lemon", "Limón"},
            description = new string[]{ "The most acidic you can find.", "Los más ácidos que puedes encontrar."},
            buyPrice =2, sellPrice=1, minAmount=1, maxAmount=99,
            imageId=4,
        },
        new ItemsProperties()
        {
            debugName ="Strawberry",
            id =5,
            name =new string[]{"Strawberry", "Fresa"},
            description = new string[]{"Seasonal.", "De temporada."},
            buyPrice =5, sellPrice=1, minAmount=1, maxAmount=99,
            imageId=5,
        },
        new ItemsProperties()
        {
            debugName ="Screen",
            id =6,
            name =new string[]{"Screen", "Fresa"},
            description = new string[]{ "Maximum resolution guaranteed.", "De temporada."},
            buyPrice =100, sellPrice=1, minAmount=1, maxAmount=99,
            imageId=6,
        },
        new ItemsProperties()
        {
            debugName ="Bycicle",
            id =7,
            name =new string[]{"Bycicle", "Fresa"},
            description = new string[]{ "Light and fast.", "De temporada."},
            buyPrice =500, sellPrice=1, minAmount=1, maxAmount=99,
            imageId=7,
        },
        new ItemsProperties()
        {
            debugName ="GameBoy",
            id =8,
            name =new string[]{"GameBoy", "Fresa"},
            description = new string[]{ "A console classic.", "De temporada."},
            buyPrice =50, sellPrice=1, minAmount=1, maxAmount=99,
            imageId=8,
        },
        new ItemsProperties()
        {
            debugName ="Scissors",
            id =9,
            name =new string[]{ "Scissors", "Fresa"},
            description = new string[]{ "Sharp and shiny.", "De temporada."},
            buyPrice =30, sellPrice=1, minAmount=1, maxAmount=99,
            imageId=9,
        },

    };
    
    static public ItemsProperties CloneItem(ItemsProperties _item) //CLONADOR
    {
        ItemsProperties newItem = new ItemsProperties()
        {
            debugName=_item.debugName,
            id=_item.id,
            name=_item.name,
            description=_item.description,
            buyPrice=_item.buyPrice,
            sellPrice=_item.sellPrice,
            minAmount=_item.minAmount,
            maxAmount=_item.maxAmount,
            imageId = _item.imageId,
        };
        return newItem;
    }

    static public ItemsProperties GetItemById(int _id)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if(items[i].id==_id)
            {
                return CloneItem(items[i]);
            }
        }
        return null;
    }
}
[System.Serializable]
public class ItemsProperties
{
    public string debugName;
    public int id;   
    public int buyPrice; //ParaComprar
    public int sellPrice; //ParaVender
    public string[] name;
    public string[] description;
    public int maxAmount;
    public int minAmount;
    public int imageId;
}