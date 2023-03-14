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
    private List<Image> enemyStridesGreen ;
    private List<Image> enemyStridesRed;
    private List<Image> enemyStridesBlue;


    // Call different methods which initialize different classes
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        InitPlayer();
        InitEnemies();
        InitPlayerShot();
        InitEventBus();
    }

    /// <summary>Go through each shot and enemy to check if
    /// the shot has collided with enemies</summary>
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
                        shot.DeleteEntity();
                        enemy.Hitpoints--;
                    }
                    enemy.isEnraged();
                    if (enemy.Hitpoints <= 0){
                        this.AddExplosion(enemy.Shape.Position, enemy.Shape.Extent);
                        enemy.DeleteEntity();
                    }
                }
                );
            }
        });
    }

    ///<summary>Render different Entities, so that they can
    /// be drawn in the window </summary>
    public override void Render() {
        player.Render();
        enemies.RenderEntities();
        playerShots.RenderEntities();
        enemyExplosions.RenderAnimations();
    }

    ///<summary>call different methods in each game loop</summary>
    public override void Update() {
        eventBus.ProcessEventsSequentially();
        player.Move();
        IterateShots();
    }

    ///<summary>Register each keypress to a corresponding game event</summary>
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
                keyPress.EventType = GameEventType.PlayerEvent;
                keyPress.Message = $"KeyPress: {key}";
                eventBus.RegisterEvent(keyPress);
                break;
            default:
                break;
        }
    }

    ///<summary>Register each key release to a corresponding game event</summary>
    private void KeyRelease(KeyboardKey key) {
        GameEvent keyRelease = new GameEvent();
        keyRelease.EventType = GameEventType.PlayerEvent;
        keyRelease.Message = $"KeyRelease: {key}";
        eventBus.RegisterEvent(keyRelease);
    }

    ///<summary>Register keyboardAction to key press or key release</summary>
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

    ///<summary>The method which is called in the ProcessEvents()
    ///in GameEventBus, to handle each gameEvent</summary>
    public void ProcessEvent(GameEvent gameEvent) {
        string[] parts = gameEvent.Message.Split(": ");
        string inputType = parts[0];
        string input = parts[1];
        bool keyPressed = (inputType == "KeyPress");
        if (gameEvent.EventType == GameEventType.WindowEvent) {
            if (gameEvent.Message == "Close Window") {
                window.CloseWindow();
            }
        }
        else if (gameEvent.EventType == GameEventType.PlayerEvent) {
            player.ProcessEvent(gameEvent);
        }
        if (inputType == "KeyRelease" && input == "Space") {
            Vec2F shotFromMiddle = new (player.GetPosition().X + player.GetExtent().X/2,
            player.GetPosition().Y);
            playerShots.AddEntity(new PlayerShot(shotFromMiddle, playerShotImage));
        }
    }

    ///<summary> create new Player instance </summary>
    public void InitPlayer() {
        player = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", "Player.png")));
    }

    /// <summary>Create a few enemies and add them to the game</summary>
    public void InitEnemies() {
        enemyStridesBlue = ImageStride.CreateStrides
                            (4,Path.Combine("Assets",
                            "Images", "BlueMonster.png"));
        const int numEnemies = 8;
        enemies = new EntityContainer<Enemy>(numEnemies);
        enemyStridesGreen = ImageStride.CreateStrides
                            (2, Path.Combine("Assets",
                            "Images", "GreenMonster.png"));
        enemyStridesRed = ImageStride.CreateStrides
                            (2, Path.Combine("Assets",
                            "Images", "RedMonster.png"));
        for (int i = 0; i < numEnemies; i++) {
            int milliseconds = 80;
            Vec2F pos = new Vec2F(0.1f + (float) i * 0.1f, 0.9f);
            Vec2F extent = new Vec2F(0.1f, 0.1f);
            Enemy enemyBlue = new Enemy(
                new DynamicShape(pos, extent),
                new ImageStride(milliseconds, enemyStridesBlue),
                new ImageStride(milliseconds, enemyStridesRed)
                );
            enemies.AddEntity(enemyBlue);
        }

        for (int i = 0; i < numEnemies; i++) {
            int milliseconds = 80;
            Vec2F pos = new Vec2F(0.1f + (float) i * 0.1f, 0.8f);
            Vec2F extent = new Vec2F(0.1f, 0.1f);
            Enemy enemyGreen = new Enemy(
                new DynamicShape(pos, extent),
                new ImageStride(milliseconds, enemyStridesGreen),
                new ImageStride(milliseconds, enemyStridesRed));
            enemies.AddEntity(enemyGreen);
        }
        enemyExplosions = new AnimationContainer(numEnemies);
        explosionStrides = ImageStride.CreateStrides(8,
                Path.Combine("Assets", "Images", "Explosion.png"));
    }

    ///<summary> create new GameEventBus instance and
    ///subscribe it to a proper GameEventType</summary>
    public void InitEventBus() {
        eventBus = new GameEventBus();
        eventBus.InitializeEventBus(new List<GameEventType> {
             GameEventType.InputEvent,
             GameEventType.WindowEvent,
             GameEventType.PlayerEvent });
        window.SetKeyEventHandler(KeyHandler);
        eventBus.Subscribe(GameEventType.InputEvent, this);
        eventBus.Subscribe(GameEventType.WindowEvent, this);
        eventBus.Subscribe(GameEventType.PlayerEvent, this);
    }

    ///<summary> create new PlayerShot instance </summary>
    public void InitPlayerShot() {
        playerShots = new EntityContainer<PlayerShot>();
        playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));
    }

    ///<summary> create new explosion animation instance </summary>
    public void AddExplosion(Vec2F position, Vec2F extent) {
        StationaryShape explosion = new StationaryShape(position, extent);
        ImageStride explosionImage = new ImageStride(EXPLOSION_LENGTH_MS / 8, explosionStrides);
        enemyExplosions.AddAnimation(explosion, EXPLOSION_LENGTH_MS, explosionImage);
    }
}