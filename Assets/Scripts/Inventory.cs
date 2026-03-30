using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject TestFood;
    public GameObject TestBanana;
    public GameObject TestGrape;
    public GameObject TestBagguette;
    public GameObject TestCarrot;
    public GameObject TestSalmon;
    public GameObject TestTortilla;
    public int selectedSlot = 0;
    public string[] inventory = { "", "" };
    public GameObject slot1;
    public GameObject slot2;
    public Image image1;
    public Image image2;
    public Sprite selectedSprite;
    public Sprite unselectedSprite;

    void Start()
    {
        image1 = slot1.GetComponent<Image>();
        image2 = slot2.GetComponent<Image>();
    }

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

    public void TestBananaWeapon()
    {
        BananaInventory banana = TestBanana.GetComponent<BananaInventory>();
        banana.BananaName();
    }

    public void TestGrapeWeapon()
    {
        GrapeInventory grape = TestGrape.GetComponent<GrapeInventory>();
        grape.GrapeName();
    }

    public void TestBagguetteWeapon()
    {
        BagguetteInventory bagguette = TestBagguette.GetComponent<BagguetteInventory>();
        bagguette.BagguetteName();
    }

    public void TestCarrotWeapon()
    {
        CarrotInventory carrot = TestCarrot.GetComponent<CarrotInventory>();
        carrot.CarrotName();
    }

    public void TestSalmonWeapon()
    {
        SalmonInventory salmon = TestSalmon.GetComponent<SalmonInventory>();
        salmon.SalmonName();
    }

    public void TestTortillaWeapon()
    {
        TortillaInventory tortilla = TestTortilla.GetComponent<TortillaInventory>();
        tortilla.TortillaName();
    }
}