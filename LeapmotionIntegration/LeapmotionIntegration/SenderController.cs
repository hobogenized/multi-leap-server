using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Leap;
using System.Xml.Serialization;

namespace LeapmotionIntegration
{

    class SenderController : Controller
    {
        Controller _localController;
        TcpClient conn;
        Stream stream;

        public SenderController(String ip, int port)
        {
            _localController = new Controller();
            conn.Connect(ip, port);
            stream = conn.GetStream();
            StreamReader r = new StreamReader(stream);
            bool running = true;
            while (running)
            {
                String read = r.ReadLine();
                Controller.PolicyFlag p = Controller.PolicyFlag.POLICY_DEFAULT;
                if (read.Contains("Policy"))
                {
                    XmlSerializer mySerializer = new XmlSerializer(typeof(Controller.PolicyFlag));
                    p = (Controller.PolicyFlag)mySerializer.Deserialize(stream);
                }
                switch (read)
                {
                    case "Frame":
                        this.Frame();
                        break;
                    case "IsPolicySet":
                        this.IsPolicySet(p);
                        break;
                    case "SetPolicy":
                        this.SetPolicy(p);
                        break;
                    case "ClearPolicy":
                        this.ClearPolicy(p);
                        break;
                    case "StartConnection":
                        this.StartConnection();
                        break;
                    case "StopConnection":
                        this.StopConnection();
                        break;
                    case "Now":
                        this.Now();
                        break;
                    case "FailedDevices":
                        this.FailedDevices();
                        break;
                    case "END":
                        running = false;
                        break;
                }
            }
            r.Close();
        }

        ~SenderController()
        {
            
            stream.Close();
            conn.Close();
        }

        public void sendMessage(String message)
        {
            try
            {
                StreamWriter w = new StreamWriter(stream, Encoding.UTF8);
                w.WriteLine(message);
                w.Close();
            }
            catch (Exception ex)
            {

            }
        }

        public new Frame Frame()
        {
            var data = _localController.Frame();
            XmlSerializer mySerializer = new XmlSerializer(typeof(Frame));
            StreamWriter myWriter = new StreamWriter(stream);
            mySerializer.Serialize(myWriter, data);
            myWriter.Close();
            return data;
        }

        public void Frame(Frame toFill)
        {
            //Apparently does not exist
            //_localController.Frame(toFill);
        }

        public new Frame Frame(int history)
        {
            var data = _localController.Frame(history);
            XmlSerializer mySerializer = new XmlSerializer(typeof(Frame));
            StreamWriter myWriter = new StreamWriter(stream);
            mySerializer.Serialize(myWriter, data);
            myWriter.Close();
            return data;
        }

        public void Frame(Frame toFill, int history)
        {
            //Apparently does not exist
            //_localController.Frame(toFill, history);
        }

        public long FrameTimestamp(int history = 0)
        {
            return 1L;

            //apparently does not exist
            //return _listOfFrames[_listOfFrames.Count - history - 1].Timestamp;
        }

        public new bool IsPolicySet(Controller.PolicyFlag policy)
        {
            var data = _localController.IsPolicySet(policy);
            sendMessage("" + data);
            return data;
        }

        public new void ClearPolicy(Controller.PolicyFlag policy)
        {
            _localController.ClearPolicy(policy);
        }

        public new void SetPolicy(Controller.PolicyFlag policy)
        {
            _localController.SetPolicy(policy);
        }

        public new void StartConnection()
        {
            _localController.StartConnection();
        }

        public new void StopConnection()
        {
            _localController.StopConnection();
        }

        public new long Now()
        {
            var data = _localController.Now();
            sendMessage("" + data);
            return data;
        }

        public new FailedDeviceList FailedDevices()
        {
            return _localController.FailedDevices();
        }

        // is an invalid image, maybe need to handle it at some point
        public new Image RequestImages(Int64 frameID, Image.ImageType type)
        {
            return _localController.RequestImages(frameID, type);
        }

        // same problem as above
        public new Image RequestImages(Int64 frameID, Image.ImageType type, byte[] imageBuffer)
        {
            return _localController.RequestImages(frameID, type, imageBuffer);
        }

        public new Frame GetTransformedFrame(LeapTransform transform, int history = 0)
        {
            return _localController.GetTransformedFrame(transform, history);
        }

        public new Frame GetInterpolatedFrame(Int64 time)
        {
            return _localController.GetInterpolatedFrame(time);
        }
    }
}
