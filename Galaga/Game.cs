namespace Galaga;
using System;
using System.Collections.Generic;
using System.IO;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.Physics;


public class Game : DIKUGame, IGameEventProcessor {
    private Player player;
    private EntityContainer<Enemy> enemies;
    private EntityContainer<PlayerShot> playerShots;
    private IBaseImage playerShotImage;
    private GameEventBus eventBus;
    private AnimationContainer enemyExplosions;
    private List<Image> explosionStrides;
    private const int EXPLOSION_LENGTH_MS = 500;


    public Game(WindowArgs windowArgs) : base(windowArgs) {
        InitPlayer();
        InitEnemies();
        InitPlayerShot();
        InitEventBus();
    }

    private void IterateShots() {
        playerShots.Iterate(shot => {
            // move the shot's shape
            shot.Shape.Move();
            // CollisionDetection.Aabb(shot.Shape.AsDynamicShape(), shot.Shape);
            if (shot.Shape.Position.X < 0.0f || shot.Shape.Position.X > 1.0f - shot.Shape.Extent.X
                || shot.Shape.Position.Y < 0.0f || shot.Shape.Position.Y > 1.0f) {
                //delete the shot
                shot.DeleteEntity();
            } else {
                enemies.Iterate(enemy => {
                    // if collision btw shot and enemy -> delete both entities
                    bool check = CollisionDetection.Aabb(shot.Shape.AsDynamicShape(), enemy.Shape).Collision;
                    if (check) {
                        this.AddExplosion(enemy.Shape.Position, enemy.Shape.Extent);
                        shot.DeleteEntity();
                        enemy.DeleteEntity();
                    }
                });
            }
        });
    }

    public override void Render() {
        player.Render();
        enemies.RenderEntities();
        playerShots.RenderEntities();
        enemyExplosions.RenderAnimations();
    }

    public override void Update() {
        eventBus.ProcessEventsSequentially();
        player.Move();
        IterateShots();
    }

    private void KeyPress(KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Escape:
                GameEvent closeEvent= new GameEvent();
                closeEvent.EventType = GameEventType.WindowEvent;
                closeEvent.Message = "Close Window";
                eventBus.RegisterEvent(closeEvent);
                break;
            case KeyboardKey.Left:
            case KeyboardKey.Right:
            case KeyboardKey.Up:
            case KeyboardKey.Down:
                GameEvent keyPress = new GameEvent();
                keyPress.EventType = GameEventType.InputEvent;
                keyPress.Message = $"KeyPress: {key}";
                eventBus.RegisterEvent(keyPress);
                break;
            default:
                break;
        }
    }

    private void KeyRelease(KeyboardKey key) {
        GameEvent keyRelease = new GameEvent();
        keyRelease.EventType = GameEventType.InputEvent;
        keyRelease.Message = $"KeyRelease: {key}";
        eventBus.RegisterEvent(keyRelease);
    }

    private void KeyHandler(KeyboardAction action, KeyboardKey key) {
        switch (action) {
            case KeyboardAction.KeyPress:
                this.KeyPress(key);
                break;
            case KeyboardAction.KeyRelease:
                this.KeyRelease(key);
                break;
        }
    }

    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.WindowEvent) {
            if (gameEvent.Message == "Close Window") {
                window.CloseWindow();
            }
        }
        else if (gameEvent.EventType == GameEventType.InputEvent) {
            string[] parts = gameEvent.Message.Split(": ");
            string inputType = parts[0];
            string input = parts[1];
            bool keyPressed = (inputType == "KeyPress");
            switch (input) {
                case "Left":
                    player.SetMoveLeft(keyPressed);
                    break;
                case "Right":
                    player.SetMoveRight(keyPressed);
                    break;
                case "Up":
                    player.SetMoveUp(keyPressed);
                    break;
                case "Down":
                    player.SetMoveDown(keyPressed);
                    break;
            }
            if (inputType == "KeyRelease" && input == "Space") {
                Vec2F shotFromMiddle = new (player.GetPosition().X + player.GetExtent().X/2,
                player.GetPosition().Y);
                playerShots.AddEntity(new PlayerShot(shotFromMiddle, playerShotImage));
            }
        }
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
            Vec2F pos = new Vec2F(0.1f + (float) i * 0.1f, 0.9f);
            Vec2F extent = new Vec2F(0.1f, 0.1f);
            Enemy enemy = new Enemy(
                new DynamicShape(pos, extent),
                new ImageStride(milliseconds, images));
            enemies.AddEntity(enemy);
        }
        enemyExplosions = new AnimationContainer(numEnemies);
        explosionStrides = ImageStride.CreateStrides(8,
        Path.Combine("Assets", "Images", "Explosion.png"));
    }

    public void InitEventBus() {
        eventBus = new GameEventBus();
        eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent, GameEventType.WindowEvent });
        window.SetKeyEventHandler(KeyHandler);
        eventBus.Subscribe(GameEventType.InputEvent, this);
        eventBus.Subscribe(GameEventType.WindowEvent, this);
    }

    public void InitPlayerShot() {
        playerShots = new EntityContainer<PlayerShot>();
        playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));
    }

    public void AddExplosion(Vec2F position, Vec2F extent) {
        StationaryShape explosion = new StationaryShape(position, extent);
        ImageStride explosionImage = new ImageStride(EXPLOSION_LENGTH_MS / 8, explosionStrides);
        enemyExplosions.AddAnimation(explosion, EXPLOSION_LENGTH_MS, explosionImage);
    }
}