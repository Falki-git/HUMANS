using KSP.Sim;
using UnityEngine;

namespace Humans
{
    public class HumanParameters<T>
    {
        public string Key { get; set; }
        public T Value { get; set; }
        public Type ValueType { get; set; }
        public string AttachToName { get; set; }
    }

    public class Name : HumanParameters<string>
    {
        public Name()
        {
            Key = "NAME";
            ValueType = typeof(string);
            AttachToName = null;
        }
    }

    public class Surname : HumanParameters<string>
    {
        public Surname()
        {
            Key = "SURNAME";
            ValueType = typeof(string);
            AttachToName = null;
        }
    }

    public class HumanType : HumanParameters<KerbalType>
    {
        public HumanType()
        {
            Key = "TYPE";
            ValueType = typeof(KerbalType);
            AttachToName = null;
        }
    }

    public class HairStyle : HumanParameters<string>
    {
        public HairStyle()
        {
            Key = "HAIRSTYLE";
            ValueType = typeof(GameObject);
            AttachToName = "bone_kerbal_head";
        }
    }

    public class Helmet : HumanParameters<string>
    {
        public Helmet()
        {
            Key = "HELMET";
            ValueType = typeof(GameObject);
            AttachToName = "bone_kerbal_helmet";
        }
    }

    public class HairColor : HumanParameters<Color>
    {
        public HairColor()
        {
            Key = "HAIRCOLOR";
            ValueType = typeof(Color);
            AttachToName = null;
        }
    }

    public class Eyes : HumanParameters<string>
    {
        public Eyes()
        {
            Key = "EYES";
            ValueType = typeof(GameObject);
            AttachToName = null;
        }
    }

    public class EyeHeight : HumanParameters<Single>
    {
        public EyeHeight()
        {
            Key = "EYEHEIGHT";
            ValueType = typeof(Single);
            AttachToName = null;
        }
    }

    public class EyeSymmetry : HumanParameters<Single>
    {
        public EyeSymmetry()
        {
            Key = "EYESYMMETRY";
            ValueType = typeof(Single);
            AttachToName = null;
        }
    }

    public class SkinColor : HumanParameters<Color>
    {
        public SkinColor()
        {
            Key = "SKINCOLOR";
            ValueType = typeof(Color);
            AttachToName = null;
        }
    }

    public class FacialHair : HumanParameters<string>
    {
        public FacialHair()
        {
            Key = "FACIALHAIR";
            ValueType = typeof(GameObject);
            AttachToName = null;
        }
    }

    public class FaceDecoration : HumanParameters<string>
    {
        public FaceDecoration()
        {
            Key = "FACEDECORATION";
            ValueType = typeof(GameObject);
            AttachToName = "bone_kerbal_head";
        }
    }

    public class TeamColor1 : HumanParameters<Color>
    {
        public TeamColor1()
        {
            Key = "TEAMCOLOR1";
            ValueType = typeof(Color);
            AttachToName = null;
        }
    }

    public class TeamColor2 : HumanParameters<Color>
    {
        public TeamColor2()
        {
            Key = "TEAMCOLOR2";
            ValueType = typeof(Color);
            AttachToName = null;
        }
    }

    public class Stupidity : HumanParameters<Single>
    {
        public Stupidity()
        {
            Key = "STUPIDITY";
            ValueType = typeof(Single);
            AttachToName = null;
        }
    }

    public class Bravery : HumanParameters<Single>
    {
        public Bravery()
        {
            Key = "BRAVERY";
            ValueType = typeof(Single);
            AttachToName = null;
        }
    }

    public class Constitution : HumanParameters<Single>
    {
        public Constitution()
        {
            Key = "CONSTITUTION";
            ValueType = typeof(Single);
            AttachToName = null;
        }
    }

    public class Optimism : HumanParameters<Single>
    {
        public Optimism()
        {
            Key = "OPTIMISM";
            ValueType = typeof(Single);
            AttachToName = null;
        }
    }

    public class IsVeteran : HumanParameters<bool>
    {
        public IsVeteran()
        {
            Key = "ISVETERAN";
            ValueType = typeof(bool);
            AttachToName = null;
        }
    }

    public class VoiceSelection : HumanParameters<Int32>
    {
        public VoiceSelection()
        {
            Key = "VOICESELECTION";
            ValueType = typeof(Int32);
            AttachToName = null;
        }
    }

    public class VoiceType : HumanParameters<Single>
    {
        public VoiceType()
        {
            Key = "VOICETYPE";
            ValueType = typeof(Single);
            AttachToName = null;
        }
    }

    public class Body : HumanParameters<string>
    {
        public Body()
        {
            Key = "BODY";
            ValueType = typeof(GameObject);
            AttachToName = null;
        }
    }

    public class Head : HumanParameters<string>
    {
        public Head()
        {
            Key = "HEAD";
            ValueType = typeof(GameObject);
            AttachToName = null;
        }
    }

    public class FacePaint : HumanParameters<string>
    {
        public FacePaint()
        {
            Key = "FACEPAINT";
            ValueType = typeof(string);
            AttachToName = null;
        }
    }

    public class Radiation : HumanParameters<Single>
    {
        public Radiation()
        {
            Key = "RADIATION";
            ValueType = typeof(Single);
            AttachToName = null;
        }
    }

    public class Happiness : HumanParameters<Single>
    {
        public Happiness()
        {
            Key = "HAPPINESS";
            ValueType = typeof(Single);
            AttachToName = null;
        }
    }

    public class Experience : HumanParameters<Int32>
    {
        public Experience()
        {
            Key = "EXPERIENCE";
            ValueType = typeof(Int32);
            AttachToName = null;
        }
    }

    public class Biography : HumanParameters<string>
    {
        public Biography()
        {
            Key = "BIOGRAPHY";
            ValueType = typeof(string);
            AttachToName = null;
        }
    }


}
