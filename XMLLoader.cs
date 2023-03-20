using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;

namespace AvaterInfo
{
    public class XMLLoader
    {
        public enum keys
        {
            NAME,
            ARMATURE,
            HIPS,
            SPINE,
            CHEST,
            NECK,
            HEAD,
            //左肩
            SHOULDER_L,
            UPPER_ARM_L,
            LOWER_ARM_L,
            HAND_L,
            FINGER_INDEX_L,
            FINGER_LITTLE_L,
            FINGER_MIDDLE_L,
            FINGER_RING_L,
            FINGER_THUMB_L,
            //右肩
            SHOULDER_R,
            UPPER_ARM_R,
            LOWER_ARM_R,
            HAND_R,
            FINGER_INDEX_R,
            FINGER_LITTLE_R,
            FINGER_MIDDLE_R,
            FINGER_RING_R,
            FINGER_THUMB_LR,
            //左足
            UPPER_LEG_L,
            LOWER_LEG_L,
            FOOT_L,
            TOE_L,
            //右足
            UPPER_LEG_R,
            LOWER_LEG_R,
            FOOT_R,
            TOE_R,
        }
        static XElement xml;
        static IEnumerable<XElement> elements;

        public XMLLoader ()
        {
            var path = AssetDatabase.GUIDToAssetPath("47081239d4c81964ea0b40ebe97514ee");
            xml = XElement.Load(path);
            elements = from item in xml.Elements("avater")
                                             select item;
        }
        public string[] GetAvaterNames()
        {
            IEnumerable<string> infos = from item in elements.Elements(keys.NAME.ToString())
                                          select item.Value;
            return infos.ToArray();
        }
        public string[] GetBoneNameFromKey(keys key)
        {
            IEnumerable<string> infos = from item in elements.Elements(key.ToString())
                                        select item.Value;
            return infos.ToArray();
        }
    }
    class BoneInfo
    {
        public string avaterName { set; get; }

    }
}

