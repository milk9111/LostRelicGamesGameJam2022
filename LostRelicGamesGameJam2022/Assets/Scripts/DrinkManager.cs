using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DrinkManager : MonoBehaviour
{
    public List<Drink> availableDrinks;

    private Drink _currentDrink;

    private DrinkTaskManager _drinkTaskManager;
    private HUDManager _hudManager;
    private GameManager _gameManager;

    private void Awake()
    {
        _drinkTaskManager = FindObjectOfType<DrinkTaskManager>();
        _hudManager = FindObjectOfType<HUDManager>();
        _gameManager = FindObjectOfType<GameManager>();
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
            newDrink = availableDrinks.First();
        }

        _currentDrink = newDrink;
        _currentDrink.SelectDrink();

        _hudManager.AssignCurrentDrink(newDrink);
        _drinkTaskManager.AssignCurrentDrink(newDrink);
    }

    public void CompleteOrder()
    {
        _hudManager.DisableCompleteOrderButton();
        _hudManager.ResetHUD();
        _currentDrink.ResetTasks();
        _gameManager.DoneTalkingToPatron();
        _drinkTaskManager.DeactivateAllTasks();
    }

    private void OnFinishDrink()
    {
        _hudManager.EnableCompleteOrderButton();
    }
}
