using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderRoad;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace TrippyWeapon
{
    public class TrippyBowEffect : MonoBehaviour
    {
        public Volume volume;
        private LensDistortion lensDistortion;
        public float valueOfDistorsion = 0;
        public bool changeValue;
        public bool overrideValue = false;

        private void Awake()
        {
            volume = gameObject.AddComponent<Volume>();
            volume.priority = 0;
            volume.isGlobal = true;
            volume.weight = 1f;
            volume.profile = ScriptableObject.CreateInstance<VolumeProfile>();
            volume.profile.Add<LensDistortion>();
            changeValue = false;
        }
        private void Start()
        {
            volume.enabled = true;
            if (volume.profile.TryGet(out lensDistortion))
            {
                lensDistortion.active = overrideValue;
                lensDistortion.intensity.overrideState = overrideValue;
                lensDistortion.scale.overrideState = overrideValue;
            }
        }

        private void FixedUpdate()
        {
            if (changeValue)
            {
                volume.enabled = overrideValue;
                if (volume.profile.TryGet(out lensDistortion))
                {
                    lensDistortion.active = overrideValue;
                    lensDistortion.intensity.overrideState = overrideValue;
                    lensDistortion.scale.overrideState = overrideValue;
                    //Go from 0 to -1f
                    lensDistortion.intensity.value = Snippet.RemapClamp(valueOfDistorsion, 0f, 1f, 0f, lensDistortion.intensity.min);
                    // Go from 1 to 0.1f
                    lensDistortion.scale.value = Snippet.RemapClamp(valueOfDistorsion, 0f, 1f, 1f, 0.3f);
                    //lensDistortion.intensity.value = Snippet.RemapClamp(valueOfDistorsion, 0f, 1f, 1f, lensDistortion.scale.min);
                    //lensDistortion.scale.value = Mathf.Clamp(valueOfDistorsion + 1f, lensDistortion.scale.min, 1f);
                }
            }
            else
            {
                lensDistortion.intensity.value = 0f;
                lensDistortion.scale.value = 1f;
                lensDistortion.intensity.overrideState = overrideValue;
                lensDistortion.scale.overrideState = overrideValue;
                volume.enabled = overrideValue;
            }
        }
    }
}
