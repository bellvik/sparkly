using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AgeSelector : MonoBehaviour
{
    [Header("UI Elements")]
    public Button leftArrowButton;
    public Button rightArrowButton;
    public TextMeshProUGUI prevNumberText;
    public TextMeshProUGUI currentNumberText;
    public TextMeshProUGUI nextNumberText;

    [Header("Age Settings")]
    public int minAge = 3;
    public int maxAge = 12;
    private int currentAge = 5;

    public int SelectedAge => currentAge;

    void Start()
    {
        leftArrowButton.onClick.AddListener(DecreaseAge);
        rightArrowButton.onClick.AddListener(IncreaseAge);
        UpdateDisplay();
    }

    void IncreaseAge()
    {
        if (currentAge < maxAge)
        {
            currentAge++;
            UpdateDisplay();
        }
    }

    void DecreaseAge()
    {
        if (currentAge > minAge)
        {
            currentAge--;
            UpdateDisplay();
        }
    }

    void UpdateDisplay()
    {
        currentNumberText.text = currentAge.ToString();
        prevNumberText.text = (currentAge - 1 >= minAge) ? (currentAge - 1).ToString() : "";
        nextNumberText.text = (currentAge + 1 <= maxAge) ? (currentAge + 1).ToString() : "";

        // Визуальное выделение
        currentNumberText.color = Color.black;
        

        prevNumberText.color = new Color(0, 0, 0, 0.5f);
        

        nextNumberText.color = new Color(0, 0, 0, 0.5f);
        

        // Обновляем состояние кнопок
        leftArrowButton.interactable = currentAge > minAge;
        rightArrowButton.interactable = currentAge < maxAge;
    }
}