using UnityEngine;

public class NPCController : MonoBehaviour,IIteractable
{
    [Header("Identidade")]
    public SuspectData meuPerfil;
    private int indiceDaConversa = 0;

    public void Interact()
    {
        if(meuPerfil == null) return;

        string textoParaFalar = "";


        switch (indiceDaConversa)
        {
            case 0:{
                textoParaFalar = meuPerfil.falaPadrao;
                break;
            }
            case 1:{
                textoParaFalar = meuPerfil.falaHabito;
                break;
            }    
            case 2:{
                textoParaFalar = meuPerfil.falaSobre;
                break;
            }
        }


        if (UIManager.Instance != null)
        {
            // Manda o perfil inteiro (foto, nome e fala) para a tela
            UIManager.Instance.MostrarDialogoNPC(meuPerfil,textoParaFalar);
        }

        indiceDaConversa++;

        if(indiceDaConversa > 2)
        {
            indiceDaConversa = 0;
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
