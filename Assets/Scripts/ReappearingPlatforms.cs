using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReappearingPlatforms : MonoBehaviour
{
    public float timeToTogglePlatform = .5f;
    public float currentTime = 0;
    public bool Enabled = true;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= timeToTogglePlatform)
        {
            currentTime = 0;
            TogglePlatform();
        }
    }

    void TogglePlatform()
    {
        Enabled = !Enabled;
        foreach(Transform child in gameObject.transform)
        {
            child.gameObject.SetActive(Enabled);
        }
    }
}
