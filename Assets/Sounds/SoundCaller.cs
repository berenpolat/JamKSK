using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCaller : MonoBehaviour
{
    public string soundName;
  
    void Start()
    {
        SoundManager.Instance.PlayMusic(soundName , true);
    }

    
}
