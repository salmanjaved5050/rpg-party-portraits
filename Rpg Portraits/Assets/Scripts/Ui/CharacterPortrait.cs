using System;
using UnityEngine;

namespace RpgPortraits.Ui
{
    [CreateAssetMenu(fileName = "CharacterPortrait",menuName = "RpgPortraits/CharacterPortrait")]
    public class CharacterPortrait : ScriptableObject
    {
        public Sprite Sprite;
    }
}