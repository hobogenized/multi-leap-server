using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Leap;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace LeapmotionIntegration
{
    class ReceiverController : Controller
    {
        TcpClient conn;
        Stream stream;
        StreamWriter w;
        StreamReader r;
        IFormatter binFormatter;

        public ReceiverController(String ip, int port)
        {
            conn = new TcpClient();
            conn.ReceiveTimeout = 100;
            conn.SendTimeout = 100;
            conn.Connect(ip, port);
            stream = conn.GetStream();
            w = new StreamWriter(stream, Encoding.UTF8);
            r = new StreamReader(stream);
            binFormatter = new BinaryFormatter();
        }

        ~ReceiverController()
        {
            w.Close();
            r.Close();
            stream.Close();
            conn.Close();
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

        //TcpClient 
        public new Frame Frame()
        {
            sendMessage("Frame");
            SerializableFrame sf = (SerializableFrame)binFormatter.Deserialize(stream);
            List<Hand> h = new List<Hand>();
            foreach (SerializableHand sh in sf.hands)
            {
                h.Add(sh.toHand());
            }
            Frame f = new Frame(sf.id,
                sf.timestamp,
                sf.fps,
                sf.interactionBox.toInteractionBox(),
                h);
            return f;
        }

        public new bool IsPolicySet(Controller.PolicyFlag policy)
        {
            sendMessage("IsPolicySet");
            binFormatter.Serialize(stream, policy);
            return Boolean.Parse(r.ReadLine());
        }

        public new void SetPolicy(Controller.PolicyFlag policy)
        {
            sendMessage("SetPolicy");
            binFormatter.Serialize(stream, policy);
        }

        public new void ClearPolicy(Controller.PolicyFlag policy)
        {
            sendMessage("ClearPolicy");
            binFormatter.Serialize(stream, policy);
        }

        public new void StartConnection()
        {
            sendMessage("StartConnection");
        }

        public new void StopConnection()
        {
            sendMessage("StopConnection");
        }

        public new long Now()
        {
            sendMessage("Now");
            return Convert.ToInt64(r.ReadLine());
        }
    }
}
