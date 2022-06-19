using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCursor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("clicked");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100000))
            {
                Debug.Log("hit something");
                var drinkTask = hit.collider.gameObject.GetComponent<DrinkTask>();
                if (drinkTask != null)
                {
                    Debug.Log("hit a DrinkTask");
                    drinkTask.Clicked();
                }
            }
        }
    }
}
