using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectReferences : MonoBehaviour
{
    //Solo es una lista de objetos que podriamos querer referenciar a traves del gameObject del player
    //Por ejemplo, en vez de usar GameObject.Find en las esencias, simplemente accedemos a este componente para conseguir el objeto que se quiere referenciar.

    public GameObject guardEssenceGO;
    public GameManager GM;

    private void Start()
    {
        //transform.try
    }
}
