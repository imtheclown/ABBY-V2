using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Unity.Mathematics;
using Unity.Collections;
using Unity.Jobs;

[UpdateInGroup(typeof(SimulationSystemGroup))]
public partial struct BioProcessSystem: ISystem{
    public void OnCreate(ref SystemState state){
        state.RequireForUpdate<PondWaterQuality>();
        state.RequireForUpdate<CommodityBioInfo>();
        state.RequireForUpdate<GameEnv>();
    }
    public void OnUpdate(ref SystemState state){
        var gameEnv = SystemAPI.GetSingleton<GameEnv>();
    }
}