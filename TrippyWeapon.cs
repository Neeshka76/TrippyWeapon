using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderRoad;
using UnityEngine;

namespace TrippyWeapon
{
    public class TrippyWeapon : MonoBehaviour, IDisposable
    {

        private Item itemTrippyWeapon;

        private TrippyEffect trippyEffect;

        private int nbHand;

        /// <summary>
        /// this items Module.
        /// </summary>
        public TrippyWeaponItemModule ItemModule { get; internal set; }

        public void Dispose()
        {
            itemTrippyWeapon.mainHandleLeft.Grabbed -= OnItemGrabbed;
            itemTrippyWeapon.mainHandleRight.Grabbed -= OnItemGrabbed;
            itemTrippyWeapon.mainHandleLeft.UnGrabbed -= OnItemUngrabbed;
            itemTrippyWeapon.mainHandleRight.UnGrabbed -= OnItemUngrabbed;
        }

        private void OnEnable()
        {
            // Each time this is unpooled it will recache and reinitialize.

            // Cache.
            itemTrippyWeapon = GetComponent<Item>();
            itemTrippyWeapon.mainHandleLeft.Grabbed += OnItemGrabbed;
            itemTrippyWeapon.mainHandleRight.Grabbed += OnItemGrabbed;
            itemTrippyWeapon.mainHandleLeft.UnGrabbed += OnItemUngrabbed;
            itemTrippyWeapon.mainHandleRight.UnGrabbed += OnItemUngrabbed;
            trippyEffect = itemTrippyWeapon.gameObject.AddComponent<TrippyEffect>();
            nbHand = 0;
            // Load effects.
        }

        private void OnDisable()
        {
            // When this gets pooled/destroyed, dispose of anything that may cause issues 
            // when it's next unpooled.
            Dispose();
        }

        /// <summary>
        /// When the item is grabbed, activate the post process effect.
        /// </summary>
        private void OnItemGrabbed(RagdollHand ragdollHand, Handle handle, EventTime eventTime)
        {
            // Is the event not OnEnd?
            if (eventTime != EventTime.OnEnd)
            {
                return;
            }
            if (ragdollHand.creature.isPlayer)
            {
                nbHand++;
                if (trippyEffect.overrideValue == false)
                {
                    trippyEffect.overrideValue = true;
                }
                if (trippyEffect.changeValue != true)
                {
                    trippyEffect.changeValue = true;
                }
            }
        }

        /// <summary>
        /// When the item is ungrabbed, desactivate the post process effect.
        /// </summary>
        private void OnItemUngrabbed(RagdollHand ragdollHand, Handle handle, EventTime eventTime)
        {
            // Is the event not OnEnd?
            if (eventTime != EventTime.OnEnd)
            {
                return;
            }
            if (ragdollHand.creature.isPlayer)
            {
                nbHand--;
                if (trippyEffect.overrideValue != false && nbHand == 0)
                {
                    trippyEffect.overrideValue = false;
                }
                if (trippyEffect.changeValue != false && nbHand == 0)
                {
                    trippyEffect.changeValue = false;
                }
            }
        }
    }
}
