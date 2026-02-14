using UnityEngine;
using TMPro;

public class GoldScript : MonoBehaviour
{

    float goldAmount = 10f;
    public TMP_Text goldText;

    public void ChangeGold(float goldChange)
    {
        goldAmount += goldChange;
        if (goldAmount < 0f)
        {
            goldAmount = 0f;
        }
        goldText.text = goldAmount.ToString();
    }

    public float GetGoldAmount(){
        return goldAmount;
    }
}
