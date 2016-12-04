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

        ConsolidatedController(List<Controller> listOfControllers)
        {
            _listOfControllers = listOfControllers;
        }

        //public Arm translateArm(Arm arm, Vector translation)
        //{

        //}

        //public Finger translateFinger(Finger finger, Vector translation)
        //{

        //}

        //public Hand translateHand(Hand hand, Vector translation)
        //{

        //}

        //public List<List<Vector>> getPalmCoordinates(List<Frame> listOfFrames, List<Vector> centersOfLeapmotion)
        //{
        //    List<List<Vector>> palmCoordinates = new List<List<Vector>>();

        //    int numberOfLeaps = centersOfLeapmotion.Count;

        //    for (int leapNumber = 0; leapNumber < numberOfLeaps; leapNumber++)
        //    {
        //        palmCoordinates.Add(new List<Vector>());

        //        List<Hand> hands = listOfFrames[leapNumber].Hands;
        //        int numberOfHands = hands.Count;

        //        for (int handNumber = 0; handNumber < numberOfHands; handNumber++)
        //        {
        //            Vector coordinatesOfPalm = listOfFrames[leapNumber].
        //                                                        InteractionBox.
        //                                                        DenormalizePoint(
        //                                                            hands[handNumber].StabilizedPalmPosition);
        //            palmCoordinates[leapNumber].Add(centersOfLeapmotion[leapNumber] +
        //                                                        coordinatesOfPalm
        //                                                        );
        //        }
        //    }
        //    return palmCoordinates;
        //}

        //public List<List<Vector>> getWristCoordinates(List<Frame> listOfFrames, List<Vector> centersOfLeapmotion)
        //{
        //    List<List<Vector>> wristCoordinates = new List<List<Vector>>();

        //    int numberOfLeaps = centersOfLeapmotion.Count;

        //    for (int leapNumber = 0; leapNumber < numberOfLeaps; leapNumber++)
        //    {
        //        wristCoordinates.Add(new List<Vector>());

        //        List<Hand> hands = listOfFrames[leapNumber].Hands;
        //        int numberOfHands = hands.Count;

        //        for (int handNumber = 0; handNumber < numberOfHands; handNumber++)
        //        {
        //            Vector coordinatesOfWrist = listOfFrames[leapNumber].
        //                                                        InteractionBox.
        //                                                        DenormalizePoint(
        //                                                            hands[handNumber].WristPosition);

        //            wristCoordinates[leapNumber].Add(centersOfLeapmotion[leapNumber] +
        //                                                        coordinatesOfWrist
        //                                                        );
        //        }
        //    }
        //    return wristCoordinates;
        //}

        //// Gives back a single frame which integrates all the frames in the list
        //public void integrate(List<Frame> listOfFrames, List<Vector> centersOfLeapmotion)
        //{
        //    // All frames need to be the same FPS
        //    float currentFPS = listOfFrames[0].CurrentFramesPerSecond;

        //    // I am assuming that the frames are from appriximately the same time
        //    // This can be changed later
        //    long timeStamp = listOfFrames[0].Timestamp;

        //    // We now are working to get all hands from different frames and remove duplicates

        //    // This will store the coordinates of all the hands relative to a global center
        //    List<List<Vector>> palmCoordinates = getPalmCoordinates(listOfFrames, centersOfLeapmotion);
        //    List<List<Vector>> wristCoordinates = getWristCoordinates(listOfFrames, centersOfLeapmotion);

        //    // Closeness Parameter
        //    float epsilon = 1;

        //    List<Hand> newHands = new List<Hand>();
        //    for (int leapNumber = 0; leapNumber < numberOfLeaps; leapNumber++)
        //    {
        //        for (int handNumber = 0; handNumber < palmCoordinates[leapNumber].Count; handNumber++)
        //        {
        //            bool alreadyPresent = false;

        //            Hand thisHand = listOfFrames[leapNumber].Hands[handNumber];

        //            for (int i = 0; i < newHands.Count; i++)
        //            {
        //                if(palmCoordinates[leapNumber][handNumber].DistanceTo(newHands[i].StabilizedPalmPosition) < epsilon)
        //                {
        //                    alreadyPresent = true;
        //                    break;
        //                }
        //            }

        //            if (!alreadyPresent)
        //            {
        //                newHands.Add(translateHand(thisHand, centersOfLeapmotion[leapNumber]));
        //            }
        //        }

        //    }

        //    // The new ID for the frame is going to be 1
        //    // DEFINITLY NEED TO CHANGE THIS
        //    long IDofFrame = 1;

        //    // Need to make a new InteracionBox
        //    InteractionBox integratedInteractionBox;
        //}

        // Need to implement all 18 functions for the controller in consolidated form
        ConsolidatedController()
        {
            return;
        }

        ConsolidatedController(int connectionKey)
        {
            return;
        }

        FailedDeviceList FailedDevices()
        {

        }
    }    
}
