using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Libro : MonoBehaviour
{  
    public GameObject Lib;
    public bool btn = false;
    
    public void Pulsar(){       
        
        if(btn){            
            Lib.SetActive(false);
            btn = false;
        }else{            
            Lib.SetActive(true);
            btn = true;
        }

        
    }
}
