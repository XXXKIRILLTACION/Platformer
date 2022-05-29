using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private Shop.DataPlayer dataPlayer = new Shop.DataPlayer();
    [HideInInspector] public string nameItem;
    [HideInInspector] public int priceItem;

    public GameObject[] allItem;
    public class DataPlayer
    {
        public int money;
        public List<string>buyItem = new List<string>();
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("SaveGame"))
        {
            LoadGame();
        }
        else
        {
            dataPlayer.money = 500;
            SaveGame();
            LoadGame();
        }
        
    }

    private void SaveGame()
    {
        
        PlayerPrefs.SetString("SaveGame", JsonUtility.ToJson(dataPlayer));
    }

    private void LoadGame()
    {
        dataPlayer = JsonUtility.FromJson<DataPlayer>(PlayerPrefs.GetString("SaveGame"));

        for (int i = 0; i < dataPlayer.buyItem.Count; i++)
        {
            for (int j = 0; j < allItem.Length; j++)
            {
                if(allItem[j].GetComponent<Item>().nameItem == dataPlayer.buyItem[i])
                {
                    allItem[j].GetComponent<Item>().TextItem.text = "Куплено";
                    allItem[j].GetComponent<Item>().isBuy = true;
                }
            }
        }
    }
    public void BuyItem()
    {
        if(dataPlayer.money >= priceItem)
        {
            dataPlayer.buyItem.Add(nameItem);
            dataPlayer.money = dataPlayer.money - priceItem;

            SaveGame();
            LoadGame();
        }
    }
}
