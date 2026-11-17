using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager sharedInstance;

    int convoCount;
    int scoreCount;

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

    // Gameplay ===============================================================================

    public void RecordKeys()
    {

    }

    public void StartGame()
    {
        convoCount = 0;
        scoreCount = 0;

        // reproducir la musica
        // corutina de spawn de objetos
    }


    [Header("Trash Spawn")]
    [SerializeField] float travelTime;
    [SerializeField] Transform launchPoint;
    [SerializeField] Vector2 direction;
    [SerializeField] float launchForce;
    [SerializeField] GameObject trashItemPrefab;
    public void LaunchTrashItem()
    {
        travelTime = Time.time;
        GameObject item = Instantiate(trashItemPrefab, launchPoint.position, Quaternion.Euler(new Vector3(0, 0, -15)));
        Rigidbody2D rigidbody2D = item.GetComponent<Rigidbody2D>();
        rigidbody2D.AddForce(direction * launchForce, ForceMode2D.Impulse);
        // rigidbody2D.AddForceAtPosition(Vector2.right * 0.2f, Vector2.up);
        Destroy(item, 2f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(launchPoint.position, launchPoint.position + (Vector3)(direction * launchForce));
    }

    // UI ===============================================================================
    [Header("UI")]
    [SerializeField] TextMeshProUGUI convoTMP;
    [SerializeField] TextMeshProUGUI scoreTMP;

    private void SetConvo(int convoCount)
    {
        convoTMP.text = "Convo: " + convoCount;
    }

    private void SetScore(int score)
    {
        scoreTMP.text = "Puntaje: " + score;
    }


    // Game Flow ===============================================================================
    [Space]
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject InGameMenu;
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject EndingMenu;
    public void PauseGame()
    {
        PauseMenu.SetActive(true);
        InGameMenu.SetActive(false);

        MainMenu.SetActive(false);

        // TODO: DETENER la cancion y desactivar controles
    }

    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        InGameMenu.SetActive(true);

        MainMenu.SetActive(false);

        // TODO: Continuar
    }

    public void ReturnMainMenu()
    {
        PauseMenu.SetActive(false);
        InGameMenu.SetActive(false);

        MainMenu.SetActive(true);

        // TODO: Reiniciar
    }
}
