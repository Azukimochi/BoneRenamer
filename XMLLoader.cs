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
            Hips,
            LeftUpperLeg,
            RightUpperLeg,
            LeftLowerLeg,
            RightLowerLeg,
            LeftFoot,
            RightFoot,
            Spine,
            Chest,
            UpperChest,
            Neck,
            Head,
            LeftShoulder,
            RightShoulder,
            LeftUpperArm,
            RightUpperArm,
            LeftLowerArm,
            RightLowerArm,
            LeftHand,
            RightHand,
            LeftToes,
            RightToes,
            LeftEye,
            RightEye,
            Jaw,
            LeftThumbProximal,
            LeftThumbIntermediate,
            LeftThumbDistal,
            LeftIndexProximal,
            LeftIndexIntermediate,
            LeftIndexDistal,
            LeftMiddleProximal,
            LeftMiddleIntermediate,
            LeftMiddleDistal,
            LeftRingProximal,
            LeftRingIntermediate,
            LeftRingDistal,
            LeftLittleProximal,
            LeftLittleIntermediate,
            LeftLittleDistal,
            RightThumbProximal,
            RightThumbIntermediate,
            RightThumbDistal,
            RightIndexProximal,
            RightIndexIntermediate,
            RightIndexDistal,
            RightMiddleProximal,
            RightMiddleIntermediate,
            RightMiddleDistal,
            RightRingProximal,
            RightRingIntermediate,
            RightRingDistal,
            RightLittleProximal,
            RightLittleIntermediate,
            RightLittleDistal,
            LastBone,
            RightBreastRoot,
            RightBreastEnd,
            LeftBreastRoot,
            LeftBreastEnd
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

