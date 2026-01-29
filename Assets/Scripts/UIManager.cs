using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Configuração da máscara")]
    public Image[] slotsDaMascara;

    public Color corRevelada = Color.white;
    public Color corDoFlash = Color.purple;
    private int indiceMascara = 0;

    [Header("Configuração do diálogo")]
    public GameObject painelDialogo;
    public TMP_Text textoDialogo;
    public float tempoDeLeitura = 4.0f;

    private Coroutine corrotinaDoDialogo;


    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
        if(painelDialogo != null) painelDialogo.SetActive(false);
    }



    public void AdicionarPistaNaMascara(Sprite iconeDaPista)
    {
        if (iconeDaPista == null) return;
        if (indiceMascara >= slotsDaMascara.Length) return;

        Image slotAtual = slotsDaMascara[indiceMascara];
        
        // Em vez de mudar direto, chamamos a animação
        StartCoroutine(AnimarRevelacao(slotAtual, iconeDaPista));

        indiceMascara++;
    }


    IEnumerator AnimarRevelacao(Image slot, Sprite novoIcone)
    {
        float duration = 0.4f;
        Vector3 escalaOriginal = Vector3.one;
        Vector3 escalaGrande = Vector3.one * 1.3f;

        slot.transform.localScale = escalaGrande;
        slot.color = corDoFlash;

        yield return new WaitForSeconds(0.3f);
        
        slot.sprite = novoIcone;
        slot.type = Image.Type.Simple;
        slot.SetNativeSize();
        slot.color = corRevelada;

        float time = 0;

        while(time < duration)
        {
            slot.transform.localScale = Vector3.Lerp(escalaGrande,escalaOriginal,time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        slot.transform.localScale = escalaOriginal;

    }



    public void MostrarDialogo(string mensagem)
    {
        if(painelDialogo == null)return;
        painelDialogo.SetActive(true);
        textoDialogo.text = mensagem;

        if (corrotinaDoDialogo != null) 
        {
            StopCoroutine(corrotinaDoDialogo);
        }
        
        // Iniciamos o novo timer e guardamos a referência
        corrotinaDoDialogo = StartCoroutine(FecharDialogoAposTempo());
    }




    IEnumerator FecharDialogoAposTempo()
    {
        yield return new WaitForSeconds(tempoDeLeitura);
        painelDialogo.SetActive(false);
    }
}
