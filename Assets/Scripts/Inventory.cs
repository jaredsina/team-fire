using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int selectedSlot = 0;
    public string[] inventory = {"", ""};

    public GameObject slot1;
    public GameObject slot2;

    public Image image1;
    public Image image2;

    public Sprite selectedSprite;
    public Sprite unselectedSprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image1 = slot1.GetComponent<Image>();
        image2 = slot2.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            selectedSlot = 0;
            image1.sprite = selectedSprite;
            image2.sprite = unselectedSprite;
        }

        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            selectedSlot = 1;
            image2.sprite = selectedSprite;
            image1.sprite = unselectedSprite;
        }
    }
}
