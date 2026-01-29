using UnityEngine;

public class AccusationManager : MonoBehaviour
{
    public static AccusationManager Instance;

    [Header("Configuração Visual")]
    public GameObject painelDeAcusacao;

    [Header("O mistério")]
    public string nomeDoVerdadeiroAssassino;

    void Awake()
    {
        if(Instance == null) Instance = this;
    }


    public void AbrirPainel()
    {
        painelDeAcusacao.SetActive(true);
        Time.timeScale = 0;
    }

    public void FecharPainel()
    {
        painelDeAcusacao.SetActive(false);
        Time.timeScale = 1;
    }

    public void Acusar(string nomeDoSuspeito)
    {
        if(nomeDoSuspeito == nomeDoVerdadeiroAssassino)
        {
            Debug.Log("Vitória");
            FecharPainel();
        }
        else
        {
            Debug.Log("Derrota");
            FecharPainel();


            if(UIManager.Instance != null)
            {
                // Perde a máscara toda
            }
        }
    }
}
