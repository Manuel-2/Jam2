using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework.Constraints;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

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
        record = PlayerPrefs.GetInt("record", 0);
        recordTMP.text = "Record: " + record;
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

    // GAMEPLAY =================

    bool playingBeat;
    [SerializeField] AudioSource audioSource;

    [SerializeField] Animator canvasAnimator;
    public void StartGame()
    {
        if (playingBeat) return;
        convoCount = 0;
        scoreCount = 0;
        SetConvo(convoCount);
        SetScore(scoreCount);

        canvasAnimator.SetTrigger("InGame");

        playingBeat = true;
        audioSource.Play();
        StartCoroutine("LaunchCicle");
    }
    public void GameOver()
    {
        playingBeat = false;
        audioSource.Stop();
        if (scoreCount > record)
        {
            PlayerPrefs.SetInt("record", scoreCount);
        }
        //TODO: mostar ending 
    }

    public void Hit()
    {
        convoCount++;
        scoreCount += convoCount;

        SetConvo(convoCount);
        SetScore(scoreCount);
    }
    public void Miss()
    {
        convoCount = 0;
        SetConvo(convoCount);
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
    float recordingStartTime;

    public void StartRecording()
    {
        beats = new List<Beat>();
        isRecording = true;
        recordingStartTime = Time.time;

        audioSource.Play();
    }

    private void RecordKeys()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            beats.Add(new Beat(Beat.TrashType.Paper, Time.time - recordingStartTime - travelTime));
            Debug.Log("Key Recorded: " + Beat.TrashType.Paper);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            beats.Add(new Beat(Beat.TrashType.Plastic, Time.time - recordingStartTime - travelTime));
            Debug.Log("Key Recorded: " + Beat.TrashType.Plastic);
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            beats.Add(new Beat(Beat.TrashType.Aluminum, Time.time - recordingStartTime - travelTime));
            Debug.Log("Key Recorded: " + Beat.TrashType.Aluminum);
        }
    }


    // TRASH LAUNCHING ===============================================================================
    [Header("Trash Spawn")]
    [SerializeField] float travelTime;
    [SerializeField] Transform launchPoint;
    [SerializeField] Vector2 direction;
    [SerializeField] float launchForce;
    // [SerializeField] GameObject trashItemPrefab;

    [SerializeField] GameObject[] trashItemsPrefabs;

    public void LaunchTrashItem(GameObject trashItemPrefab)
    {
        GameObject item = Instantiate(trashItemPrefab, launchPoint.position, Quaternion.Euler(new Vector3(0, 0, -15)));
        Rigidbody2D rigidbody2D = item.GetComponent<Rigidbody2D>();
        rigidbody2D.AddForce(direction * launchForce, ForceMode2D.Impulse);
        Destroy(item, 2.3f);
    }

    IEnumerator LaunchCicle()
    {
        float prevTime = 0;
        while (playingBeat)
        {
            foreach (var b in beats)
            {
                float delay = b.time - prevTime;
                prevTime = b.time;
                yield return new WaitForSecondsRealtime(delay);
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
            yield return new WaitForSecondsRealtime(2f);
            GameOver();
        }
    }

    // UI ===============================================================================
    [Header("UI")]
    [SerializeField] TextMeshProUGUI convoTMP;
    [SerializeField] TextMeshProUGUI scoreTMP;
    [SerializeField] TextMeshProUGUI recordTMP;

    int record = 0;
    private void SetConvo(int convoCount)
    {
        convoTMP.text = "Combo: " + convoCount;
    }

    private void SetScore(int score)
    {
        scoreTMP.text = "Puntaje: " + score;

        if (score > record)
        {
            recordTMP.text = "Record: " + score;
        }
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

    public void ExitGame()
    {
        Application.Quit();
    }

}
