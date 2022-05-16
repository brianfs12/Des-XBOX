using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpiritRange : MonoBehaviour
{
    public CircleCollider2D spiritRange;
    public List<GameObject> enemigosEnRango;
    public List<GameObject> enemigosAcomodados;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRadius();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy") || collision.transform.CompareTag("PossessionPoint"))
        {
            if (collision.GetComponent<EnemyBase>() || collision.GetComponent<EnemyPersecutor>() || collision.GetComponent<PossessionPoints>()) {
                if(!enemigosEnRango.Contains(collision.gameObject))
                {
                    EnemyPersecutor script = collision.gameObject.GetComponent<EnemyPersecutor>();
                    if (script)
                    {
                        if (script.poseible)
                        {
                            enemigosEnRango.Add(collision.gameObject);
                        }
                    }
                    else {
                        EnemyBase scriptt = collision.gameObject.GetComponent<EnemyBase>();
                        if(scriptt)
                        {
                            if (scriptt.poseible)
                            {
                                enemigosEnRango.Add(collision.gameObject);
                            }
                        }
                        else if(collision.GetComponent<PossessionPoints>())
                        {
                            PossessionPoints poss = collision.GetComponent<PossessionPoints>();
                            //PRUEBA poseer postes solo si has desbloqueado el color
                            if (poss.auraColor == PossessionPoints.AuraColor.Blue && GameManager.Instance.blue)
                            {
                                enemigosEnRango.Add(collision.gameObject);
                            }
                            else if (poss.auraColor == PossessionPoints.AuraColor.Green && GameManager.Instance.green)
                            {
                                enemigosEnRango.Add(collision.gameObject);
                            }
                            else if (poss.auraColor == PossessionPoints.AuraColor.Red && GameManager.Instance.red)
                            {
                                enemigosEnRango.Add(collision.gameObject);
                            }
                        }
                    }
                }            
            }
        }
        if (collision.transform.CompareTag("Boss1PossessionPoint")) {
            enemigosEnRango.Add(collision.gameObject);
        }
    }

    public void OrdenarEnemigos() {
        if (enemigosEnRango.Count > 1) {
            enemigosAcomodados = new List<GameObject>();
            for (int i = 0; i < enemigosEnRango.Count; i++) {
                if (i > 0)
                {
                    float distanciaEnemigo = Vector3.Distance(transform.position, enemigosEnRango[i].transform.position);
                    for (int j = 0; j < enemigosAcomodados.Count; j++)
                    {
                        float distanciaElementoLista = Vector3.Distance(transform.position, enemigosAcomodados[j].transform.position);
                        if (distanciaEnemigo < distanciaElementoLista)
                        {
                            if (!enemigosAcomodados.Contains(enemigosEnRango[i].gameObject))
                            {
                                enemigosAcomodados.Insert(j, enemigosEnRango[i].gameObject);
                            }
                        }
                    }
                    if (!enemigosAcomodados.Contains(enemigosEnRango[i].gameObject))
                    {
                        enemigosAcomodados.Add(enemigosEnRango[i].gameObject);
                    }
                }
                else {
                    enemigosAcomodados.Add(enemigosEnRango[i].gameObject);
                }
            }
            enemigosEnRango = enemigosAcomodados;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            material_manager script = null;
            script = collision.gameObject.GetComponentInChildren<material_manager>();
            if (script == null)
            {
                collision.gameObject.GetComponentInParent<material_manager>();
            }
            script.DesActivarMaterialEspiritu();
            enemigosEnRango.Remove(collision.gameObject);
        }
    }
    public void UpdateRadius()
    {
        if (spiritRange.radius != GameManager.Instance.playerStats.spiritRange) {
            spiritRange.radius = GameManager.Instance.playerStats.spiritRange;
        }
    }
}
