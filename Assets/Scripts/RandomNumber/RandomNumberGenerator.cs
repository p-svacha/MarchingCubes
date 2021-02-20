using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class generates random numbers using a seed. Implepentation via the Linear congruential generator algorithm
/// </summary>
public class RandomNumberGenerator
{
    public long Seed;

    private long Modulus;
    private long Multiplier;
    private long Increment;

    private long CurrentNumber;

    /* Recommended Values
     * Seed: 1 - Modulus
     * Modulo: 2^32 = 4294967296
     * Multiplier: 1664525
     * Increment: 1013904223
     */
    public RandomNumberGenerator(long seed = 0, long modulus = 4294967296, long multiplier = 1664525, long increment = 1013904223)
    {
        Seed = seed == 0 ? (long)Random.Range(1, modulus) : seed;
        Modulus = modulus;
        Multiplier = multiplier;
        Increment = increment;
        CurrentNumber = Seed;
        //Debug.Log("Mod: " + Modulus + ", Mult: " + Multiplier + ", Inc: " + Increment);
    }

    public float Next(float min = 0, float max = 1)
    {
        CurrentNumber = ((Multiplier * CurrentNumber) + Increment) % Modulus;
        float normalized = (1f * CurrentNumber / Modulus); // makes value 0-1
        float scaled = normalized * (max - min); // makes value 0-(max-min)
        float shifted = scaled + min; // makes value min-max
        return shifted;
    }
}