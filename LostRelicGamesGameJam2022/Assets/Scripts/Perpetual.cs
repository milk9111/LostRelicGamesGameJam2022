using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perpetual : MonoBehaviour
{
    public static Perpetual i;

    public Texture2D hoverCursor;

    // Start is called before the first frame update
    void Awake()
    {
        if (i == null)
        {
            i = this;
            DontDestroyOnLoad(this);
        } else
        {
            Destroy(this.gameObject);
            return;
        }
    }
}
