using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Leap;
using System.IO;
using System.Xml.Serialization;

namespace LeapmotionIntegration
{
    class ReceiverController : Controller
    {
        TcpClient conn;
        Stream stream;
        StreamWriter w;
        StreamReader r;

        public ReceiverController(String ip, int port)
        {
            conn = new TcpClient();
            conn.ReceiveTimeout = 100;
            conn.SendTimeout = 100;
            conn.Connect(ip, port);
            stream = conn.GetStream();
            w = new StreamWriter(stream, Encoding.UTF8);
            r = new StreamReader(stream);
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
            XmlSerializer mySerializer = new XmlSerializer(typeof(Frame));
            return (Frame)mySerializer.Deserialize(stream);
        }

        public new bool IsPolicySet(Controller.PolicyFlag policy)
        {
            sendMessage("IsPolicySet");
              
            XmlSerializer mySerializer = new XmlSerializer(typeof(Controller.PolicyFlag));
            
            mySerializer.Serialize(w, policy);
            return Boolean.Parse(r.ReadLine());
        }

        public new void SetPolicy(Controller.PolicyFlag policy)
        {
            sendMessage("SetPolicy");
             
            XmlSerializer mySerializer = new XmlSerializer(typeof(Controller.PolicyFlag));
            mySerializer.Serialize(w, policy);
        }

        public new void ClearPolicy(Controller.PolicyFlag policy)
        {
            sendMessage("ClearPolicy");
        
            XmlSerializer mySerializer = new XmlSerializer(typeof(Controller.PolicyFlag));
            mySerializer.Serialize(w, policy);
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
