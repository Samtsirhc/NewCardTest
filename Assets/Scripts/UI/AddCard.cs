using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddCard : MonoBehaviour
{
    public List<GameObject> gameObjects;
    public Dropdown dropdown;
    // Start is called before the first frame update
    void Start()
    {
        dropdown.ClearOptions();
        List<Dropdown.OptionData> listOptions = new List<Dropdown.OptionData>();
        foreach (var item in gameObjects)
        {
            listOptions.Add(new Dropdown.OptionData(item.GetComponent<MyCard>().cardName));
        }
        dropdown.AddOptions(listOptions);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Add()
    {
        foreach (var item in gameObjects)
        {
            if (item.ToString() == dropdown.options[dropdown.value].text)
            {
                DeckManager.Instance.AddCard(Instantiate(item, GameObject.Find("Canvas").transform));
            }
        }
    }
}
