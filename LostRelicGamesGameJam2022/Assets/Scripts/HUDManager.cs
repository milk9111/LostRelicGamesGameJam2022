using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public GameObject currentDrinkGameObject;
    public TextMeshProUGUI currentDrinkValueText;
    public TextMeshProUGUI currentDrinkTaskListText;
    public Button completeOrderButton;
    public GameObject buttonGroup;

    private Dictionary<string, Dictionary<string, bool>> _drinkTaskList;

    private void Start()
    {
        DisableCompleteOrderButton();
    }

    public void DisableCompleteOrderButton()
    {
        completeOrderButton.gameObject.SetActive(false);
    }

    public void EnableCompleteOrderButton()
    {
        completeOrderButton.gameObject.SetActive(true);
    }

    public void HideButtons()
    {
        buttonGroup.SetActive(false);
    }

    public void ShowButtons()
    {
        buttonGroup.SetActive(true);
    }

    public void ResetHUD()
    {
        currentDrinkGameObject.SetActive(false);
        currentDrinkValueText.text = "None";
        currentDrinkTaskListText.text = "";
        _drinkTaskList = null;
    }

    public void AssignCurrentDrink(Drink drink)
    {
        currentDrinkGameObject.SetActive(true);
        _drinkTaskList = new Dictionary<string, Dictionary<string, bool>>();

        currentDrinkValueText.text = $"{drink.drinkName}";
        foreach(var taskArray in drink.tasks)
        {
            var taskList = new Dictionary<string, bool>();
            foreach(var task in taskArray.tasks)
            {
                taskList.Add(task.name, false);
            }

            _drinkTaskList.Add(taskArray.header, taskList);
        }

        PrintTaskList();
    }

    public void CompleteTask(DrinkTask drinkTask)
    {
        _drinkTaskList[drinkTask.taskHeader][drinkTask.name] = true;
        PrintTaskList();
    }

    private void PrintTaskList()
    {
        var sb = new StringBuilder();
        foreach(var header in _drinkTaskList)
        {
            var i = 0;
            var allCompleted = true;
            var innerSb = new StringBuilder();
            foreach(var task in header.Value)
            {
                var taskStr = $"{i+1}. {task.Key}";
                if (task.Value)
                {
                    taskStr = $"<s>{taskStr}</s>";
                }

                innerSb.AppendLine($"<indent=1em>{taskStr}</indent>");
                i++;
                allCompleted = allCompleted && task.Value;
            }

            var headerStr = $"\u2022 {header.Key}";
            if (allCompleted)
            {
                headerStr = $"<s>{headerStr}</s>";
            }

            sb.AppendLine(headerStr);
            sb.Append(innerSb.ToString());
        }

        currentDrinkTaskListText.text = sb.ToString();
    }
}
