using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fish_reset : MonoBehaviour
{

    public GameObject fishFlock;

    private void Awake()
    {
        fishFlock.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        fishFlock.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
