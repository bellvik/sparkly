using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class WeekManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI currentDateText;
    public Transform weekPanel;

    [Header("Existing Day Buttons")]
    public Button mondayButton;
    public Button tuesdayButton;
    public Button wednesdayButton;
    public Button thursdayButton;
    public Button fridayButton;
    public Button saturdayButton;
    public Button sundayButton;

    [Header("Text References - Перетащите текстовые элементы")]
    public TextMeshProUGUI mondayDayName;
    public TextMeshProUGUI mondayDayNumber;
    public TextMeshProUGUI tuesdayDayName;
    public TextMeshProUGUI tuesdayDayNumber;
    public TextMeshProUGUI wednesdayDayName;
    public TextMeshProUGUI wednesdayDayNumber;
    public TextMeshProUGUI thursdayDayName;
    public TextMeshProUGUI thursdayDayNumber;
    public TextMeshProUGUI fridayDayName;
    public TextMeshProUGUI fridayDayNumber;
    public TextMeshProUGUI saturdayDayName;
    public TextMeshProUGUI saturdayDayNumber;
    public TextMeshProUGUI sundayDayName;
    public TextMeshProUGUI sundayDayNumber;

    [Header("Russian Language Settings")]
    public string[] dayNames = { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота", "Воскресенье" };
    public string[] shortDayNames = { "ПН", "ВТ", "СР", "ЧТ", "ПТ", "СБ", "ВС" };
    public string[] monthNames = { "января", "февраля", "марта", "апреля", "мая", "июня",
                                  "июля", "августа", "сентября", "октября", "ноября", "декабря" };

    private DateTime currentDate;
    private Button selectedDayButton;
    private Dictionary<Button, DateTime> buttonDates = new Dictionary<Button, DateTime>();
    private Dictionary<Button, TextMeshProUGUI[]> buttonTexts = new Dictionary<Button, TextMeshProUGUI[]>();
    private List<Button> dayButtons = new List<Button>();

    void Start()
    {
        currentDate = DateTime.Today;
        SetupButtonTextReferences();
        UpdateCurrentDateDisplay();
        InitializeExistingButtons();
        SelectToday();
    }

    void SetupButtonTextReferences()
    {
        // Собираем ссылки на текстовые элементы для каждой кнопки
        buttonTexts[mondayButton] = new TextMeshProUGUI[] { mondayDayName, mondayDayNumber };
        buttonTexts[tuesdayButton] = new TextMeshProUGUI[] { tuesdayDayName, tuesdayDayNumber };
        buttonTexts[wednesdayButton] = new TextMeshProUGUI[] { wednesdayDayName, wednesdayDayNumber };
        buttonTexts[thursdayButton] = new TextMeshProUGUI[] { thursdayDayName, thursdayDayNumber };
        buttonTexts[fridayButton] = new TextMeshProUGUI[] { fridayDayName, fridayDayNumber };
        buttonTexts[saturdayButton] = new TextMeshProUGUI[] { saturdayDayName, saturdayDayNumber };
        buttonTexts[sundayButton] = new TextMeshProUGUI[] { sundayDayName, sundayDayNumber };
    }

    void UpdateCurrentDateDisplay()
    {
        // Правильное преобразование дня недели для русского формата
        int russianDayOfWeek = ConvertToRussianDayOfWeek(currentDate.DayOfWeek);
        string dateString = $"{dayNames[russianDayOfWeek]}, {currentDate.Day} {monthNames[currentDate.Month - 1]}";
        currentDateText.text = dateString;
    }

    // Метод для преобразования DayOfWeek в русский формат (понедельник = 0)
    int ConvertToRussianDayOfWeek(DayOfWeek dayOfWeek)
    {
        switch (dayOfWeek)
        {
            case DayOfWeek.Monday: return 0;    // Понедельник
            case DayOfWeek.Tuesday: return 1;   // Вторник
            case DayOfWeek.Wednesday: return 2; // Среда
            case DayOfWeek.Thursday: return 3;  // Четверг
            case DayOfWeek.Friday: return 4;    // Пятница
            case DayOfWeek.Saturday: return 5;  // Суббота
            case DayOfWeek.Sunday: return 6;    // Воскресенье
            default: return 0;
        }
    }

    void InitializeExistingButtons()
    {
        // Собираем все кнопки в список
        dayButtons.Clear();
        buttonDates.Clear();

        if (mondayButton != null) dayButtons.Add(mondayButton);
        if (tuesdayButton != null) dayButtons.Add(tuesdayButton);
        if (wednesdayButton != null) dayButtons.Add(wednesdayButton);
        if (thursdayButton != null) dayButtons.Add(thursdayButton);
        if (fridayButton != null) dayButtons.Add(fridayButton);
        if (saturdayButton != null) dayButtons.Add(saturdayButton);
        if (sundayButton != null) dayButtons.Add(sundayButton);

        // Находим понедельник текущей недели
        DateTime monday = GetMondayOfWeek(currentDate);

        // Назначаем даты кнопкам и настраиваем их
        for (int i = 0; i < dayButtons.Count; i++)
        {
            DateTime dayDate = monday.AddDays(i);
            SetupDayButton(dayButtons[i], dayDate, i);
        }
    }

    DateTime GetMondayOfWeek(DateTime date)
    {
        int diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
        return date.AddDays(-diff).Date;
    }

    void SetupDayButton(Button button, DateTime date, int dayIndex)
    {
        // Сохраняем дату для кнопки
        buttonDates[button] = date;

        // Получаем текстовые элементы из словаря
        TextMeshProUGUI dayNameText = null;
        TextMeshProUGUI dayNumberText = null;

        if (buttonTexts.ContainsKey(button) && buttonTexts[button].Length >= 2)
        {
            dayNameText = buttonTexts[button][0];
            dayNumberText = buttonTexts[button][1];
        }

        // Если ссылки не установлены, пытаемся найти автоматически
        if (dayNameText == null || dayNumberText == null)
        {
            FindTextElementsAutomatically(button, ref dayNameText, ref dayNumberText);
        }

        // Обновляем тексты
        if (dayNameText != null)
        {
            dayNameText.text = shortDayNames[dayIndex];
            Debug.Log($"Установлен текст дня: {shortDayNames[dayIndex]} для кнопки {button.name}");
        }

        if (dayNumberText != null)
        {
            dayNumberText.text = date.Day.ToString();
            Debug.Log($"Установлено число: {date.Day} для кнопки {button.name}");
        }

        // Настраиваем обработчик клика
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => SelectDay(button));

        // Устанавливаем начальный цвет
        UpdateButtonVisual(button, false);
    }

    void FindTextElementsAutomatically(Button button, ref TextMeshProUGUI dayNameText, ref TextMeshProUGUI dayNumberText)
    {
        // Ищем все TextMeshPro элементы в кнопке
        TextMeshProUGUI[] texts = button.GetComponentsInChildren<TextMeshProUGUI>();

        if (texts.Length >= 2)
        {
            // Предполагаем, что первый текст - название дня, второй - число
            dayNameText = texts[0];
            dayNumberText = texts[1];
            Debug.Log($"Автоматически найдены текстовые элементы в {button.name}");
        }
        else if (texts.Length == 1)
        {
            // Если только один текстовый элемент, используем его для обоих
            dayNameText = texts[0];
            dayNumberText = texts[0];
            Debug.LogWarning($"В кнопке {button.name} только один текстовый элемент");
        }
        else
        {
            Debug.LogError($"В кнопке {button.name} не найдены текстовые элементы!");
        }
    }

    public void SelectDay(Button selectedButton)
    {
        // Снимаем выделение с предыдущей кнопки
        if (selectedDayButton != null)
        {
            UpdateButtonVisual(selectedDayButton, false);
        }

        // Выделяем новую кнопку
        selectedDayButton = selectedButton;
        UpdateButtonVisual(selectedButton, true);

        Debug.Log($"Выбран день: {buttonDates[selectedButton].ToString("dd.MM.yyyy")}");
    }

    void SelectToday()
    {
        foreach (var button in dayButtons)
        {
            if (buttonDates.ContainsKey(button) && buttonDates[button].Date == currentDate.Date)
            {
                SelectDay(button);
                break;
            }
        }
    }

    void UpdateButtonVisual(Button button, bool isSelected)
    {
        Image buttonImage = button.GetComponent<Image>();
        if (buttonImage == null) return;

        DateTime buttonDate = buttonDates[button];
        bool isToday = (buttonDate.Date == DateTime.Today.Date);

        if (isSelected && isToday)
        {
            buttonImage.color = new Color(0.1f, 0.6f, 0.2f); // Темно-зеленый для выделенного сегодня
        }
        else if (isSelected)
        {
            buttonImage.color = new Color(0.2f, 0.4f, 0.8f); // Синий для выделенного
        }
        else if (isToday)
        {
            buttonImage.color = new Color(0.2f, 0.8f, 0.3f); // Зеленый для сегодня
        }
        else
        {
            buttonImage.color = Color.white; // Белый для обычного
        }
    }

    // Методы для навигации по неделям
    public void NextWeek()
    {
        currentDate = currentDate.AddDays(7);
        UpdateCurrentDateDisplay();
        InitializeExistingButtons();
        SelectToday();
    }

    public void PreviousWeek()
    {
        currentDate = currentDate.AddDays(-7);
        UpdateCurrentDateDisplay();
        InitializeExistingButtons();
        SelectToday();
    }

    // Получить выбранную дату
    public DateTime GetSelectedDate()
    {
        return selectedDayButton != null && buttonDates.ContainsKey(selectedDayButton)
            ? buttonDates[selectedDayButton]
            : DateTime.Today;
    }
}