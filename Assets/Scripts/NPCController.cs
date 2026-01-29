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
    }
}
