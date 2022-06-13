using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink : MonoBehaviour
{
    public string drinkName;
    public List<DrinkTaskArray> tasks;

    private List<Queue<DrinkTask>> _queuedTasks;

    public void SelectDrink()
    {
        _queuedTasks = new List<Queue<DrinkTask>>();
        foreach (var taskList in tasks)
        {
            var queue = new Queue<DrinkTask>();
            foreach (var task in taskList.tasks)
            {
                task.SetActive(true);
                task.taskHeader = taskList.header;
                queue.Enqueue(task);
            }

            _queuedTasks.Add(queue);
        }
    }

    public List<AvailableTask> GetAvailableTasks()
    {
        var availableTasks = new List<AvailableTask>();
        for(var i = 0; i < _queuedTasks.Count; i++)
        {
            availableTasks.Add(new AvailableTask
            {
                taskIndex = i,
                task = _queuedTasks[i].Dequeue()
            });
        }

        return availableTasks;
    }

    public AvailableTask GetNextTask(int taskIndex)
    {
        if (_queuedTasks[taskIndex].Count == 0)
        {
            return null;
        }

        return new AvailableTask
        {
            taskIndex = taskIndex,
            task = _queuedTasks[taskIndex].Dequeue()
        };
    }

    public void ResetTasks()
    {
        foreach(var taskArray in tasks)
        {
            foreach(var task in taskArray.tasks)
            {
                task.SetActive(false);
                task.SetUsable(false);
                task.ResetTask();
            }
        }
    }
}

public class AvailableTask
{
    public int taskIndex;
    public DrinkTask task;
    public string taskHeader;
}

[Serializable]
public class DrinkTaskArray
{
    public string header;
    public List<DrinkTask> tasks;
}
