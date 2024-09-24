using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Zenject;

public class GameManager : MonoInstaller
{
    [Header("References")]
    public UIManager uiManager;
    public PlayerController playerController;
    public GameManager gameManager;

    [Header("Death")]
    public Sprite deathSprite;

    public override void InstallBindings()
    {
        Container.Bind<UIManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlayerController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();
    }
}
