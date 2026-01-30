using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NovoSuspeito",menuName ="Detetive/Suspeito")]
public class SuspectData : ScriptableObject
{
    
    [Header("Identidade")]
    public string nomeDoPersonagem;

    public Sprite fotoDoRosto;
    

    [Header("Segredos")]
    [TextArea] public string falaPadrao;
    [TextArea] public string falaHabito;
    [TextArea] public string falaSobre;

    public List<ClueData> pistasDoCrime;

    [HideInInspector] public bool ehOAssassinho = false;




} 
