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
        public Frame toFrame()
        {
            List<Hand> h = new List<Hand>();
            foreach (SerializableHand sh in hands)
            {
                h.Add(sh.toHand());
            }
            return new Frame(
                id,
                timestamp,
                fps,
                interactionBox.toInteractionBox(),
                h
            );
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

        public InteractionBox toInteractionBox()
        {
            return new InteractionBox(
                center.toVector(),
                size.toVector()
                );
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

        public long frameId;
        public int id;
        public float confidence;
        public float grabStrength;
        public float grabAngle;
        public float pinchStrength;
        public float pinchDistance;
        public float palmWidth;
        public bool isLeft;
        public float timeVisible;
        public SerializableArm arm;

        public SerializableHand(Hand h)
        {
            frameId = h.FrameId;
            id = h.Id;
            confidence = h.Confidence;
            grabStrength = h.GrabStrength;
            grabAngle = h.GrabAngle;
            pinchStrength = h.PinchStrength;
            pinchDistance = h.PinchDistance;
            palmWidth = h.PalmWidth;
            isLeft = h.IsLeft;
            timeVisible = h.TimeVisible;
            arm = new SerializableArm(h.Arm);
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

        public Hand toHand()
        {
            List<Finger> f = new List<Finger>();
            foreach (SerializableFinger sf in Fingers)
            {
                f.Add(sf.toFinger());
            }
            return new Hand(
                frameId,
                id,
                confidence,
                grabStrength,
                grabAngle,
                pinchStrength,
                pinchDistance,
                palmWidth,
                isLeft,
                timeVisible,
                arm.toArm(),
                f,
                PalmPosition.toVector(),
                StabilizedPalmPosition.toVector(),
                PalmVelocity.toVector(),
                PalmNormal.toVector(),
                Direction.toVector(),
                WristPosition.toVector()
                );
        }
    }
    [Serializable]
    public class SerializableFinger
    {
        public Finger.FingerType Type;
        public SerializableBone[] _bones;
        public long _frameId;
        public int Id;
        public int HandId;
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

        public Finger toFinger()
        {
            Bone[] b = new Bone[4];
            for (int i = 0; i < _bones.Length; ++i)
            {
                b[i] = _bones[i].toBone();
            }
            return new Finger(
                _frameId,
                HandId,
                Id,
                TimeVisible,
                TipPosition.toVector(),
                TipVelocity.toVector(),
                Direction.toVector(),
                StabilizedTipPosition.toVector(),
                Width,
                Length,
                IsExtended,
                Type,
                b[0],
                b[1],
                b[2],
                b[3]);
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

        public Vector toVector()
        {
            return new Vector(x, y, z);
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

        public LeapQuaternion toLeapQuaternion()
        {
            return new LeapQuaternion(x, y, z, w);
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

        public Bone toBone()
        {
            return new Bone(
                prevJoint.toVector(),
                nextJoint.toVector(),
                center.toVector(),
                direction.toVector(),
                length,
                width,
                type,
                rotation.toLeapQuaternion()
                );
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

        public Arm toArm()
        {
            return new Arm(
                prevJoint.toVector(),
                nextJoint.toVector(),
                center.toVector(),
                direction.toVector(),
                length,
                width,
                rotation.toLeapQuaternion()
                );
        }
    }
}