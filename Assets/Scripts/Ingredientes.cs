using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredientes : MonoBehaviour  
{   
    public static List<string> ingredientes = new List<string>();

    private void OnTriggerEnter2D(Collider2D col){
        Debug.Log("entra");
        if(col.gameObject.tag == "Ingrediente"){
            ingredientes.Add(col.name);          
        }        
    }

    public List<string> listaIngredientes{
        get{return ingredientes;}
    }
}
