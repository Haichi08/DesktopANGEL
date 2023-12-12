using System;

public class Pet
{
    public string lastTimeFed, lastTimeHappy, lastTimeEnergized;
    public int mental, happiness, energy;

    public Pet(string lastTimeFed,
        string lastTimeHappy,
        string lastTimeEnergized,
        int mental,
        int happiness,
        int energy)
    {
        this.lastTimeFed = lastTimeFed;
        this.lastTimeHappy = lastTimeHappy;
        this.lastTimeEnergized = lastTimeEnergized;
        this.mental = mental;
        this.energy = energy;
        this.happiness = happiness;
    }
}


