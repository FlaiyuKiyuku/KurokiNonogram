using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuchoose : MonoBehaviour
{

    public float Max = 1f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            changeMax(17.8f);
        }
    }

    public void changeMax(float x)
    {
        Max = x;
    }
}
