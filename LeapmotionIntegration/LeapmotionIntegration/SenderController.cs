using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
//using System.Threading.Tasks;
using Leap;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
namespace LeapmotionIntegration
{
    class SenderController : Controller
    {
        Controller _localController;
        TcpListener l;
        TcpClient client;
        NetworkStream receiveStream;
        StreamReader r;
        StreamWriter w;
        IFormatter binFormatter;
        public SenderController()
        {
            _localController = new Controller();
            l = new TcpListener(IPAddress.Any, 80);
            l.Start();
            Console.WriteLine("Waiting for client...");
            client = l.AcceptTcpClient();
            Console.WriteLine("Accepted client.");
            receiveStream = client.GetStream();
            r = new StreamReader(receiveStream);
            w = new StreamWriter(receiveStream, Encoding.UTF8);
            binFormatter = new BinaryFormatter();
            if (!receiveStream.CanRead)
            {
                Console.WriteLine("Can't read");
            }
            if (!receiveStream.CanWrite)
            {
                Console.WriteLine("Can't write");
            }
            l.Stop();
        }
        ~SenderController()
        {
            w.Close();
            r.Close();
            receiveStream.Close();
            client.Close();
        }
        public void Run()
        {
            bool running = true;
            while (running)
            {
                if (r.EndOfStream) continue;
                String read = r.ReadLine();
                Console.WriteLine("Read a line: " + read);
                Controller.PolicyFlag p = Controller.PolicyFlag.POLICY_DEFAULT;
                if (read.Contains("Policy"))
                {
                    p = (Controller.PolicyFlag)binFormatter.Deserialize(receiveStream);
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
        }
        public void sendMessage(String message)
        {
            try
            {
                w.WriteLine(message);
                w.Flush();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public new Frame Frame()
        {
            var data = _localController.Frame();
            var sendData = new SerializableFrame(data);
            Console.WriteLine("Begin serialize");
            binFormatter.Serialize(receiveStream, sendData);
            Console.WriteLine("Serialized");
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
            var sendData = new SerializableFrame(data);
            binFormatter.Serialize(receiveStream, sendData);
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