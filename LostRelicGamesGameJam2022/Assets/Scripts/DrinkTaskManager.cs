using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DrinkTaskManager : MonoBehaviour
{
    [SerializeField]
    private Drink _currentDrink;

    private HashSet<AvailableTask> _currentDrinkTasks;

    private HUDManager _hudManager;

    public Action onFinishDrink;

    private void Awake()
    {
        _hudManager = FindObjectOfType<HUDManager>();
    }

    void Start()
    {
        if (_currentDrink != null)
        {
            AssignCurrentDrink(_currentDrink);
        }
    }

    public void OnTaskComplete(DrinkTask drinkTask)
    {
        AvailableTask task = null;
        for(var i = 0; i < _currentDrinkTasks.Count; i++)
        {
            task = _currentDrinkTasks.ToList()[i];
            if (task.task == drinkTask)
            {
                break;
            }
        }

        if (task == null)
        {
            Debug.LogError("Task was null");
            return;
        }

        _hudManager.CompleteTask(drinkTask);

        task.task.SetUsable(false);
        _currentDrinkTasks.Remove(task);

        var nextTask = _currentDrink.GetNextTask(task.taskIndex);
        if (nextTask == null)
        {
            if (!_currentDrinkTasks.Any())
            {
                onFinishDrink();
            }

            return;
        }

        nextTask.task.SetUsable(true);
        nextTask.task.SetOnComplete(OnTaskComplete);
        _currentDrinkTasks.Add(nextTask);
    }

    public void AssignCurrentDrink(Drink drink)
    {
        _currentDrinkTasks = new HashSet<AvailableTask>();
        _currentDrink = drink;
        foreach(var task in _currentDrink.GetAvailableTasks())
        {
            task.task.SetUsable(true);
            task.task.SetOnComplete(OnTaskComplete);
            _currentDrinkTasks.Add(task);
        }
    }
}
