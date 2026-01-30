using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    public GameObject prefabPistaGenerica;

    [Header("--- SISTEMA DE PISTAS (MEMÓRIA) ---")]
    public List<string> pistaColetadsa = new List<string>();


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


    public void RegistrarColeta(string idDaPista)
    {
        if (!pistaColetadsa.Contains(idDaPista))
        {
            pistaColetadsa.Add(idDaPista);
        }
    }

    public bool VerificarSeJaPegou(string idDaPista)
    {
        return pistaColetadsa.Contains(idDaPista);
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

        if(AccusationManager.Instance != null)
        {
            AccusationManager.Instance.nomeDoVerdadeiroAssassino = assassinoDaVez.nomeDoPersonagem;
        }

        EspalharPistasNaCena();
    }

    void EspalharPistasNaCena()
    {
        ClueSpawnPoint[] pontosPossiveis = FindObjectsByType<ClueSpawnPoint>(FindObjectsSortMode.None);

        if(pontosPossiveis.Length < assassinoDaVez.pistasDoCrime.Count)
        {
            Debug.Log("Não tem pontos de spawn o suficiente para todas as pistas");
            return;
        }


        for(int i = 0; i < pontosPossiveis.Length; i++)
        {
            ClueSpawnPoint temp = pontosPossiveis[i];
            int r = Random.Range(i,pontosPossiveis.Length);
            pontosPossiveis[i] = pontosPossiveis[r];
            pontosPossiveis[r] = temp;
        }


        for(int i = 0; i < assassinoDaVez.pistasDoCrime.Count; i++)
        {

            if (i >= pontosPossiveis.Length) break;

            ClueData dadosDaPista = assassinoDaVez.pistasDoCrime[i];
            ClueSpawnPoint local = pontosPossiveis[i];

            GameObject novaPistaObj = Instantiate(prefabPistaGenerica,local.transform.position,Quaternion.identity);
            ClueObject scriptPista = novaPistaObj.GetComponent<ClueObject>();
            SpriteRenderer spriteR = novaPistaObj.GetComponent<SpriteRenderer>();

            if(scriptPista != null)
            {
                scriptPista.dadosDaPista = dadosDaPista;
                scriptPista.gameObject.SetActive(true);
            }


            if(spriteR != null && dadosDaPista.icone != null)
            {
                spriteR.sprite = dadosDaPista.icone;
            }

        }

    }
}
