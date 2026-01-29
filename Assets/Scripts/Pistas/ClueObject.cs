using UnityEngine;

public class ClueObject : MonoBehaviour,IIteractable
{
    [Header("Configuração")]
    public ClueData dadosDaPista;


    void Start()
    {
        if(dadosDaPista == null) return;

        if (GameManager.Instance.VerificarSeJaPegou(dadosDaPista.name))
        {
            gameObject.SetActive(false);
        }
    }

    public void Interact()
    {
        if(dadosDaPista == null)
        {
            Debug.Log("Objeto sem clue associado!");
            return;
        }

        if(GameManager.Instance != null)
        {
            GameManager.Instance.RegistrarColeta(dadosDaPista.nomeDaPista);
        }


        if(UIManager.Instance != null)
        {
            UIManager.Instance.AdicionarPistaNaMascara(dadosDaPista.icone);
            UIManager.Instance.MostrarDialogo(dadosDaPista.descricao);
        }

        if(InvestigationManager.Instance != null)
        {
            InvestigationManager.Instance.RegistrarPIsta(dadosDaPista);
        }
        gameObject.SetActive(false);
    }
}
