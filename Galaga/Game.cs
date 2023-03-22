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
using Galaga.GalagaStates;
using Galaga.MovementStrategy;
using Galaga.Squadron;

public class Game : DIKUGame, IGameEventProcessor {
    private Player player;
    private EntityContainer<PlayerShot> playerShots;
    private IBaseImage playerShotImage;
    private GameEventBus eventBus = GalagaBus.GetBus();
    private AnimationContainer enemyExplosions;
    private List<Image> explosionStrides;
    private const int EXPLOSION_LENGTH_MS = 500;
    private IMovementStrategy movementStrategy;
    private ISquadron squadron;
    private Score score;

    private List<Image> blueEnemyStride;
    private List<Image> greenEnemyStride;
    private List<Image> redEnemyStride;
    private Image playerImage;

    private TimedEvent Autoshoot = new TimedEvent(100);
    private StateMachine stateMachine;

    public Game(WindowArgs windowArgs) : base(windowArgs) {
        InitEventBus();
        InitAssets();
        ResetState();
    }

    private void ResetState() {
        Enemy.baseSpeed = 0.0003f;
        stateMachine = new StateMachine();
        InitPlayer();
        InitEnemies();
        InitPlayerShot();
        InitExplosion();
        InitScore();
    }

    private void InitAssets() {
        blueEnemyStride = ImageStride.CreateStrides(
            4, Path.Combine("Assets", "Images", "BlueMonster.png"));
        greenEnemyStride = ImageStride.CreateStrides(
            2, Path.Combine("Assets", "Images", "GreenMonster.png"));
        redEnemyStride = ImageStride.CreateStrides(
            2, Path.Combine("Assets", "Images", "RedMonster.png"));
        playerImage = new Image(
            Path.Combine("Assets", "Images", "Player.png"));
        explosionStrides = ImageStride.CreateStrides(8,
            Path.Combine("Assets", "Images", "Explosion.png"));
        playerShotImage = new Image(
            Path.Combine("Assets", "Images", "BulletRed2.png"));
    }

    /// <summary>
    /// Go through each shot and enemy to check if
    /// the shot has collided with enemies.
    /// </summary>
    private void IterateShots() {
        playerShots.Iterate(shot => {
            if (shot.Shape.Position.X < 0.0f || shot.Shape.Position.X > 1.0f - shot.Shape.Extent.X
                || shot.Shape.Position.Y < 0.0f || shot.Shape.Position.Y > 1.0f) {
                shot.DeleteEntity();
                return;
            }
            shot.Shape.Move();
            squadron.Enemies.Iterate(enemy => {
                bool collisionWithShot =
                    CollisionDetection.Aabb(shot.Shape.AsDynamicShape(), enemy.Shape).Collision;
                if (collisionWithShot) {
                    shot.DeleteEntity();
                    enemy.LoseHealth(1);
                }
            });

        });
    }

    // Check if the enemy is enraged or killed
    private void iterateEnemy(){
        squadron.Enemies.Iterate( enemy => {
            enemy.isEnraged();
            bool collisionWithPlayer =
                CollisionDetection.Aabb(player.Shape.AsDynamicShape(), enemy.Shape).Collision;
            if (collisionWithPlayer || enemy.Shape.Position.Y < 0f) {
                player.LoseHealth(enemy.hitpoints);
                enemy.LoseHealth(enemy.hitpoints);
            }

            if (enemy.IsDead()) {
                Explode(enemy);
                enemy.DeleteEntity();
                score.IncrementPoints();
            }

            if (player.IsDead()) {
                GameOver();
            }
        });
    }

    private void GameOver() {
        ResetState();
    }

    private void NextLevel() {
        Enemy.baseSpeed += 0.0002f;
        InitEnemies();
    }

    ///<summary>Render different Entities, so that they can
    /// be drawn in the window </summary>
    public override void Render() {
        player.Render();
        squadron.Enemies.RenderEntities();
        playerShots.RenderEntities();
        enemyExplosions.RenderAnimations();
        score.Render();
        stateMachine.ActiveState.RenderState();
    }

    ///<summary>call different methods in each game loop</summary>
    public override void Update() {
        eventBus.ProcessEventsSequentially();
        player.Move();
        IterateShots();
        iterateEnemy();
        movementStrategy.MoveEnemies(squadron.Enemies);
        if (squadron.HasLost()) {
            NextLevel();
        }

        if (Autoshoot.EventIsActive()) {
             eventBus.RegisterEvent(
                    new GameEvent {
                        EventType = GameEventType.InputEvent,
                        ObjectArg1 = KeyboardKey.Space,
                        IntArg1 = (int)KeyboardAction.KeyRelease,
                    }
                );
        }
    }

