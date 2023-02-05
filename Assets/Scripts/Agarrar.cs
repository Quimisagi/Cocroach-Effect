using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agarrar : MonoBehaviour
{
    public float velocidad = 5;
    public Vector3 posicionObj;
    public static List<Agarrar> objSelecc = new List<Agarrar>();
    public bool seleccionado = false;
    public Vector3 inicial;
    public bool act = false;

    // Start is called before the first frame update
    void Awake(){
        inicial = this.transform.position; 
        inicial.z = this.transform.position.z; 
    }
    
    void Start()
    {
        objSelecc.Add(this);
        posicionObj = this.transform.position; 
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && seleccionado){
            posicionObj = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            posicionObj.z = this.transform.position.z;           

        }



        if(act){
            //transform.position = inicial * Time.deltaTime;
            //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime, speed);
            this.transform.position = Vector3.MoveTowards(this.transform.position, inicial, velocidad * Time.deltaTime);
        }
        else{
            this.transform.position = Vector3.MoveTowards(this.transform.position, posicionObj, velocidad * Time.deltaTime);
        }
            

    }

    void LateUpdate(){
        
        
    }

    private void OnMouseDown(){
        seleccionado = true;
        //this.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        foreach (Agarrar obj in objSelecc){
            if(obj != this){                
                obj.seleccionado = false;
                
                //obj.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col){
        act = true;
        
    }

    private void OnTriggerExit2D(Collider2D col){
        //act = false;
        
    }

    
}
