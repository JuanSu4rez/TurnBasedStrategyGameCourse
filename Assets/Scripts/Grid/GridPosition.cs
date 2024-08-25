using System;
using System.Collections.Generic;

public struct GridPosition : IEquatable<GridPosition>{

    public int x;
    public int z;
    public Unit _unit { get; set; }

    public GridPosition (int x, int z) {
        this.x = x;
        this.z = z;
        _unit = null;
    }
    public GridPosition (int x, int z, Unit unit) {
        this.x = x;
        this.z = z;
        _unit = unit;
    }

    public override string ToString () => $"x: {x}, z: {z}";

    public override bool Equals (object obj) {
        return obj is GridPosition position &&
               x == position.x &&
               z == position.z;
    }

    public override int GetHashCode () {
        return HashCode.Combine(x, z);
    }

    public bool Equals (GridPosition other) {
        return this == other;
    }

    public static bool operator == (GridPosition a, GridPosition b){
        return a.x == b.x && a.z == b.z;
    }

    public static bool operator != (GridPosition a, GridPosition b) {
        return !(a == b);
    }

    public static GridPosition operator + (GridPosition a, GridPosition b) {
        return new GridPosition( a.x + b.x, a.z + b.z);
    }

    public static GridPosition operator - (GridPosition a, GridPosition b) {
        return new GridPosition(a.x - b.x, a.z - b.z);
    }


}


