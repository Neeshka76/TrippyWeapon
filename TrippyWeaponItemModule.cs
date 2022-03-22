using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderRoad;
using UnityEngine;

namespace TrippyWeapon
{
    public class TrippyWeaponItemModule : ItemModule
    {
        public override void OnItemLoaded(Item item)
        {
            // Load the base class.
            base.OnItemLoaded(item);

            // Is the current level the character selection screen?
            if (string.CompareOrdinal(Level.current.data.id, "CharacterSelection") == 0)
            {
                // Yes, do not initialize further.
                return;
            }

            // Add the HolsterShield component for initialization and set the module.
            item.gameObject.AddComponent<TrippyWeapon>().ItemModule = this;
        }
    }
}
