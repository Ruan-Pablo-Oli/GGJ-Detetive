using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    public static DontDestroy Instance;
    public static Vector3 proximaPosicao;
    
    public static bool existeTeleportePendente = false;

    void Awake()
    {

        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  

            SceneManager.sceneLoaded += AoCarregarCena;
        }
        else
        {
            Destroy(gameObject);
        }
         
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= AoCarregarCena;
    }

    void AoCarregarCena(Scene cena, LoadSceneMode modo)
    {
        if (existeTeleportePendente)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if(player != null)
            {
                player.transform.position = proximaPosicao;      
            }

            existeTeleportePendente = false;
        }
    }

}
