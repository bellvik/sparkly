using UnityEngine;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.U2D.Aseprite;

public class TaskUI : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI crystalsText;
    public Toggle completionToggle;
    public Button editButton;

    private TaskData taskData;
    private System.Action<TaskData> onTaskUpdated;
    private System.Action<TaskData> onTaskDeleted;

    void Start()
    {
        completionToggle.onValueChanged.AddListener(OnCompletionChanged);
        editButton.onClick.AddListener(OnEditClicked);
        
    }
    public void Initialize(TaskData task, System.Action<TaskData> updatedCallback, System.Action<TaskData> deletedCallback)
    {
        taskData = task;
        onTaskUpdated = updatedCallback;
        onTaskDeleted = deletedCallback;

        titleText.text = task.title;
        timeText.text = task.time;
        crystalsText.text = $"{task.crystals} кристаллов";
        completionToggle.isOn = task.isCompleted;

        UpdateVisualState();
    }

    void OnCompletionChanged(bool isCompleted)
    {
        taskData.isCompleted = isCompleted;
        UpdateVisualState();
        onTaskUpdated?.Invoke(taskData);
    }

    void OnEditClicked()
    {
        // TODO: Реализовать редактирование задачи
        Debug.Log($"Редактирование задачи: {taskData.title}");
    }

    

    void UpdateVisualState()
    {
        if (taskData.isCompleted)
        {
            titleText.color = Color.gray;
            // Можно добавить зачёркивание
        }
        else
        {
            titleText.color = Color.black;
        }
    }
}