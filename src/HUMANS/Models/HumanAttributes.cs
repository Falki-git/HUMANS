using KSP.Game;
using KSP.Sim;
using UnityEngine;

namespace Humans
{
    public class HumanAttribute<T>
    {
        public string Key { get; set; }
        public T Value { get; set; }
        public Type ValueType { get; set; }
        public string AttachToName { get; set; }
        public VarietyPreloadInfo Variety => new VarietyPreloadInfo(Value, ValueType, AttachToName);
        public void ApplyAttribute(KerbalInfo kerbal, T value)
        {
            if (value == null || value.ToString().ToUpperInvariant().Contains("EMPTY"))
                return;

            Value = value;
            kerbal.Attributes.SetAttribute(Key, Variety);
        }
    }

    public class FirstNameAttribute : HumanAttribute<string>
    {
        public FirstNameAttribute()
        {
            Key = "NAME";
            ValueType = typeof(string);
            AttachToName = null;
        }
    }

    public class SurnameAttribute : HumanAttribute<string>
    {
        public SurnameAttribute()
        {
            Key = "SURNAME";
            ValueType = typeof(string);
            AttachToName = null;
        }
    }

    public class KerbalTypeAttribute : HumanAttribute<KerbalType>
    {
        public KerbalTypeAttribute()
        {
            Key = "TYPE";
            ValueType = typeof(KerbalType);
            AttachToName = null;
        }
    }

    public class HairStyleAttribute : HumanAttribute<string>
    {
        public HairStyleAttribute()
        {
            Key = "HAIRSTYLE";
            ValueType = typeof(GameObject);
            AttachToName = "bone_kerbal_head";
        }
    }

    public class HelmetAttribute : HumanAttribute<string>
    {
        public HelmetAttribute()
        {
            Key = "HELMET";
            ValueType = typeof(GameObject);
            AttachToName = "bone_kerbal_helmet";
        }
    }

    public class HairColorAttribute : HumanAttribute<Color>
    {
        public HairColorAttribute()
        {
            Key = "HAIRCOLOR";
            ValueType = typeof(Color);
            AttachToName = null;
        }
    }

    public class EyesAttribute : HumanAttribute<string>
    {
        public EyesAttribute()
        {
            Key = "EYES";
            ValueType = typeof(GameObject);
            AttachToName = null;
        }
    }

    public class EyeHeightAttribute : HumanAttribute<Single>
    {
        public EyeHeightAttribute()
        {
            Key = "EYEHEIGHT";
            ValueType = typeof(Single);
            AttachToName = null;
        }
    }

    public class EyeSymmetryAttribute : HumanAttribute<Single>
    {
        public EyeSymmetryAttribute()
        {
            Key = "EYESYMMETRY";
            ValueType = typeof(Single);
            AttachToName = null;
        }
    }

    public class SkinColorAttribute : HumanAttribute<Color>
    {
        public SkinColorAttribute()
        {
            Key = "SKINCOLOR";
            ValueType = typeof(Color);
            AttachToName = null;
        }
    }

    public class FacialHairAttribute : HumanAttribute<string>
    {
        public FacialHairAttribute()
        {
            Key = "FACIALHAIR";
            ValueType = typeof(GameObject);
            AttachToName = null;
        }
    }

    public class FaceDecorationAttribute : HumanAttribute<string>
    {
        public FaceDecorationAttribute()
        {
            Key = "FACEDECORATION";
            ValueType = typeof(GameObject);
            AttachToName = "bone_kerbal_head";
        }
    }

    public class TeamColor1Attribute : HumanAttribute<Color>
    {
        public TeamColor1Attribute()
        {
            Key = "TEAMCOLOR1";
            ValueType = typeof(Color);
            AttachToName = null;
        }
    }

    public class TeamColor2Attribute : HumanAttribute<Color>
    {
        public TeamColor2Attribute()
        {
            Key = "TEAMCOLOR2";
            ValueType = typeof(Color);
            AttachToName = null;
        }
    }

    public class StupidityAttribute : HumanAttribute<Single>
    {
        public StupidityAttribute()
        {
            Key = "STUPIDITY";
            ValueType = typeof(Single);
            AttachToName = null;
        }
    }

    public class BraveryAttribute : HumanAttribute<Single>
    {
        public BraveryAttribute()
        {
            Key = "BRAVERY";
            ValueType = typeof(Single);
            AttachToName = null;
        }
    }

    public class ConstitutionAttribute : HumanAttribute<Single>
    {
        public ConstitutionAttribute()
        {
            Key = "CONSTITUTION";
            ValueType = typeof(Single);
            AttachToName = null;
        }
    }

    public class OptimismAttribute : HumanAttribute<Single>
    {
        public OptimismAttribute()
        {
            Key = "OPTIMISM";
            ValueType = typeof(Single);
            AttachToName = null;
        }
    }

    public class IsVeteranAttribute : HumanAttribute<bool>
    {
        public IsVeteranAttribute()
        {
            Key = "ISVETERAN";
            ValueType = typeof(bool);
            AttachToName = null;
        }
    }

    public class VoiceSelectionAttribute : HumanAttribute<Int32>
    {
        public VoiceSelectionAttribute()
        {
            Key = "VOICESELECTION";
            ValueType = typeof(Int32);
            AttachToName = null;
        }
    }

    public class VoiceTypeAttribute : HumanAttribute<Single>
    {
        public VoiceTypeAttribute()
        {
            Key = "VOICETYPE";
            ValueType = typeof(Single);
            AttachToName = null;
        }
    }

    public class BodyAttribute : HumanAttribute<string>
    {
        public BodyAttribute()
        {
            Key = "BODY";
            ValueType = typeof(GameObject);
            AttachToName = null;
        }
    }

    public class HeadAttribute : HumanAttribute<string>
    {
        public HeadAttribute()
        {
            Key = "HEAD";
            ValueType = typeof(GameObject);
            AttachToName = null;
        }
    }

    public class FacePaintAttribute : HumanAttribute<string>
    {
        public FacePaintAttribute()
        {
            Key = "FACEPAINT";
            ValueType = typeof(string);
            AttachToName = null;
        }
    }

    public class RadiationAttribute : HumanAttribute<Single>
    {
        public RadiationAttribute()
        {
            Key = "RADIATION";
            ValueType = typeof(Single);
            AttachToName = null;
        }
    }

    public class HappinessAttribute : HumanAttribute<Single>
    {
        public HappinessAttribute()
        {
            Key = "HAPPINESS";
            ValueType = typeof(Single);
            AttachToName = null;
        }
    }

    public class ExperienceAttribute : HumanAttribute<Int32>
    {
        public ExperienceAttribute()
        {
            Key = "EXPERIENCE";
            ValueType = typeof(Int32);
            AttachToName = null;
        }
    }

    public class BiographyAttribute : HumanAttribute<string>
    {
        public BiographyAttribute()
        {
            Key = "BIOGRAPHY";
            ValueType = typeof(string);
            AttachToName = null;
        }
    }
}
