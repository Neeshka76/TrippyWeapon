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
        private bool grabbed;
        private float currentPull;
        private Vector3 startLocalPos;
        public bool enableEffectOnBow = true;

        private TrippyBowEffect trippyBowEffect;

        /// <summary>
        /// this items Module.
        /// </summary>
        public BowItemModule ItemModule { get; internal set; }


        public void Init(bool activate)
        {
            enableEffectOnBow = activate;
        }

        public void OnEnable()
        {
            item = GetComponent<Item>();
            bowString = item.GetComponentInChildren<BowString>();
            trippyBowEffect = item.gameObject.AddComponent<TrippyBowEffect>();
            item.mainHandleLeft.Grabbed += OnBowGrabbed;
            item.mainHandleRight.Grabbed += OnBowGrabbed;
            item.mainHandleLeft.UnGrabbed += OnBowUngrabbed;
            item.mainHandleRight.UnGrabbed += OnBowUngrabbed;
            startLocalPos = bowString.transform.localPosition;
        }

        public void FixedUpdate()
        {
            if (enableEffectOnBow)
            {
                if (bowString.isPulling == true && grabbed == true)
                {
                    Debug.Log("BowBehaviour : Ispulling && grabbed");
                    if (trippyBowEffect.overrideValue == false)
                    {
                        trippyBowEffect.overrideValue = true;
                    }
                    if (trippyBowEffect.changeValue != true)
                    {
                        trippyBowEffect.changeValue = true;
                    }
                    currentPull = Mathf.Clamp(((startLocalPos.z - bowString.transform.localPosition.z) - bowString.pullOffset) * bowString.pullMultiplier, 0.0f, bowString.animationClipLength);
                    Debug.Log("currentPull : " + currentPull);
                    trippyBowEffect.valueOfDistorsion = Snippet.RemapClamp01(currentPull, 0.0f, 1.0f);
                    Debug.Log("value of distorsion : " + currentPull);
                }
                else
                {
                    if (trippyBowEffect.overrideValue != false)
                    {
                        trippyBowEffect.overrideValue = false;
                    }
                    if (trippyBowEffect.changeValue != false)
                    {
                        trippyBowEffect.changeValue = false;
                        trippyBowEffect.valueOfDistorsion = 0f;
                    }
                }
            }
        }

        /// <summary>
        /// When the bow is grabbed.
        /// </summary>
        private void OnBowGrabbed(RagdollHand ragdollHand, Handle handle, EventTime eventTime)
        {
            // Is the event not OnEnd?
            if (eventTime != EventTime.OnStart)
            {
                return;
            }
            //Work with 2 hands but not with the first one 
            // Set flag.
            if (ragdollHand.creature == Player.local.creature)
            {
                grabbed = true;
                Debug.Log("BowBehaviour : grabbed");
            }
        }

        /// <summary>
        /// When the bow is grabbed.
        /// </summary>
        private void OnBowUngrabbed(RagdollHand ragdollHand, Handle handle, EventTime eventTime)
        {
            // Is the event not OnEnd?
            if (eventTime != EventTime.OnEnd)
            {
                return;
            }

            // Set flag.
            if (ragdollHand.creature.isPlayer)
            {
                grabbed = false;
                Debug.Log("BowBehaviour : Ungrabbed");
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
            item.mainHandleLeft.Grabbed -= OnBowGrabbed;
            item.mainHandleRight.Grabbed -= OnBowGrabbed;
            item.mainHandleLeft.UnGrabbed -= OnBowUngrabbed;
            item.mainHandleRight.UnGrabbed -= OnBowUngrabbed;
        }
    }
}
