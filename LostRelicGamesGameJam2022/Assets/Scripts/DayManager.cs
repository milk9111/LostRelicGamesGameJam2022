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
    public bool onlyUseCharles;

    private CharlesManager _charlesManager;
    public DayTransition _dayTransition;

    public Queue<Patron> _currentDayPatrons;
    public int _currentDayPatronCount;

    private int _daysSinceStart;
    private int _nextCharlesDay;
    private bool _started;

    // Start is called before the first frame update
    void Awake()
    {
        _daysSinceStart = 0;
    }

    private void Start()
    {
        _charlesManager = FindObjectOfType<CharlesManager>();
        _nextCharlesDay = _charlesManager.GetNextDay();

        _dayTransition.gameObject.SetActive(false);
    }

    public int GetNextCharlesDay()
    {
        return _nextCharlesDay;
    }

    public void StartTransition(Action callback)
    {
        _dayTransition.gameObject.SetActive(true);
        _dayTransition.Play(GetCurrentDate(), CurrentDate().AddDays(-1).ToString("MM/dd/yyyy"), () =>
        {
            _dayTransition.gameObject.SetActive(false);
            StartDay();
            callback();
        });
    }

    public void StartDay()
    {
        foreach (var patron in patrons)
        {
            patron.gameObject.SetActive(false);
        }

        _charlesManager.charlesPatron.gameObject.SetActive(false);

        var numberOfPatronsToday = Random.Range(3, 6);
        var currentDayPatrons = new List<Patron>();
        var _currentCount = 0;
        while (_currentCount < numberOfPatronsToday)
        {
            var remainingPatrons = new List<Patron>();
            foreach (var patron in patrons)
            {
                var found = false;
                foreach (var currentPatron in currentDayPatrons)
                {
                    if (currentPatron == patron)
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    remainingPatrons.Add(patron);
                }
            }
            currentDayPatrons.Add(remainingPatrons.ToList()[Mathf.FloorToInt(Random.value * 10) % remainingPatrons.Count()]);
            _currentCount++;
        }

        _currentDayPatrons = new Queue<Patron>();
        if (!onlyUseCharles)
        {
            foreach (var patron in currentDayPatrons)
            {
                _currentDayPatrons.Enqueue(patron);
            }
        }

        if (_daysSinceStart == _nextCharlesDay)
        {
            Debug.LogError("Adding charles");
            if (_started)
            {
                _nextCharlesDay = _charlesManager.GetNextDay();
            } else
            {
                _started = true;
            }
            
            _currentDayPatrons.Enqueue(_charlesManager.charlesPatron);
        }

        _currentDayPatronCount = _currentDayPatrons.Count();
    }

    public Patron GetNextPatron()
    {
        if (!_currentDayPatrons.Any())
        {
            return null;
        }

        var patron = _currentDayPatrons.Dequeue();
        patron.gameObject.SetActive(true);

        return patron;
    }

    public void FinishDay()
    {
        _daysSinceStart++;
    }

    private DateTime CurrentDate()
    {
        return DateTime.ParseExact(startDate, "yyyy-MM-dd", CultureInfo.InvariantCulture).AddDays(_daysSinceStart);
    }

    public string GetCurrentDate()
    {
        return DateTime.ParseExact(startDate, "yyyy-MM-dd", CultureInfo.InvariantCulture).AddDays(_daysSinceStart).ToString("MM/dd/yyyy");
    }
}
