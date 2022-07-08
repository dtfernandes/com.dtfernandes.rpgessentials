using System;

/// <summary>
/// Struct that defines a ranged between a min and a max value
/// </summary>
[System.Serializable]
public struct RangedInt : IEquatable<RangedInt>
{
    private int min;
    private int max;
    private bool flatten;

    /// <summary>
    /// Min value the int can be
    /// </summary>
    public int Min => min;

    /// <summary>
    /// Max value the int can be
    /// </summary>
    public int Max => max;

    /// <summary>
    /// Determines if the RangedInt is used as a range or is considered a normal int.
    /// If this variable is true the min value is used.
    /// </summary>
    public bool Flatten => flatten;

    //Random Component
    private static Random _rnd;

    /// <summary>
    /// Constructor for the RangedInt class.
    /// </summary>
    /// <param name="minV">Min value the int can be</param>
    /// <param name="maxV">Max value the int can be</param>
    public RangedInt(int minV, int maxV)
    {
        this.min = minV;
        this.max = maxV;
        flatten = false;
    }

    /// <summary>
    /// Flatten constructor for the RangedInt class.
    /// Flatten is toggle to true.
    /// </summary>
    /// <param name="value">Value used for both max and min</param>
    public RangedInt(int value)
    {
        this.min = value;
        this.max = value;
        flatten = true;
    }
    
    /// <summary>
    /// Operator that transforms a int into a RangeInt.
    /// Creates a flatten RangedInt.
    /// </summary>
    /// <param name="value">Value of the flatten RangeInt</param>
    public static implicit operator RangedInt(int value)
    {
        return new RangedInt(value);
    }

    public static bool operator ==(RangedInt self, RangedInt other)
    {
        return self.Equals(other);
    }

    public static bool operator !=(RangedInt self, RangedInt other)
    {
        return !self.Equals(other);
    }

    public bool Equals(RangedInt other)
    {
        return Flatten == other.Flatten 
            && Min == other.Min 
            && Max == other.Max;
    }

    /// <summary>
    /// Operator that transforms a RangedInt into a int
    /// </summary>
    /// <param name="self">Given RangedInt</param>
    public static implicit operator int(RangedInt self)
    {        
        if(_rnd == null)
            _rnd = new Random();

        return
            !self.Flatten ? _rnd.Next(self.Min, self.Max + 1) : self.Min;
    }

}
