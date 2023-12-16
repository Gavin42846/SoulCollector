using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInfo : MonoBehaviour
{
    public int fishCount;
    // Start is called before the first frame update
    void Start()
    {
        fishCount = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FishAdder()
    {
        fishCount++;
    }
}
