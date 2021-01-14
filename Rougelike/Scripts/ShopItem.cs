using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{

    public GameObject[] shopItems;
    public Sprite[] shopItemSprites;
    public int[] shopPrices;

    public Text price;
    public Image icon;

    private int selectedNumber;
    private GameObject selectedItem;
    private Sprite selectedImage;
    private int selectedPrice;

    // Start is called before the first frame update
    void Start()
    {
        selectedNumber=Random.Range(0, shopPrices.Length);
        selectedItem=shopItems[selectedNumber];
        selectedImage=shopItemSprites[selectedNumber];
        selectedPrice=shopPrices[selectedNumber];

        price.text = selectedPrice.ToString();
        icon.sprite = selectedImage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(PlayerControl.instance.currentCoins >= selectedPrice)
            {
            PlayerControl.instance.LoseCoin(selectedPrice);
            Instantiate(selectedItem, transform.position, transform.rotation);
            Destroy(gameObject);
            }
        }
    }
}
