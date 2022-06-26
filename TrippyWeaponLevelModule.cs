using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ThunderRoad;

namespace TrippyWeapon
{
    public class TrippyWeaponLevelModule : LevelModule
    {
        private TrippyEffect trippyEffect;
        public bool enableEffectOnAllWeapon;
        public bool enableEffectOnBow;
        private int nbHand = 0;
        public override IEnumerator OnLoadCoroutine()
        {
            EventManager.onPossess += EventManager_onPossess;
            EventManager.onUnpossess += EventManager_onUnpossess;
            trippyEffect = GameManager.local.gameObject.AddComponent<TrippyEffect>();
            return base.OnLoadCoroutine();
        }


        private void EventManager_onPossess(Creature creature, EventTime eventTime)
        {
            if (eventTime == EventTime.OnEnd)
            {
                creature.handLeft.OnGrabEvent += OnItemGrabbed;
                creature.handRight.OnGrabEvent += OnItemGrabbed;
                creature.handLeft.OnUnGrabEvent += OnItemUngrabbed;
                creature.handRight.OnUnGrabEvent += OnItemUngrabbed;
            }
        }
        private void EventManager_onUnpossess(Creature creature, EventTime eventTime)
        {
            if (eventTime == EventTime.OnStart)
            {
                creature.handLeft.OnGrabEvent -= OnItemGrabbed;
                creature.handRight.OnGrabEvent -= OnItemGrabbed;
                creature.handLeft.OnUnGrabEvent -= OnItemUngrabbed;
                creature.handRight.OnUnGrabEvent -= OnItemUngrabbed;
            }
        }

        /// <summary>
        /// When the item is grabbed, activate the post process effect.
        /// </summary>
        private void OnItemGrabbed(Side side, Handle handle, float axisPosition, HandlePose orientation, EventTime eventTime)
        {
            // Is the event not OnEnd?
            if (eventTime != EventTime.OnEnd)
            {
                return;
            }
            if(handle.item.itemId.Contains("Bow") && enableEffectOnBow)
            {
                handle.item.gameObject.GetOrAddComponent<BowBehaviour>().enableEffectOnBow = enableEffectOnBow;
            }
            if ((handle.item.data.type == ItemData.Type.Weapon || handle.item.data.type == ItemData.Type.Shield) && (!handle.item.itemId.Contains("Bow") && !handle.item.itemId.Contains("Arrow")) && enableEffectOnAllWeapon)
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
        private void OnItemUngrabbed(Side side, Handle handle, bool throwing, EventTime eventTime)
        {
            // Is the event not OnEnd?
            if (eventTime != EventTime.OnEnd)
            {
                return;
            }
            if (handle.item.itemId.Contains("Bow") && !handle.name.Contains("String") && enableEffectOnBow)
            {
                handle.item.gameObject.GetOrAddComponent<BowBehaviour>().enableEffectOnBow = false;
            }
            if ((handle.item.data.type == ItemData.Type.Weapon || handle.item.data.type == ItemData.Type.Shield) && (!handle.item.itemId.Contains("Bow") && !handle.item.itemId.Contains("Arrow")) && enableEffectOnAllWeapon)
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

        public override void OnUnload()
        {
            EventManager.onPossess -= EventManager_onPossess;
            EventManager.onUnpossess -= EventManager_onUnpossess;
        }

    }
}