    ///<summary>Register each keypress to a corresponding game event</summary>
    private void KeyPress(KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Escape:
                eventBus.RegisterEvent(
                    new GameEvent {
                        EventType = GameEventType.WindowEvent,
                        ObjectArg1 = key,
                        IntArg1 = (int)KeyboardAction.KeyPress
                    }
                );
                break;
            case KeyboardKey.Left:
            case KeyboardKey.Right:
            case KeyboardKey.Up:
            case KeyboardKey.Down:
                eventBus.RegisterEvent(
                    new GameEvent {
                        EventType = GameEventType.PlayerEvent,
                        ObjectArg1 = key,
                        IntArg1 = (int)KeyboardAction.KeyPress
                    }
                );
                break;
            case KeyboardKey.A: //Autoshoot
                Autoshoot.startStop();
                break;
            default:
                break;
        }
    }

    ///<summary>Register each key release to a corresponding game event</summary>
    private void KeyRelease(KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Left:
            case KeyboardKey.Right:
            case KeyboardKey.Up:
            case KeyboardKey.Down:
                eventBus.RegisterEvent(
                    new GameEvent {
                        EventType = GameEventType.PlayerEvent,
                        ObjectArg1 = key,
                        IntArg1 = (int)KeyboardAction.KeyRelease
                    }
                );
                break;
            case KeyboardKey.Space:
                eventBus.RegisterEvent(
                    new GameEvent {
                        EventType = GameEventType.InputEvent,
                        ObjectArg1 = key,
                        IntArg1 = (int)KeyboardAction.KeyRelease,
                    }
                );
                break;
            default:
                break;
        }
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
        GameEventType? eventType = gameEvent.EventType;
        KeyboardKey? key = (KeyboardKey?)gameEvent.ObjectArg1;
        KeyboardAction? action = (KeyboardAction?)gameEvent.IntArg1;

        switch (eventType, key, action) {
            case (GameEventType.WindowEvent, KeyboardKey.Escape, KeyboardAction.KeyPress):
                window.CloseWindow();
                break;
            case (GameEventType.InputEvent, KeyboardKey.Space, KeyboardAction.KeyRelease):
                Vec2F shotFromMiddle = new (player.GetPosition().X +
                                            player.GetExtent().X/2,
                                            player.GetPosition().Y);
                playerShots.AddEntity(new PlayerShot(shotFromMiddle, playerShotImage));
                break;
            default:
                break;
        }
    }

    ///<summary> create new Player instance </summary>
    public void InitPlayer() {
        player = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            playerImage);
    }

    private ISquadron RandomSquadron() {
        int nTypesOfSquadron = 3;
        int randInt = new Random().Next(nTypesOfSquadron);
        var squadrons = new Dictionary<int, ISquadron>() {
            {
                0,
                new RowSquadron(
                    blueEnemyStride,
                    redEnemyStride
                )
            },
            {
                1,
                new TriangleSquadron(
                    greenEnemyStride,
                    redEnemyStride
                )
            },
            {
                2,
                new XSquadron(
                    blueEnemyStride,
                    redEnemyStride
                )
            },
        };
        return squadrons[randInt];
    }

    /// <summary>Create a few enemies and add them to the game</summary>
    public void InitEnemies() {
        squadron = RandomSquadron();
        squadron.Enemies.Iterate(enemy=>{
            movementStrategy = new ZigZagDown(enemy);
        });
    }

    public void InitExplosion(){
        enemyExplosions = new AnimationContainer(squadron.MaxEnemies);
    }

    ///<summary> create new GameEventBus instance and
    ///subscribe it to a proper GameEventType</summary>
    public void InitEventBus() {
        eventBus = GalagaBus.GetBus();
        window.SetKeyEventHandler(KeyHandler);
        eventBus.Subscribe(GameEventType.InputEvent, this);
        eventBus.Subscribe(GameEventType.WindowEvent, this);
        // eventBus.Subscribe(GameEventType.PlayerEvent, player);
    }

    ///<summary> create new PlayerShot instance </summary>
    public void InitPlayerShot() {
        playerShots = new EntityContainer<PlayerShot>();
    }

    ///<summary> create new explosion animation instance </summary>
    public void Explode(Entity entity) {
        Vec2F pos = entity.Shape.Position;
        Vec2F extent = entity.Shape.Extent;
        StationaryShape explosion = new StationaryShape(pos, extent);
        int nImages = 8;
        ImageStride stride =
            new ImageStride(EXPLOSION_LENGTH_MS / nImages, explosionStrides);
        enemyExplosions.AddAnimation(explosion, EXPLOSION_LENGTH_MS, stride);
    }

    public void InitScore(){
        score = new Score(new Vec2F (0.8f, 0.8f), new Vec2F(0.2f, 0.2f));
    }
}