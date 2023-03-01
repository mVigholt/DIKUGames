namespace Galaga;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System.Collections.Generic;
using DIKUArcade.Physics;
using System;


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
            if (shot.Shape.Position.X < 0.0f || shot.Shape.Position.X > (1.0f - shot.Shape.Extent.X)
                || shot.Shape.Position.Y< 0.0f ||shot.Shape.Position.Y> 1.0f ){
                //delete the shot
                shot.DeleteEntity();
            }
            else {
                enemies.Iterate(enemy => {
            // if collision btw shot and enemy -> delete both entities
                bool check = CollisionDetection.Aabb(shot.Shape.AsDynamicShape(), enemy.Shape).Collision;
                if (check){
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
                case KeyboardKey.Space:
                    playerShots.AddEntity(new PlayerShot(player.GetPosition(), playerShotImage));
                    break;
                default:
                    break;
            }
        // TODO: switch on key string and disable the player's move direction
    }

    private void KeyHandler(KeyboardAction action, KeyboardKey key) {
        // TODO: Switch on KeyBoardAction and call proper method
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
        eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent });
        window.SetKeyEventHandler(KeyHandler);
        eventBus.Subscribe(GameEventType.InputEvent, this);
    }
    public void InitPlayerShot(){
        playerShots = new EntityContainer<PlayerShot>();
        playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));
    }

    public void AddExplosion(Vec2F position, Vec2F extent) {
        // TODO: add explosion to the AnimationContainer
        StationaryShape explosion = new StationaryShape(position, extent);
        ImageStride explosionImage = new ImageStride(EXPLOSION_LENGTH_MS/8, explosionStrides);
        enemyExplosions.AddAnimation(explosion, EXPLOSION_LENGTH_MS, explosionImage);
    }

}