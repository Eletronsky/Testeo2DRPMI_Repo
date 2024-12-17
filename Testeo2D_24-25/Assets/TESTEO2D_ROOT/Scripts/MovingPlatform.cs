using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed; //velocidad de la plataforma
    [SerializeField] int startingpoint; //n�mero para determinar el index del punto de inicio del movimiento.
    [SerializeField] Transform[] points; //array de puntos de posici�n a los que las plataformas "perseguir�".
    int i; //Index que determina que n�mero de plataforma se persigue actualmente.


    // Start is called before the first frame update
    void Start()
    {
        //setear a posici�n inicial de la plataforma en uno de los puntos. 
        transform.position = points[startingpoint].position;
    }

    // Update is called once per frame
    void Update()
    {
        PlatformMove();
    }

    void PlatformMove()
    {
        //detector de si la plataforma ha llegado al destino, cambiando el destino. 
        if (Vector2.Distance(transform.position, points[i].position ) < 0.02f)
        {
            i++; //Aumenta a 1 el Index cambia de objetivo. 
            if (i == points.Length) i = 0;
        }

        //Movimiento: SIEMPRE DESPU�S DE LA DETENCCI�N.
        //Mueve la plataforma al punto del array que coincide con el valor de i. 
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }
}
