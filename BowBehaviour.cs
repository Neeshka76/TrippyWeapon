using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderRoad;
using UnityEngine;

namespace TrippyWeapon
{
    public class BowBehaviour : MonoBehaviour, IDisposable
    {
        private Item item;
        private BowString bowString;
        private float currentPull;
        private Vector3 startLocalPos;
        public bool enableEffectOnBow;
        private bool resetDone = false;

        private TrippyBowEffect trippyBowEffect;

        /// <summary>
        /// this items Module.
        /// </summary>
        public BowItemModule ItemModule { get; internal set; }

        public void OnEnable()
        {
            item = GetComponent<Item>();
            bowString = item.GetComponentInChildren<BowString>();
            trippyBowEffect = item.gameObject.AddComponent<TrippyBowEffect>();
            startLocalPos = bowString.transform.localPosition;
        }


        public void FixedUpdate()
        {
            if (enableEffectOnBow)
            {
                if (bowString.stringHandle.handlers.Exists(hand => hand.creature.isPlayer) && bowString.stringHandle.IsHanded())
                {
                    if (trippyBowEffect.overrideValue == false)
                    {
                        trippyBowEffect.overrideValue = true;
                    }
                    if (trippyBowEffect.changeValue == false)
                    {
                        trippyBowEffect.changeValue = true;
                    }
                    currentPull = Mathf.Clamp(((startLocalPos.z - bowString.transform.localPosition.z) - bowString.pullOffset) * bowString.pullMultiplier, 0.0f, bowString.animationClipLength);
                    trippyBowEffect.valueOfDistorsion = Snippet.RemapClamp01(currentPull, 0.0f, 1.0f);
                    resetDone = false;
                }
                else
                {
                    if (!resetDone)
                    {
                        if (trippyBowEffect.overrideValue == true)
                        {
                            trippyBowEffect.overrideValue = false;
                        }
                        if (trippyBowEffect.changeValue == true)
                        {
                            trippyBowEffect.valueOfDistorsion = 0f;
                            trippyBowEffect.changeValue = false;
                        }
                        resetDone = true;
                    }
                }
            }
            else
            {
                if (!resetDone)
                {
                    if (trippyBowEffect.overrideValue == true)
                    {
                        trippyBowEffect.overrideValue = false;
                    }
                    if (trippyBowEffect.changeValue == true)
                    {
                        trippyBowEffect.valueOfDistorsion = 0f;
                        trippyBowEffect.changeValue = false;
                    }
                    resetDone = true;
                }
            }
        }



        private void OnDisable()
        {
            // When this gets pooled/destroyed, dispose of anything that may cause issues 
            // when it's next unpooled.
            Dispose();
        }

        public void Dispose()
        {
            
        }
    }
}
