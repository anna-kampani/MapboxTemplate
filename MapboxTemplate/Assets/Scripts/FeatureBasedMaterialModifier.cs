namespace Mapbox.Unity.MeshGeneration.Modifiers
{
    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;
    using Mapbox.Unity.MeshGeneration.Components;
    using Mapbox.Unity.MeshGeneration.Data;
    using Mapbox.Unity.Map;
    using System;

  
    /// <summary>
    /// Texture Modifier is a basic modifier which simply adds a TextureSelector script to the features.
    /// Logic is all pushed into this TextureSelector mono behaviour to make it's easier to change it in runtime.
    /// </summary>
    [CreateAssetMenu(menuName = "Mapbox/Modifiers/Feature Material Modifier")]
    public class FeatureBasedMaterialModifier : GameObjectModifier
    {

        GeometryMaterialOptions _options;
        //[SerializeField]
        //FeatureBasedMaterialModifierOptions _settings;

        [Header("Visualisation Settings")] 
        public string parameter;
        public Material material;
        [Header("Gradient Property Case Settings")]
        public Gradient gradient;
        public float domainStart = 2f;
        public float domainEnd = 25f;
      
       

        public override void SetProperties(ModifierProperties properties)
        {
            //_settings = (FeatureBasedMaterialModifierOptions)properties;
            _options = (GeometryMaterialOptions)properties;
            _options.PropertyHasChanged += UpdateModifier;

        }

        public override void UnbindProperties()
        {
            _options.PropertyHasChanged -= UpdateModifier;
        }

        private float GetRenderMode(float val)
        {
            return Mathf.Approximately(val, 1.0f) ? 0f : 3f;
        }

        public override void Run(VectorEntity ve, UnityTile tile)
        {
            var min = Math.Min(_options.materials.Length, ve.MeshFilter.mesh.subMeshCount);
            var mats = new Material[min];


            float renderMode = 0.0f;

            renderMode = GetRenderMode(_options.colorStyleColor.a);            
            
            for (int i = 0; i < min; i++)
            {
                if (ve.Feature.Data.GetProperties().ContainsKey(parameter))
                {
                    string _value = ve.Feature.Data.GetProperties()[parameter].ToString();
                    float colorValue = 0;
                    if (_value != "")
                    {
                        string[] width = _value.Split('m');
                        colorValue = float.Parse(width[0]).Remap(domainStart, domainEnd, 0, 1);                       
                    }
                    Color colour = gradient.Evaluate(colorValue);
                    
                    mats[i] = Instantiate(material);
                    mats[i].color = colour;
                }
                else
                {
                    mats[i] = Instantiate(material);
                }
            }

            ve.MeshRenderer.materials = mats;
        }
    }


}



public static class ExtensionMethods
{

    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
