using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharlesManager : MonoBehaviour
{
    public Patron charlesPatron;

    public List<int> charlesDays;

    private Queue<int> _charlesDays;

    private void Awake()
    {
        _charlesDays = new Queue<int>();
        foreach(var day in charlesDays)
        {
            _charlesDays.Enqueue(day);
        }
    }

    public int GetNextDay()
    {
        if (!_charlesDays.Any())
        {
            Debug.Log("not any charles days");
            return -1;
        }

        Debug.Log("some charles days");

        return _charlesDays.Dequeue() - 1;
    }
}
