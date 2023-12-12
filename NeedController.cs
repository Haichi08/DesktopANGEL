using System;
using UnityEngine;

public class NeedController : MonoBehaviour
{
    public int mental, happiness, energy;
    public int mentalTickRate, happinessTickRate, energyTickRate;
    public DateTime lastTimeFed,
           lastTimeHappy,
           lastTimeEnergized;

    private void Awake()
    {
    }

    public void Initialize(int mental, int happiness, int energy,
        int mentalTickRate, int happinessTickRate, int energyTickRate)
    {
        lastTimeFed = DateTime.Now;
        lastTimeHappy = DateTime.Now;
        lastTimeEnergized = DateTime.Now;
        this.mental = mental;
        this.happiness = happiness;
        this.energy = energy;
        this.mentalTickRate = mentalTickRate;
        this.happinessTickRate = happinessTickRate;
        this.energyTickRate = energyTickRate;
        PetUIController.instance.UpdateImages(mental, happiness, energy);
    }

    public void Initialize(int mental, int happiness, int energy,
       int mentalTickRate, int happinessTickRate, int energyTickRate,
       DateTime lastTimeFed, DateTime lastTimeHappy, DateTime lastTimeEnergized)
    {
        this.lastTimeFed = lastTimeFed;
        this.lastTimeHappy = lastTimeHappy;
        this.lastTimeEnergized = lastTimeEnergized;

        this.mental = mental
            - mentalTickRate
            * TickAmountSinceLastTimeToCurrentTime(lastTimeFed, TimingManager.instance.hourLength);

        this.happiness = happiness
             - happinessTickRate
                * TickAmountSinceLastTimeToCurrentTime(lastTimeHappy, TimingManager.instance.hourLength);

        this.energy = energy
            - energyTickRate
            * TickAmountSinceLastTimeToCurrentTime(lastTimeEnergized, TimingManager.instance.hourLength);

        this.mentalTickRate = mentalTickRate;
        this.happinessTickRate = happinessTickRate;
        this.energyTickRate = energyTickRate;
        if (this.mental < 0) this.mental = 0;
        if (this.happiness < 0) this.happiness = 0;
        if (this.energy < 0) this.energy = 0;
        PetUIController.instance.UpdateImages(this.mental, this.happiness, this.energy);
    }

    private void Update()
    {
        if (TimingManager.instance.gameHourTimer < 0)
        {
            ChangeMental(-mentalTickRate);
            ChangeHappiness(-happinessTickRate);
            ChangeEnergy(-energyTickRate);
            PetUIController.instance.UpdateImages(mental, happiness, energy);
        }
    }

    public void ChangeMental(int amount)
    {
        mental += amount;
        if (amount > 0)
        {
            lastTimeFed = DateTime.Now;

        }
        if (mental < 0)
        {
            PetManager.instance.Die();
        }
        else if (mental > 100) mental = 100;
    }

    public void ChangeHappiness(int amount)
    {
        happiness += amount;
        if (amount > 0)
        {
            lastTimeHappy = DateTime.Now;
        }
        if (happiness < 0)
        {
            PetManager.instance.Die();
        }
        else if (happiness > 100) happiness = 100;
    }

    public void ChangeEnergy(int amount)
    {
        energy += amount;
        if (amount > 0)
        {
            lastTimeEnergized = DateTime.Now;
        }
        if (energy < 0)
        {
            PetManager.instance.Die();
        }
        else if (energy > 100) energy = 100;
    }

    public int TickAmountSinceLastTimeToCurrentTime(DateTime lastTime, float tickRateInSeconds)
    {
        DateTime currentDateTime = DateTime.Now;
        int dayOfYearDifference = currentDateTime.DayOfYear - lastTime.DayOfYear;
        if (currentDateTime.Year > lastTime.Year
            || dayOfYearDifference >= 7) return 1500;
        int dayDifferenceSecondsAmount = dayOfYearDifference * 86400;
        if (dayOfYearDifference > 0) return Mathf.RoundToInt(dayDifferenceSecondsAmount / tickRateInSeconds);

        int hourDifferenceSecondsAmount = (currentDateTime.Hour - lastTime.Hour) * 3600;
        if (hourDifferenceSecondsAmount > 0) return Mathf.RoundToInt(hourDifferenceSecondsAmount / tickRateInSeconds);

        int minuteDifferenceSecondsAmount = (currentDateTime.Minute - lastTime.Minute) * 60;
        if (minuteDifferenceSecondsAmount > 0) return Mathf.RoundToInt(minuteDifferenceSecondsAmount / tickRateInSeconds);

        int secondDifferenceAmount = currentDateTime.Second - lastTime.Second;
        return Mathf.RoundToInt(secondDifferenceAmount / tickRateInSeconds);
    }
}

