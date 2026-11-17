using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
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

        playingBeat = false;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (isRecording)
        {
            RecordKeys();
        }
    }

    // Recording ===============================================================================
    [Serializable]
    private class Beat
    {
        public enum TrashType
        {
            Plastic,
            Paper,
            Aluminum
        }

        public TrashType type;
        public float time;

        public Beat(TrashType type, float time)
        {
            this.type = type;
            this.time = time;
        }
    }

    [Header("Song Data")]
    [SerializeField] List<Beat> beats;
    bool isRecording = false;
    float startTime;

    public void StartRecording()
    {
        beats = new List<Beat>();
        isRecording = true;
        startTime = Time.time;

        audioSource.Play();
    }


    private void RecordKeys()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            beats.Add(new Beat(Beat.TrashType.Paper, Time.time - startTime));
            Debug.Log("Key Recorded: " + Beat.TrashType.Paper);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            beats.Add(new Beat(Beat.TrashType.Plastic, Time.time - startTime));
            Debug.Log("Key Recorded: " + Beat.TrashType.Plastic);
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            beats.Add(new Beat(Beat.TrashType.Aluminum, Time.time - startTime));
            Debug.Log("Key Recorded: " + Beat.TrashType.Aluminum);
        }
    }


    // GAMEPLAY =================

    bool playingBeat;
    [SerializeField] AudioClip clip;
    [SerializeField] AudioSource audioSource;

    public void StartGame()
    {
        convoCount = 0;
        scoreCount = 0;
        playingBeat = true;
        StartCoroutine("LaunchCicle");
        // reproducir la musica
        // corutina de spawn de objetos
    }

    public void StopGame()
    {
        playingBeat = false;

        //TODO: mostar ending 
    }


    [Header("Trash Spawn")]
    [SerializeField] float travelTime;
    [SerializeField] Transform launchPoint;
    [SerializeField] Vector2 direction;
    [SerializeField] float launchForce;
    // [SerializeField] GameObject trashItemPrefab;

    [SerializeField] GameObject[] trashItemsPrefabs;

    public void LaunchTrashItem(GameObject trashItemPrefab)
    {
        travelTime = Time.time;
        GameObject item = Instantiate(trashItemPrefab, launchPoint.position, Quaternion.Euler(new Vector3(0, 0, -15)));
        Rigidbody2D rigidbody2D = item.GetComponent<Rigidbody2D>();
        rigidbody2D.AddForce(direction * launchForce, ForceMode2D.Impulse);
        // rigidbody2D.AddForceAtPosition(Vector2.right * 0.2f, Vector2.up);
        Destroy(item, 2f);
    }

    IEnumerator LaunchCicle()
    {
        while (playingBeat)
        {
            foreach (var b in beats)
            {
                yield return new WaitForSeconds(b.time);
                GameObject item = null;
                switch (b.type)
                {
                    case Beat.TrashType.Paper:
                        item = trashItemsPrefabs[0];
                        break;
                    case Beat.TrashType.Plastic:
                        item = trashItemsPrefabs[1];
                        break;
                    case Beat.TrashType.Aluminum:
                        item = trashItemsPrefabs[2];
                        break;
                }
                LaunchTrashItem(item);
            }
        }
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
