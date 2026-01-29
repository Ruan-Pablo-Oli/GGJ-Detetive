using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Banco de Dados")]

    public List<SuspectData> todosOsSuspeitos;

    [Header("Informação do jogo atual")]
    public SuspectData assassinoDaVez;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        RealizarSorteio();
    }

    public void RealizarSorteio()
    {
        if(todosOsSuspeitos.Count == 0)
        {
            Debug.Log("Nenhum suspeito na lista do gameManager!");
            return;
        }

        foreach(var suspeito in todosOsSuspeitos)
        {
            suspeito.ehOAssassinho = false;
        }

        int indexSorteado = Random.Range(0,todosOsSuspeitos.Count);
    
        assassinoDaVez = todosOsSuspeitos[indexSorteado];
        assassinoDaVez.ehOAssassinho = true;
    }
}
