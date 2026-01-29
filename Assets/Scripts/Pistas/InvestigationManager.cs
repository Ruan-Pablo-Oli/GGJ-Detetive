using System.Collections.Generic;
using UnityEngine;

public class InvestigationManager : MonoBehaviour
{
    public static InvestigationManager Instance;

    [Header("Progresso")]
    public List<ClueData> pistaColetadas = new List<ClueData>();


    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegistrarPIsta(ClueData pista)
    {
        if(pistaColetadas.Contains(pista)) return;
        
        
        pistaColetadas.Add(pista);
        Debug.Log($"[MANAGER] Pista Arquivada: {pista.nomeDaPista} | Revelou: {pista.tracoRevelado}");

    }
}
