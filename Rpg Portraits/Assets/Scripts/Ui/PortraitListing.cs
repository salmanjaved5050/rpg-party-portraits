using System.Collections.Generic;
using UnityEngine;

namespace RpgPortraits.Ui
{
    [CreateAssetMenu(fileName = "PortraitListing",menuName = "RpgPortraits/PortraitListing")]
    public class PortraitListing : ScriptableObject
    {
        public List<CharacterPortrait> CharacterPortraits;
    }
}
