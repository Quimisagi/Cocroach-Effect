using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;

public class Nativo : MonoBehaviour
{
    public Ingredientes control;
    public TextMeshProUGUI ing1, ing2, ing3;
    public static List<string> gusto = new List<string>();
    public bool gameOn = true;  
    public GameObject canva; 

    void Start(){
        gusto.Add("lavanda"); 
        gusto.Add("jengibre"); 
        gusto.Add("te");


    }

    void Update(){
         if(control.listaIngredientes.Count == 3 && gameOn){
            Comparar(); 
            gameOn = false;           
        }
    }

    public void Comparar(){    
            var aciertos = control.listaIngredientes.Intersect(gusto).ToList();
            canva.SetActive(true);
            if(aciertos.Count == 3){
                ing1.text = aciertos[0];            
                ing2.text = aciertos[1];
                ing3.text = aciertos[2];
            } else if(aciertos.Count == 2){
                ing1.text = aciertos[0];            
                ing2.text = aciertos[1];
            } else if(aciertos.Count == 1){
                ing1.text = aciertos[0];
            }
                
    } 
} 

