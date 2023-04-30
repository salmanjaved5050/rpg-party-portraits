using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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
        [Tooltip("Location of portraits panel on canvas. It can be on left,right or bottom of the screen.")]
        public PortraitDockLocation PortraitDockLocation;
        
        [Tooltip("Applies adjustment to portraits horzintally i.e. for left, add offset of 2 and for right subtract offset of 20.")]
        public DockAdjustment HorizontalDockAdjustment;
        
        [Tooltip("Applies adjustment to portraits vertically i.e. for bottom subtract offset of 20.")]
        public DockAdjustment VerticalDockAdjustment;
    }

    [Serializable]
    public class DockAdjustment
    {
        public OffsetType OffsetType;
        public OffsetDirection OffsetDirection;
        public float OffsetValue;
    }

    public enum PortraitDockLocation
    {
        Left,
        Right,
        Bottom
    }

    public enum OffsetDirection
    {
        X,
        Y
    }

    public enum OffsetType
    {
        [Tooltip("Absolute means we just add the offset value according to the direction i.e. subtract/add offset from left, right or bottom.")]
        Absolute,
        
        [Tooltip("Calculated means we calculate offset for individual portraits in direction and then add the value provided.")]
        Calculated,
    }
}