using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TaskData
{
    public string id;
    public string title;
    public string description;
    public DateTime date;
    public string time;
    public string group;
    public bool[] repeatDays; // 7 элементов для дней недели
    public int crystals;
    public bool allowPostpone;
    public bool isCompleted;

    // Конструктор для удобства
    public TaskData()
    {
        id = Guid.NewGuid().ToString();
        repeatDays = new bool[7];
        date = DateTime.Today;
    }
}

[System.Serializable]
public class TaskList
{
    public List<TaskData> tasks;

    public TaskList(List<TaskData> tasks)
    {
        this.tasks = tasks;
    }
}