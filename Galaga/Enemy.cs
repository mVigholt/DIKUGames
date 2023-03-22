namespace Galaga;

using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga.Creation;

public class Enemy : Entity {

    public static float baseSpeed = 0.0003f;
    private float speed = baseSpeed;

    private static readonly Vec2F EXTENT = new Vec2F(0.1f, 0.1f);
    public readonly Vec2F startPosition;
    private IBaseImage alterImage;
    public int hitpoints {get; private set;}

    public Enemy(Vec2F position, IBaseImage image, IBaseImage alterImage)
        : base(new DynamicShape(position, EXTENT), image) {
        Image = image;
        this.alterImage = alterImage;
        startPosition = position;
        hitpoints = 4;
    }

    public Enemy(EnemyBuilder builder)
        : base(new DynamicShape(builder.position, EXTENT), builder.image) {
        this.startPosition = builder.position;
        this.Image = builder.image;
        this.alterImage = builder.alterImage;
        this.speed = builder.speed;
        this.hitpoints = builder.hitpoints;
    }

    public Enemy(
        Vec2F position,
        float speed,
        IBaseImage image,
        IBaseImage alterImage
    ) : base(new DynamicShape(position, EXTENT), image) {
        // Use this constructor to set speed
        Image = image;
        this.alterImage = alterImage;
        startPosition = position;
        hitpoints = 4;
        this.speed = speed;
    }    

    public float Speed {
        get {
            return speed;
        }
        private set {
            speed = value;
        }
    }

    public void LoseHealth(int hp) {
        hitpoints -= hp;
    }

    public bool IsDead() {
        return hitpoints <= 0;
    }

    public bool isEnraged(){
        if (this.hitpoints <= 2) {
            this.speed = baseSpeed * 10f;
            this.Image = alterImage;
            return true;
        }
        else {
            return false;
        }
    }
}

/// <summary>
/// The builder pattern works differently in C#
/// that it does in Java. In Java, you can use an inner
/// class and a private constructor (in Enemy).
/// Inner classes do not exist in C#.
/// Therefore, we must have a constructor that takes
/// many arguments.
/// I would still argue that the builder pattern
/// is better than the telescoping constructor pattern.
/// This implementation is taken from Mark's blog
/// (Mark from our team!):
/// https://blog.ploeh.dk/2017/08/15/test-data-builders-in-c/#aae3468437314471b7eb750dd6f50960
/// It's also inspired by the Java version from Effective Java.
/// - Asger
/// </summary>
public class EnemyBuilder : IBuilder<Enemy> {

    public Vec2F position;
    public IBaseImage image;
    public IBaseImage alterImage;
    
    // Numeric primitive types default to 0, not null
    // https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/default-values
    public int hitpoints;
    public float speed;

    public EnemyBuilder() {}

    public EnemyBuilder WithSpeed(float speed) {
        this.speed = speed;
        return this;
    }

    public EnemyBuilder WithPosition(Vec2F position) {
        this.position = position;
        return this;
    }

    public EnemyBuilder WithImage(IBaseImage image) {
        this.image = image;
        return this;
    }

    public EnemyBuilder WithAlternativeImage(IBaseImage image) {
        this.alterImage = image;
        return this;
    }

    public EnemyBuilder WithHitpoints(int hitpoints) {
        this.hitpoints = hitpoints;
        return this;
    }

    /// <summary>
    /// The final method you need to call when constructing
    /// an enemy. It is common practice to put all validation
    /// inside this method.
    /// </summary>
    public Enemy Build() {
        // Required arguments
        if (position is null ||
            image is null ||
            alterImage is null
        ) {
            throw new ArgumentException(
                "An enemy must have a position and two images");
        }
        // Optional arguments
        if (speed == 0f) {
            speed = 0.0003f;
        }
        if (hitpoints == 0) {
            hitpoints = 4;
        }
        return new Enemy(this);
    }
}