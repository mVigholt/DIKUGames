using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System.Collections.Generic;
using System;

namespace Galaga;

public class Game : DIKUGame, IGameEventProcessor {
    private Player player;
    private EntityContainer<Enemy> enemies;

    private GameEventBus eventBus;

    public Game(WindowArgs windowArgs) : base(windowArgs) {
        InitPlayer();
        InitEnemies();
        InitEventBus();
    }

    public override void Render() {
        player.Render();
        enemies.RenderEntities();
    }

    public override void Update() {
        eventBus.ProcessEventsSequentially();
        player.Move();
    }

    private void KeyPress(KeyboardKey key) {
         switch (key) {
            case KeyboardKey.Escape:
                window.CloseWindow();
                break;
            case KeyboardKey.Left:
                player.SetMoveLeft(true);
                break;
            case KeyboardKey.Right:
                player.SetMoveRight(true);
                break;
            default:
                break;
        }
    }

    private void KeyRelease(KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Left:
                player.SetMoveLeft(false);
                break;
            case KeyboardKey.Right:
                player.SetMoveRight(false);
                break;
            default:
                break;
        }
    }

    private void KeyHandler(KeyboardAction action, KeyboardKey key) {
        // TODO: Switch on KeyBoardAction and call proper method
        switch(action){
            case KeyboardAction.KeyPress:
                this.KeyPress(key);
                break;
            case KeyboardAction.KeyRelease:
                this.KeyRelease(key);
                break;
        }
    }

    public void ProcessEvent(GameEvent gameEvent) {
        // Leave this empty for now
    }

    public void InitPlayer() {
        player = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", "Player.png")));
    }

    /// <summary>Create a few enemies and add them to the game</summary>
    public void InitEnemies() {
        string imagePath = Path.Combine("Assets", "Images", "BlueMonster.png");
        int numStrides = 4;
        List<Image> images = ImageStride.CreateStrides(numStrides, imagePath);
        const int numEnemies = 8;
        enemies = new EntityContainer<Enemy>(numEnemies);
        for (int i = 0; i < numEnemies; i++) {
            int milliseconds = 80;
            Vec2F pos = new Vec2F(0.1f + (float)i * 0.1f, 0.9f);
            Vec2F extent = new Vec2F(0.1f, 0.1f);
            Enemy enemy = new Enemy(
                new DynamicShape(pos, extent),
                new ImageStride(milliseconds, images));
            enemies.AddEntity(enemy);
        }
    }

    public void InitEventBus() {
        eventBus = new GameEventBus();
        eventBus.InitializeEventBus(new List<GameEventType> {GameEventType.InputEvent});
        window.SetKeyEventHandler(KeyHandler);
        eventBus.Subscribe(GameEventType.InputEvent, this);
    }
}