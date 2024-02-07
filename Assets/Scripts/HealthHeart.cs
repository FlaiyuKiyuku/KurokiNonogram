using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHeart : MonoBehaviour
{
    public Sprite fullheart, emptyheart;
    Image heartImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        heartImage = GetComponent<Image>();
    }

    public void SetHeartImage(HeartStatus status)
    {
        switch (status)
        {
            case HeartStatus.Empty:
                heartImage.sprite = emptyheart;
                break;
            case HeartStatus.Full:
                heartImage.sprite = fullheart;
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum HeartStatus
{
    Empty = 0,
    Full = 1
}
