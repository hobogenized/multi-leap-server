using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Leap
{
    [Serializable]
    public class SerializableFrame
    {
        public long id;
        public long timestamp;
        public float fps;
        public SerializableInteractionBox interactionBox;
        public List<SerializableHand> hands;
        public SerializableFrame(Frame f)
        {
            id = f.Id;
            timestamp = f.Timestamp;
            fps = f.CurrentFramesPerSecond;
            interactionBox = new SerializableInteractionBox(f.InteractionBox);
            hands = new List<SerializableHand>();
            foreach (Hand h in f.Hands)
            {
                hands.Add(new SerializableHand(h));
            }
        }
    }
    [Serializable]
    public class SerializableInteractionBox
    {
        SerializableVector center;
        SerializableVector size;
        public SerializableInteractionBox(InteractionBox b)
        {
            center = new SerializableVector(b.Center);
            size = new SerializableVector(b.Size);
        }
    }
    [Serializable]
    public class SerializableHand
    {
        public SerializableVector PalmPosition;
        public SerializableVector StabilizedPalmPosition;
        public SerializableVector PalmVelocity;
        public SerializableVector PalmNormal;
        public SerializableVector Direction;
        public SerializableVector WristPosition;
        public List<SerializableFinger> Fingers;
        public SerializableHand(Hand h)
        {
            PalmPosition = new SerializableVector(h.PalmPosition);
            StabilizedPalmPosition = new SerializableVector(h.StabilizedPalmPosition);
            PalmVelocity = new SerializableVector(h.PalmVelocity);
            PalmNormal = new SerializableVector(h.PalmNormal);
            Direction = new SerializableVector(h.Direction);
            WristPosition = new SerializableVector(h.WristPosition);
            Fingers = new List<SerializableFinger>();
            foreach (Finger f in h.Fingers)
            {
                Fingers.Add(new Leap.SerializableFinger(f));
            }
        }
    }
    [Serializable]
    public class SerializableFinger
    {
        public Finger.FingerType Type;
        public SerializableBone[] _bones;
        public long _frameId;
        public long Id;
        public long HandId;
        public SerializableVector TipPosition;
        public SerializableVector TipVelocity;
        public SerializableVector Direction;
        public float Width;
        public float Length;
        public bool IsExtended;
        public SerializableVector StabilizedTipPosition;
        public float TimeVisible;
        public SerializableFinger(Finger f)
        {
            Type = f.Type;
            _bones = new SerializableBone[4];
            for (int i = 0; i < 4; ++i)
            {
                _bones[i] = new Leap.SerializableBone(f.Bone((Bone.BoneType)i));
            }
            _frameId = 0; //Private
            Id = f.Id;
            HandId = f.HandId;
            TipPosition = new SerializableVector(f.TipPosition);
            TipVelocity = new SerializableVector(f.TipVelocity);
            Direction = new SerializableVector(f.Direction);
            Width = f.Width;
            Length = f.Length;
            IsExtended = f.IsExtended;
            StabilizedTipPosition = new SerializableVector(f.StabilizedTipPosition);
            TimeVisible = f.TimeVisible;

        }
    }
    [Serializable]
    public class SerializableVector
    {
        public float x;
        public float y;
        public float z;
        public SerializableVector(Vector v)
        {
            x = v.x;
            y = v.y;
            z = v.z;
        }
    }
    [Serializable]
    public class SerializableLeapQuaternion
    {
        public float x;
        public float y;
        public float z;
        public float w;
        public SerializableLeapQuaternion(LeapQuaternion q)
        {
            x = q.x;
            y = q.y;
            z = q.z;
            w = q.w;
        }
    }
    [Serializable]
    public class SerializableBone
    {
        SerializableVector prevJoint;
        SerializableVector nextJoint;
        SerializableVector center;
        SerializableVector direction;
        float length;
        float width;
        Bone.BoneType type;
        SerializableLeapQuaternion rotation;
        public SerializableBone(Bone b)
        {
            prevJoint = new SerializableVector(b.PrevJoint);
            nextJoint = new SerializableVector(b.NextJoint);
            center = new SerializableVector(b.Center);
            direction = new SerializableVector(b.Direction);
            length = b.Length;
            width = b.Width;
            type = b.Type;
            rotation = new SerializableLeapQuaternion(b.Rotation);
        }
    }
    [Serializable]
    public class SerializableArm
    {
        SerializableVector prevJoint;
        SerializableVector nextJoint;
        SerializableVector center;
        SerializableVector direction;
        float length;
        float width;
        Bone.BoneType type;
        SerializableLeapQuaternion rotation;
        public SerializableArm(Arm b)
        {
            prevJoint = new SerializableVector(b.PrevJoint);
            nextJoint = new SerializableVector(b.NextJoint);
            center = new SerializableVector(b.Center);
            direction = new SerializableVector(b.Direction);
            length = b.Length;
            width = b.Width;
            type = b.Type;
            rotation = new SerializableLeapQuaternion(b.Rotation);
        }
    }
}