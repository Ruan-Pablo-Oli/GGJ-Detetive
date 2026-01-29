using UnityEngine;

public class NPCController : MonoBehaviour,IIteractable
{
    [Header("Identidade")]
    public SuspectData meuPerfil;

    public void Interact()
    {
        Debug.Log("Falando com: " + meuPerfil.nomeDoPersonagem);

        if (meuPerfil.ehOAssassinho)
        {
            Debug.Log("Assassino");
        }
        else
        {
            Debug.Log("Inocente");
        }

        if (UIManager.Instance != null)
        {
            // Manda o perfil inteiro (foto, nome e fala) para a tela
            UIManager.Instance.MostrarDialogoNPC(meuPerfil);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(UIManager.Instance != null)
            {
                UIManager.Instance.FecharDialogo();
            }
        }
    }
}
