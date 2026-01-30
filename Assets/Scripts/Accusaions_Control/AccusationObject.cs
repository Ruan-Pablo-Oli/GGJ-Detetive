using UnityEngine;

public class AccusationObject : MonoBehaviour,IIteractable
{

    public void Interact()
    {
        Debug.Log("Telefone tocando...Abrindo painel de acusação!");
        if(AccusationManager.Instance != null)
        {
            AccusationManager.Instance.AbrirPainel();
        }
        else
        {
            Debug.Log("ERRO. Não foi possível abrir o painel!");
        }
    }

}
