/*
 * collect from : https://coffeebraingames.wordpress.com/2017/10/08/better-c-enums/
 * 一种多常数值的实现，不一定做枚举的替代，可做一种参考
 */
public class Planet {

    // The different values
    public static readonly Planet MERCURY = new Planet(0, 0.330f, 4879, 5427, 3.7f);
    public static readonly Planet VENUS = new Planet(1, 4.87f, 12104, 5243, 8.9f);
    public static readonly Planet EARTH = new Planet(2, 5.97f, 12756, 5514, 9.8f);
    public static readonly Planet MARS = new Planet(3, 0.642f, 6792, 3933, 3.7f);
    public static readonly Planet JUPITER = new Planet(4, 1898.0f, 142984, 1326, 23.1f);
    public static readonly Planet SATURN = new Planet(5, 568.0f, 120536, 687, 9.0f);
    public static readonly Planet URANUS = new Planet(6, 86.8f, 51118, 1271, 8.7f);
    public static readonly Planet NEPTUNE = new Planet(7, 102.0f, 49528, 1638, 11.0f);
    public static readonly Planet PLUTO = new Planet(8, 0.0146f, 2370, 2095, 0.7f);

    // Use readonly to maintain immutability
    private readonly int id;
    private readonly float mass; // in 10^24 kg
    private readonly int diameter; // in km
    private readonly int density; // in kg/m^3
    private readonly float gravity; // in m/s^2

    // We use a private constructor because this should not be instantiated
    // anywhere else.
    private Planet(int id, float mass, int diameter, int density, float gravity) {
        this.id = id;
        this.mass = mass;
        this.diameter = diameter;
        this.density = density;
        this.gravity = gravity;
    }

    public int Id {
        get {
            return id;
        }
    }

    public float Mass {
        get {
            return mass;
        }
    }

    public int Diameter {
        get {
            return diameter;
        }
    }

    public int Density {
        get {
            return density;
        }
    }

    public float Gravity {
        get {
            return gravity;
        }
    }
    
}