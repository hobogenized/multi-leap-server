﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Leap;

namespace LeapmotionIntegration
{
    class ConsolidatedController : Controller
    {
        List<Controller> _listOfControllers;
        List<Frame> _listOfFrames;
        long _numberOfFrames = 0;

		static void Main()
		{
		}

        public ConsolidatedController(List<Controller> listOfControllers)
        {
            _listOfControllers = listOfControllers;
        }

        Hand changeHandID(Hand hand, long frameID, int handID)
        {
            Hand newHand = new Hand(frameID, handID, hand.Confidence, hand.GrabStrength, hand.GrabAngle, hand.PinchStrength, hand.PinchDistance, hand.PalmWidth, hand.IsLeft, hand.TimeVisible, hand.Arm, hand.Fingers, hand.PalmPosition, hand.StabilizedPalmPosition, hand.PalmVelocity, hand.PalmNormal, hand.Direction, hand.WristPosition);
            return newHand;
        }

        // Merge frames from a list of frames
        public Frame mergeFrames(List<Frame> listOfFrames, long frameID)
        {

            List<Hand> listOfHands = new List<Hand>();
            for(int i = 0; i < listOfFrames.Count; i++)
            {
                for(int j = 0; j < listOfFrames[i].Hands.Count; j++)
                {
                    int newHandID = listOfFrames.Count * listOfFrames[i].Hands[j].Id + i;
                    Hand newHand = changeHandID(listOfFrames[i].Hands[j], frameID, newHandID);
                    listOfHands.Add(newHand);
                }
            }

            float minX = 10000000, minY = 10000000, minZ = 10000000, maxX = -1, maxY = -1, maxZ = -1;
            for(int i = 0; i < listOfFrames.Count; i++)
            {
                minX = Math.Min(minX, listOfFrames[i].InteractionBox.Center.x
                                 - listOfFrames[i].InteractionBox.Width / 2);
                minY = Math.Min(minY, listOfFrames[i].InteractionBox.Center.y
                                 - listOfFrames[i].InteractionBox.Height / 2);
                minZ = Math.Min(minZ, listOfFrames[i].InteractionBox.Center.z
                                 - listOfFrames[i].InteractionBox.Depth / 2);
                maxX = Math.Max(maxX, listOfFrames[i].InteractionBox.Center.x
                                 - listOfFrames[i].InteractionBox.Width / 2);
                maxY = Math.Max(maxY, listOfFrames[i].InteractionBox.Center.y
                                 - listOfFrames[i].InteractionBox.Height / 2);
                maxZ = Math.Max(maxZ, listOfFrames[i].InteractionBox.Center.z
                                 - listOfFrames[i].InteractionBox.Depth / 2);
            }

            Vector newSize = new Vector(maxX - minX, maxY - minY, maxZ - minZ);
            InteractionBox newBox = new InteractionBox(listOfFrames[0].InteractionBox.Center, newSize);

            Frame mergedFrame = new Frame(frameID, listOfFrames[0].Timestamp, listOfFrames[0].CurrentFramesPerSecond, newBox, listOfHands);
            return mergedFrame;
        }

        // Need to implement all 18 functions for the controller in consolidated form

        public void ClearPolicy(Controller.PolicyFlag policy)
        {
            return _listOfControllers[0].ClearPolicy(policy);
        }

        public Controller()
        {
            return _listOfControllers[0].Controller();
        }

        public Controller(int connectionKey)
        {
            return _listOfControllers[0].Controller(connectionKey);
        }

        // TODO
        public new FailedDeviceList FailedDevices()
        {
            FailedDeviceList lst = new FailedDeviceList();
            for(int i = 0; i < _listOfControllers.Count; i++)
            {
              lst.Concat(_listOfControllers[i].FailedDevices());
            }
            return lst.ToList();
        }

        public new Frame Frame(int history)
        {
            if (history > 0)
                return _listOfFrames[_listOfFrames.Count - history];
            else
                return this.Frame();
        }

        // TODO
        public void Frame(Frame toFill, int history)
        {
            if (history > 0)
                _listOfFrames[_listOfFrames.Count - history](toFill);
            else
                this.Frame(toFill);
        }

        public new Frame Frame()
        {
            List<Frame> currentFrames = new List<Frame>();
            for(int i = 0; i < _listOfControllers.Count; i++)
            {
                currentFrames.Add(_listOfControllers[i].Frame());
            }
            Frame newFrame = mergeFrames(currentFrames, _numberOfFrames);
            _numberOfFrames += 1;
            _listOfFrames.Add(newFrame);
            return newFrame;
        }

        // how to fill?
        // TODO
        public void Frame(Frame toFill)
        {
            return this.Frame();
        }

        // TODO
        public long FrameTimestamp(int history = 0)
        {
            return _listOfControllers[0].FrameTimestamp(history);
        }

        // TODO
        public new Frame GetInterpolatedFrame(Int64 time)
        {
            return _connection.GetInterpolatedFrame(time);
        }

        // TODO
        public new Frame GetTransformedFrame(LeapTransform trs, int history = 0)
        {
            return this.Frame(history).TransformedCopy(trs);
        }

        public new bool IsPolicySet(Controller.PolicyFlag policy)
        {
            bool isPolicySet = true;
            for(int i = 0; i < _listOfControllers.Count; i++)
            {
                isPolicySet &= _listOfControllers[i].IsPolicySet(policy);
            }

            return isPolicySet;
        }

        public new long Now()
        {
            return _listOfControllers[0].Now();
        }

        // probably need to handle frameID
        // TODO
        public new Image RequestImages(Int64 frameID, Image.ImageType type)
        {
            return _connection.RequestImages(frameID, type);
        }

        // TODO
        public new Image RequestImages(Int64 frameID, Image.ImageType type, byte[] imageBuffer)
        {
            return _connection.RequestImages(frameID, type, imageBuffer);
        }

        public new void SetPolicy(Controller.PolicyFlag policy)
        {
            for(int i = 0; i < _listOfControllers.Count; i++)
            {
                _listOfControllers[i].SetPolicy(policy);
            }
            return;
        }

        public new void StartConnection()
        {
            for(int i = 0; i < _listOfControllers.Count; i++)
            {
                _listOfControllers[i].StartConnection();
            }
            return;
        }
        
        public new void StopConnection()
        {
            for (int i = 0; i < _listOfControllers.Count; i++)
            {
                _listOfControllers[i].StopConnection();
            }
            return;
        }

    }    
}
