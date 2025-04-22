using UnityEngine;
using Unity.Entities;


class CommodityAuthoring : MonoBehaviour{
    class Baker: Baker<CommodityAuthoring>{
        public override void Bake(CommodityAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new CommodityBioInfo {});

        }
    }
}

public struct CommodityBioInfo : IComponentData{
    public float weight;
    public float feedIntake;
    public bool isHungry;
}

