using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkManager : MonoBehaviour
{
    public List<Drink> availableDrinks;

    private Drink _currentDrink;

    private DrinkTaskManager _drinkTaskManager;
    private HUDManager _hudManager;

    private void Awake()
    {
        _drinkTaskManager = FindObjectOfType<DrinkTaskManager>();
        _hudManager = FindObjectOfType<HUDManager>();
    }

    private void Start()
    {
        _drinkTaskManager.onFinishDrink += OnFinishDrink;
    }

    public void AssignCurrentDrink(string name)
    {
        _hudManager.HideButtons();

        Drink newDrink = null;
        foreach(var drink in availableDrinks)
        {
            if (drink.drinkName.ToLower() == name.ToLower())
            {
                newDrink = drink;
                break;
            }
        }

        if (newDrink == null)
        {
            Debug.LogError($"Could not find drink of name {name}");
            return;
        }

        _currentDrink = newDrink;
        _currentDrink.SelectDrink();

        _hudManager.AssignCurrentDrink(newDrink);
        _drinkTaskManager.AssignCurrentDrink(newDrink);
    }

    public void CompleteOrder()
    {
        _hudManager.DisableCompleteOrderButton();
        //_hudManager.ShowButtons();
        _hudManager.ResetHUD();
        _currentDrink.ResetTasks();
    }

    private void OnFinishDrink()
    {
        _hudManager.EnableCompleteOrderButton();
    }
}
