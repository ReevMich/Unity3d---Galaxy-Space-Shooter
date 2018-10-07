using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private UIManager _uIManager;
    private SpawnManager _spawnManager;

    private bool gameStarted = false;

    private void Start()
    {
        _uIManager = FindObjectOfType<UIManager>();
        _spawnManager = FindObjectOfType<SpawnManager>();

        _uIManager.UpdateTitle(gameStarted);
    }

    private void Update()
    {
        if (Input.GetButtonUp("Submit") && !gameStarted)
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        gameStarted = true;
        _uIManager.UpdateTitle(gameStarted);
        _spawnManager.StartSpawning();
    }

    public void EndGame()
    {
        gameStarted = false;
        _uIManager.UpdateTitle(gameStarted);
        _spawnManager.StopSpawning();
    }

}
