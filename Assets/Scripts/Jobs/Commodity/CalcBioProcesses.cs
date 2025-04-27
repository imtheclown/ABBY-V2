
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
public partial struct CaclBioProcesses: IJobEntity{
    [ReadOnly] public float temperature;
    public EntityCommandBuffer.ParallelWriter ecb;

    public void Execute(ref CommodityBioInfo bioInfo){
        float tempFactor = CalcTemperatureFactor(temperature);
        float activityFactor = CalcActivityFactor(bioInfo.hungerLevel); // (optional, based on hunger or digestion)
        float oxygenConsumption = GetBaseOxygenRate(bioInfo.weight) * bioInfo.weight * tempFactor * activityFactor;
    }

    float CalcTemperatureFactor(float temp)
    {
        // Example: faster metabolism at hotter temps
        if (temp < 20f) return 0.7f;
        if (temp < 28f) return 1.0f;
        if (temp <= 32f) return 1.2f;
        return 1.4f; // Hotter = more stress
    }

    float GetBaseOxygenRate(float weight)
    {
        //measure (mg O₂/g/hour)
        if (weight < 1f) // Postlarvae stage
        {
            return 0.65f; // Average of 0.5 - 0.8
        }
        else if (weight < 10f) // Juvenile stage
        {
            return 0.4f; // Average of 0.3 - 0.5
        }
        else // Adult stage
        {
            return 0.3f; // Average of 0.2 - 0.4
        }
    }

    float CalcActivityFactor(float hungerLevel)
    {
        // Clamp HungerLevel between 0 and 1 to be safe
        hungerLevel = math.clamp(hungerLevel, 0f, 1f);

        return 0.8f + (hungerLevel * 0.4f);
    }
}