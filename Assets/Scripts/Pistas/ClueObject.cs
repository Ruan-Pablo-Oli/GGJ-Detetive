using UnityEngine;

public class ClueObject : MonoBehaviour,IIteractable
{
    [Header("Configuração")]
    public ClueData dadosDaPista;

    public void Interact()
    {
        if(dadosDaPista == null)
        {
            Debug.Log("Objeto sem clue associado!");
            return;
        }

        if(UIManager.Instance != null)
        {
            UIManager.Instance.AdicionarPistaNaMascara(dadosDaPista.icone);
            UIManager.Instance.MostrarDialogo(dadosDaPista.descricao);
        }

        InvestigationManager.Instance.RegistrarPIsta(dadosDaPista);
        gameObject.SetActive(false);
    }
}
