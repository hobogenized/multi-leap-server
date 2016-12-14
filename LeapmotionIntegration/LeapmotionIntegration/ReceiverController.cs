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

        public ReceiverController(String ip, int port)
        {
            conn.Connect(ip, port);
            stream = conn.GetStream();
        }

        ~ReceiverController()
        {
            conn.Close();
            stream.Close();
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
            StreamWriter myWriter = new StreamWriter(stream);
            mySerializer.Serialize(myWriter, policy);
            myWriter.Close();
            StreamReader r = new StreamReader(stream);
            return Boolean.Parse(r.ReadLine());
        }

        public new void SetPolicy(Controller.PolicyFlag policy)
        {
            sendMessage("SetPolicy");
             
            XmlSerializer mySerializer = new XmlSerializer(typeof(Controller.PolicyFlag));
            StreamWriter myWriter = new StreamWriter(stream);
            mySerializer.Serialize(myWriter, policy);
            myWriter.Close();
        }

        public new void ClearPolicy(Controller.PolicyFlag policy)
        {
            sendMessage("ClearPolicy");
        
            XmlSerializer mySerializer = new XmlSerializer(typeof(Controller.PolicyFlag));
            StreamWriter myWriter = new StreamWriter(stream);
            mySerializer.Serialize(myWriter, policy);
            myWriter.Close();
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
            StreamReader r = new StreamReader(stream);
            return Convert.ToInt64(r.ReadLine());
        }
    }
}
