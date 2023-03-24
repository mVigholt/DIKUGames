namespace Galaga.GalagaStates;

using System;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Galaga.GalagaStates;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using DIKUArcade.State;
using Galaga.MovementStrategy;
using Galaga.Squadron;

public class GameRunning : IGameState {
    private static GameRunning instance = null;
    private Player player;
    private GameEventBus eventBus = GalagaBus.GetBus();
    private EntityContainer<PlayerShot> playerShots;
    private ISquadron squadron;
    private const int EXPLOSION_LENGTH_MS = 500;
    private IMovementStrategy movementStrategy;
    private AnimationContainer enemyExplosions;
    private Score score;

    private TimedEvent Autoshoot;
    public static GameRunning GetInstance() {
        if (GameRunning.instance == null) {
            GameRunning.instance = new GameRunning();
            GameRunning.instance.InitializeGameState();
        }
        return GameRunning.instance;

    }

    public void InitializeGameState(){
        Autoshoot = new TimedEvent(100);
        InitPlayer();
        InitEnemies();
        InitPlayerShot();;
        InitExplosion();
        InitScore();
    }

    public void InitPlayer() {
            player = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            Assets.playerImage);

    }

    private ISquadron RandomSquadron() {
        int nTypesOfSquadron = 3;
        int randInt = new Random().Next(nTypesOfSquadron);
        var squadrons = new Dictionary<int, ISquadron>() {
            {
                0,
                new RowSquadron(
                    Assets.blueEnemyStride,
                    Assets.redEnemyStride
                )
            },
            {
                1,
                new TriangleSquadron(
                    Assets.greenEnemyStride,
                    Assets.redEnemyStride
                )
            },
            {
                2,
                new XSquadron(
                    Assets.blueEnemyStride,
                    Assets.redEnemyStride
                )
            },
        };
        return squadrons[randInt];
    }

    public void InitEnemies() {
        squadron = RandomSquadron();
        squadron.Enemies.Iterate(enemy=>{
            movementStrategy = new ZigZagDown(enemy);
        });
    }
     public void InitPlayerShot() {
        playerShots = new EntityContainer<PlayerShot>();
    }
    public void InitExplosion(){
        enemyExplosions = new AnimationContainer(squadron.MaxEnemies);
    }
    public void InitScore(){
        score = new Score(new Vec2F (0.8f, 0.8f), new Vec2F(0.2f, 0.2f));
    }
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

    private void IterateEnemy(){
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

    public void GameOver() {
        eventBus.RegisterEvent(
            new EventBuilder()
                .WithType(GameEventType.GameStateEvent)
                .WithStateType(GameStateType.GameLost)
                .Build()
        );
    }

    public void Explode(Entity entity) {
        Vec2F pos = entity.Shape.Position;
        Vec2F extent = entity.Shape.Extent;
        StationaryShape explosion = new StationaryShape(pos, extent);
        int nImages = 8;
        ImageStride stride =
            new ImageStride(EXPLOSION_LENGTH_MS / nImages, Assets.explosionStrides);
        enemyExplosions.AddAnimation(explosion, EXPLOSION_LENGTH_MS, stride);
    }

    /// <summary> Player increases level and enemies increase speed</summary>
     private void NextLevel() {
        Enemy.baseSpeed += 0.0002f;
        InitEnemies();
        player.Level++;
        GameWon();
    }

    /// <summary> when the level reaches to a certain number, GameWon</summary>
    private void GameWon(){
        if (player.Level >= 15){
            eventBus.RegisterEvent(
                new EventBuilder()
                    .WithType(GameEventType.GameStateEvent)
                    .WithStateType(GameStateType.GameWon)
                    .Build()
            );
        }

    }
    
    private void Shoot() {
        Vec2F shotFromMiddle = new (player.GetPosition().X +
                                    player.GetExtent().X/2,
                                    player.GetPosition().Y);
        playerShots.AddEntity(new PlayerShot(shotFromMiddle, Assets.playerShotImage));
    }

    public void RenderState() {
        player.Render();
        squadron.Enemies.RenderEntities();
        playerShots.RenderEntities();
        enemyExplosions.RenderAnimations();
        score.Render();
    }

    public void ResetState() {
        this.InitializeGameState();
        InitScore();
        Enemy.baseSpeed = 0.0003f;
    }

    public void UpdateState() {
        player.Move();
        IterateShots();
        IterateEnemy();
        movementStrategy.MoveEnemies(squadron.Enemies);
        if (squadron.HasLost()) {
            NextLevel();
        }
        if (Autoshoot.EventIsActive()) {
            Shoot();
        }
    }

    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        switch (action) {
            case KeyboardAction.KeyPress:
                this.KeyPress(key);
                break;
            case KeyboardAction.KeyRelease:
                this.KeyRelease(key);
                break;
        }
    }

    private void KeyPress(KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Escape:
                eventBus.RegisterEvent(
                    new EventBuilder()
                        .WithType(GameEventType.GameStateEvent)
                        .WithStateType(GameStateType.GamePaused)
                        .Build()
                );
                break;
            case KeyboardKey.Left:
            case KeyboardKey.Right:
            case KeyboardKey.Up:
            case KeyboardKey.Down:
                eventBus.RegisterEvent(
                    new EventBuilder()
                        .WithType(GameEventType.PlayerEvent)
                        .WithKey(key)
                        .WithAction(KeyboardAction.KeyPress)
                        .Build()
                );
                break;
            case KeyboardKey.A: //Autoshoot
                Autoshoot.startStop();
                break;
            default:
                break;
        }
    }

    private void KeyRelease(KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Left:
            case KeyboardKey.Right:
            case KeyboardKey.Up:
            case KeyboardKey.Down:
                eventBus.RegisterEvent(
                    new EventBuilder()
                        .WithType(GameEventType.PlayerEvent)
                        .WithKey(key)
                        .WithAction(KeyboardAction.KeyRelease)
                        .Build()
                );
                break;
            case KeyboardKey.Space:
                Shoot();
                break;
            default:
                break;
        }
    }

}