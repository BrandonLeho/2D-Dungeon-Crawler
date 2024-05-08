using UnityEngine;
using TMPro;

public class ClockManager : MonoBehaviour
{
    public int baseGoldCost = 100;
    public int goldCostIncrease = 50;
    public int currentGoldCost;
    public static int currentGoldCostStatic { get; private set; }

    [SerializeField] public TMP_Text costText;

    void Start()
    {
        currentGoldCostStatic = baseGoldCost;
        currentGoldCost = currentGoldCostStatic;
        UpdateAllCostTexts();
    }

    public void ResetClock()
    {
        currentGoldCostStatic += goldCostIncrease;
        currentGoldCost = currentGoldCostStatic;
        UpdateAllCostTexts();
    }

    static void UpdateAllCostTexts()
    {
        ClockManager[] allClocks = FindObjectsOfType<ClockManager>();
        foreach (ClockManager clock in allClocks)
        {
            clock.UpdateCostText(currentGoldCostStatic);
        }
    }

    void UpdateCostText(int amount)
    {
        if (costText != null)
            costText.text = "Cost: " + amount.ToString();
    }
}
