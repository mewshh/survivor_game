using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;

public class GameManager : MonoInstaller
{
    [Header("References")]
    public UIManager uiManager;
    public PlayerController playerController;
    public GameObject[] enemyPrefabs;
    public bool gameOver;

    [Header("Death")]
    public Sprite deathSprite;

    [Header("Kills")]
    public int killCounter;

    public override void InstallBindings()
    {
        Container.Bind<UIManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlayerController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();

        Container.Bind<GameObject[]>().WithId("EnemyPrefabs").FromInstance(enemyPrefabs).AsSingle();

        Container.Bind<EnemyPool>().AsSingle().WithArguments(enemyPrefabs, 10);
        Container.Bind<EnemySpawner>().FromComponentInHierarchy().AsSingle();
    }


    public void SetDeathSprite(SpriteRenderer spriteRenderer)
    {
        spriteRenderer.sprite = deathSprite;
    }

    public void AddKill()
    {
        killCounter++;
        uiManager.UpdateKillCounterText(killCounter);
    }

    public void GameOver()
    {
        if (!gameOver)
        {
            gameOver = true;
            uiManager.ShowDarkPanel();
        }
    }
}
