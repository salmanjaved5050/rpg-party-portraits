using System;
using System.Collections.Generic;
using UnityEngine;

namespace RpgPortraits.Utility
{
    [CreateAssetMenu(fileName = "DockConfigurationListing", menuName = "RpgPortraits/DockConfigurationListing")]
    public class DockConfigurationListing : ScriptableObject
    {
        public List<PortraitDockConfiguration> Configurations;
    }


    [Serializable]
    public class PortraitDockConfiguration
    {
        public PortraitDockLocation PortraitDockLocation;
        
        [Tooltip("Horizontal offset applied relative to anchor.")]
        public float HorizontalDockOffset;
        
        [Tooltip("Vertical offset applied relative to anchor.")]
        public float VerticalDockOffset;
        
        [Tooltip("Spacing between consecutive portraits. In case of left/right docing this will be applied vertically and for bottom, horzintally.")]
        public float SpacingBetweenPortraits;

    }

    public enum PortraitDockLocation
    {
        Left,
        Right,
        Bottom
    }
}