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
    public class TrippyEffect : MonoBehaviour
    {
        public Volume volume;
        private Bloom bloom;
        private MotionBlur motionBlur;
        private ChromaticAberration chromaticAberration;
        private PaniniProjection paniniProjection;
        private DepthOfField depthOfField;
        private LensDistortion lensDistortion;
        public float speed = 1;
        public bool changeValue;
        public bool overrideValue = false;

        private void Awake()
        {
            volume = gameObject.AddComponent<Volume>();
            volume.priority = 0;
            volume.isGlobal = true;
            volume.weight = 1f;
            volume.profile = ScriptableObject.CreateInstance<VolumeProfile>();
            volume.profile.Add<Bloom>();
            volume.profile.Add<MotionBlur>();
            volume.profile.Add<ChromaticAberration>();
            volume.profile.Add<PaniniProjection>();
            volume.profile.Add<DepthOfField>();
            volume.profile.Add<LensDistortion>();
            changeValue = false;
        }
        private void Start()
        {
            volume.enabled = true;
            if (volume.profile.TryGet(out bloom))
            {
                bloom.active = overrideValue;
                bloom.threshold.overrideState = overrideValue;
                bloom.intensity.overrideState = overrideValue;
                bloom.scatter.overrideState = overrideValue;
            }
            if (volume.profile.TryGet(out motionBlur))
            {
                motionBlur.active = overrideValue;
                motionBlur.mode.Override(MotionBlurMode.CameraAndObjects);
                motionBlur.quality.Override(MotionBlurQuality.High);
                motionBlur.mode.overrideState = overrideValue;
                motionBlur.quality.overrideState = overrideValue;
                motionBlur.intensity.overrideState = overrideValue;
            }
            if (volume.profile.TryGet(out chromaticAberration))
            {
                chromaticAberration.active = overrideValue;
                chromaticAberration.intensity.overrideState = overrideValue;
            }
            if (volume.profile.TryGet(out paniniProjection))
            {
                paniniProjection.active = overrideValue;
                paniniProjection.distance.overrideState = overrideValue;
                paniniProjection.cropToFit.overrideState = overrideValue;
            }
            if (volume.profile.TryGet(out depthOfField))
            {
                depthOfField.active = overrideValue;
                depthOfField.mode.overrideState = overrideValue;
                depthOfField.mode.Override(DepthOfFieldMode.Bokeh);
                depthOfField.focusDistance.overrideState = overrideValue;
                depthOfField.focalLength.overrideState = overrideValue;
                depthOfField.aperture.overrideState = overrideValue;
                depthOfField.bladeCount.overrideState = overrideValue;
                depthOfField.bladeCurvature.overrideState = overrideValue;
                depthOfField.bladeRotation.overrideState = overrideValue;
            }
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
                if (volume.profile.TryGet(out bloom))
                {
                    bloom.active = overrideValue;
                    bloom.threshold.overrideState = overrideValue;
                    bloom.intensity.overrideState = overrideValue;
                    bloom.scatter.overrideState = overrideValue;
                    bloom.threshold.value = Mathf.Lerp(0f, 1f, Mathf.PingPong(Time.time * speed * 1f, 1));
                    bloom.intensity.value = Mathf.Lerp(0.1f, 0.3f, Mathf.PingPong(Time.time * speed * 1f, 1));
                    bloom.scatter.value = Mathf.Lerp(0f, 1f, Mathf.PingPong(Time.time * speed * 1f, 1));
                }
                if (volume.profile.TryGet(out motionBlur))
                {
                    motionBlur.active = overrideValue;
                    motionBlur.mode.overrideState = overrideValue;
                    motionBlur.quality.overrideState = overrideValue;
                    motionBlur.intensity.overrideState = overrideValue;
                    motionBlur.mode.Override(MotionBlurMode.CameraAndObjects);
                    motionBlur.quality.Override(MotionBlurQuality.High);
                    motionBlur.intensity.value = Mathf.Lerp(0.1f, 0.55f, Mathf.PingPong(Time.time * speed * 1f, 1));
                }
                if (volume.profile.TryGet(out chromaticAberration))
                {
                    chromaticAberration.active = overrideValue;
                    chromaticAberration.intensity.overrideState = overrideValue;
                    chromaticAberration.intensity.value = Mathf.Lerp(0f, 1f, Mathf.PingPong(Time.time * speed * 0.5f, 1));
                }
                if (volume.profile.TryGet(out paniniProjection))
                {
                    paniniProjection.active = overrideValue;
                    paniniProjection.distance.overrideState = overrideValue;
                    paniniProjection.cropToFit.overrideState = overrideValue;
                    paniniProjection.distance.value = Mathf.Lerp(1f, 0f, Mathf.PingPong(Time.time * speed * 1f, 1));
                    paniniProjection.cropToFit.value = Mathf.Lerp(0f, 1f, Mathf.PingPong(Time.time * speed * 1f, 1));
                }
                if (volume.profile.TryGet(out depthOfField))
                {
                    depthOfField.active = overrideValue;
                    depthOfField.mode.overrideState = overrideValue;
                    depthOfField.mode.Override(DepthOfFieldMode.Bokeh);
                    depthOfField.focusDistance.overrideState = overrideValue;
                    depthOfField.focalLength.overrideState = overrideValue;
                    depthOfField.aperture.overrideState = overrideValue;
                    depthOfField.bladeCount.overrideState = overrideValue;
                    depthOfField.bladeCurvature.overrideState = overrideValue;
                    depthOfField.bladeRotation.overrideState = overrideValue;
                    depthOfField.focusDistance.value = Mathf.Lerp(0.1f, 30f, Mathf.PingPong(Time.time * speed * 1f, 1));
                    depthOfField.focalLength.value = Mathf.Lerp(30f, 60f, Mathf.PingPong(Time.time * speed * 1f, 1));
                    depthOfField.aperture.value = Mathf.Lerp(1f, 32f, Mathf.PingPong(Time.time * speed * 1f, 1));
                    depthOfField.bladeCount.value = (int)Mathf.Lerp(3f, 9f, Mathf.PingPong(Time.time * speed * 1f, 1));
                    depthOfField.bladeCurvature.value = Mathf.Lerp(0f, 1f, Mathf.PingPong(Time.time * speed * 1f, 1));
                    depthOfField.bladeRotation.value = Mathf.Lerp(-180f, 180f, Mathf.PingPong(Time.time * speed * 1f, 1));
                }
                if (volume.profile.TryGet(out lensDistortion))
                {
                    lensDistortion.active = overrideValue;
                    lensDistortion.intensity.overrideState = overrideValue;
                    lensDistortion.scale.overrideState = overrideValue;
                    lensDistortion.intensity.value = Mathf.Lerp(-1f, 1f, Mathf.PingPong(Time.time * speed * 0.5f, 1));
                    lensDistortion.scale.value = Mathf.Lerp(0.3f, 1f, Mathf.PingPong(Time.time * speed * 1f, 1));
                }
            }
            else
            {
                volume.enabled = overrideValue;
            }
        }
    }
}
