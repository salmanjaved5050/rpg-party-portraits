using System.Collections.Generic;
using UnityEngine;

namespace RpgPortraits.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PortraitsListing",menuName = "RpgPortraits/PortraitsListing")]
    public class PortraitsListing : ScriptableObject
    {
        public List<CharacterPortrait> CharacterPortraits;
    }
}
