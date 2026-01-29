using UnityEngine;

[CreateAssetMenu(fileName ="NovoSuspeito",menuName ="Detetive/Suspeito")]
public class SuspectData : ScriptableObject
{
    
    [Header("Identidade")]
    public string nomeDoPersonagem;
    [TextArea]
    public Sprite fotoDoRosto;

    [Header("Segredos")]
    [TextArea] public string falaPadrao;

    [HideInInspector] public bool ehOAssassinho = false;
}
