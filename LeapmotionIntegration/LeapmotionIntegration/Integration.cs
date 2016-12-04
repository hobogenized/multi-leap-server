using System;
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

        public ConsolidatedController(List<Controller> listOfControllers)
        {
            _listOfControllers = listOfControllers;
        }

        // Merge frames from a list of frames
        public Frame mergeFrames(List<Frame> listOfFrames)
        {
            Frame mergedFrame = new Frame();
            return mergedFrame;
        }

        // Need to implement all 18 functions for the controller in consolidated form
        public ConsolidatedController()
        {
            return;
        }

        public ConsolidatedController(int connectionKey)
        {
            return;
        }

        // What to do with this?
        // this don't work yo
        public new FailedDeviceList FailedDevices()
        {
            FailedDeviceList failedList = new FailedDeviceList();
            return failedList;
        }

        public new Frame Frame(int history)
        {
            List<Frame> listOfFrames = new List<Frame>();

            return mergeFrames(listOfFrames);
        }

        public void Frame(Frame toFill, int history)
        {
            return;
        }

        public new Frame Frame()
        {
            Frame newFrame = new Frame();
            return newFrame;
        }

        public void Frame(Frame toFill)
        {
            return;
        }

        public long FrameTimestamp(int history = 0)
        {
            long timeStamp = 0;
            return timeStamp;
        }

        public new Frame GetInterpolatedFrame(Int64 time)
        {
            Frame interpolatedFrame = new Leap.Frame();
            return interpolatedFrame;
        }

        public new Frame GetTransformedFrame(LeapTransform trs, int history = 0)
        {
            Frame transformedFrame = new Leap.Frame();
            return transformedFrame;
        }

        public new bool IsPolicySet(Controller.PolicyFlag policy)
        {
            bool isPolicySet = true;

            return isPolicySet;
        }

        public new long Now()
        {
            long currentTimeStamp = 0;

            return currentTimeStamp;
        }

        public new Image RequestImages(Int64 frameId, Image.ImageType type)
        {
            Image requestedImage = new Image();

            return requestedImage;
        }

        public new Image RequestImages(Int64 frameId, Image.ImageType type, byte[] imageBuffer)
        {
            Image requestedImage = new Image();

            return requestedImage;
        }

        public new void SetPolicy(Controller.PolicyFlag policy)
        {
            return;
        }

        public new void StartConnection()
        {
            return;
        }

        public new void StopConnection()
        {
            return;
        }

    }    
}
