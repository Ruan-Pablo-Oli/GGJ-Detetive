using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraBoundsManager : MonoBehaviour
{
    private CinemachineConfiner2D confiner;

    void Awake()
    {
        confiner = GetComponent<CinemachineConfiner2D>();
    }

    void OnEnable()
    {
        
        SceneManager.sceneLoaded += AoCarregarCena; 
    }


    void Osable()
    {
        SceneManager.sceneLoaded -= AoCarregarCena;      
    }

    void AoCarregarCena(Scene cena,LoadSceneMode modo)
    {
        StartCoroutine(ProcurarLimiteComAtraso(cena.name));

    }

    IEnumerator ProcurarLimiteComAtraso(string nomeDaCena)
    {
        // 1. Espera 1 frame (dá tempo da Unity organizar a nova cena)
        yield return null; 

        // 2. Tenta achar o objeto pela Tag
        GameObject limite = GameObject.FindGameObjectWithTag("Confiner");

        if (limite != null)
        {
            Collider2D novoShape = limite.GetComponent<Collider2D>();
            
            if (novoShape != null)
            {
                // Limpa o antigo para garantir
                confiner.BoundingShape2D = null; 
                
                // Aplica o novo
                confiner.BoundingShape2D = novoShape;
                
                // Tenta invalidar o cache (se esse comando não existir na sua versão, pode apagar)
                confiner.InvalidateBoundingShapeCache();

            }
 
        }

    }
}
