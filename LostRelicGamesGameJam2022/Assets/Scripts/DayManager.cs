using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class DayManager : MonoBehaviour
{
    public string startDate;
    public List<Patron> patrons;

    private CharlesManager _charlesManager;

    private Queue<Patron> _currentDayPatrons;

    private int _daysSinceStart;
    private int _nextCharlesDay;

    // Start is called before the first frame update
    void Awake()
    {
        _daysSinceStart = 0;
    }

    private void Start()
    {
        _charlesManager = FindObjectOfType<CharlesManager>();
        _nextCharlesDay = _charlesManager.GetNextDay();
    }

    public void StartDay()
    {
        foreach (var patron in patrons)
        {
            patron.gameObject.SetActive(false);
        }

        var numberOfPatronsToday = Random.Range(3, 6);
        _currentDayPatrons = new Queue<Patron>();
        while(_currentDayPatrons.Count < numberOfPatronsToday)
        {
            foreach(var patron in patrons.Except(_currentDayPatrons))
            {
                if (Random.value < 0.5)
                {
                    _currentDayPatrons.Enqueue(patron);
                    break;
                }
            }
        }

        if (_daysSinceStart == _nextCharlesDay)
        {
            _nextCharlesDay = _charlesManager.GetNextDay();
            _currentDayPatrons.Enqueue(_charlesManager.charlesPatron);
        }
    }

    public Patron GetNextPatron()
    {
        if (!_currentDayPatrons.Any())
        {
            return null;
        }

        return _currentDayPatrons.Dequeue();
    }

    public void FinishDay()
    {
        _daysSinceStart++;
    }

    public string GetCurrentDate()
    {
        return DateTime.ParseExact(startDate, "yyyy-MM-dd", CultureInfo.InvariantCulture).AddDays(_daysSinceStart).ToString("MM/dd/yyyy");
    }
}
