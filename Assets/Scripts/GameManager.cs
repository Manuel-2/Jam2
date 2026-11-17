using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager sharedInstance;

    void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else
        {
            Debug.LogWarning("El Game Manager esta Reepetido !!!!");
            Destroy(this.gameObject);
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    // Game Flow ==================================
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject InGameMenu;
    [SerializeField] GameObject MainMenu;
    void PauseGame()
    {
        PauseMenu.SetActive(true);
        InGameMenu.SetActive(false);

        MainMenu.SetActive(false);

        // TODO: DETENER la cancion y desactivar controles
    }

    void ResumeGame()
    {
        PauseMenu.SetActive(false);
        InGameMenu.SetActive(true);

        MainMenu.SetActive(false);

        // TODO: Continuar
    }

    void ReturnMainMenu()
    {
        PauseMenu.SetActive(false);
        InGameMenu.SetActive(false);

        MainMenu.SetActive(true);

        // TODO: Reiniciar

    }
}
