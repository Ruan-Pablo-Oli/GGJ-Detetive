using UnityEngine;

[CreateAssetMenu(fileName = "NovaPista",menuName ="Detetive/Pista")]
public class ClueData : ScriptableObject
{
    [Header("Identificação")]
    public string nomeDaPista;

    [TextArea]
    public string descricao;

    [Header("Mecânica")]
    public Trait tracoRevelado;

    [Header("Visual")]
    public Sprite icone;
}
