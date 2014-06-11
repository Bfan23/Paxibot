using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Paxibot.Server
{
    public class Server
    {
        private Socket serverSocket;
        private Thread serverThread;
        private List<PaxibotClient> clientList;
        private ServerStatus status;      

        private int maxListenQueue;

        public Server()
        {
            this.status = ServerStatus.Idle;
            this.maxListenQueue = 100;            
        }

        public void Initiate(int port)
        {
            this.clientList = new List<PaxibotClient>();

            //create server socket
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);          
            
            //get ip address
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);

            //start listening
            serverSocket.Bind(localEndPoint);
            serverSocket.Listen(maxListenQueue);
            status = ServerStatus.WaitingConnections;
            serverThread = new Thread(new ThreadStart(AcceptClients));
            serverThread.Start();

        }

        private void AcceptClients()
        {
            while (true)
            {
                var newConnection = serverSocket.Accept();
                var client = new PaxibotClient()
                {
                    Socket = newConnection,
                    Id = Guid.NewGuid().ToString()
                };


                clientList.Add(client);

                //TODO register clients to the game
            }
        }

        public void StartGame()
        {
            status = ServerStatus.GameStarted;
            //TODO start game process
        }


        }

    public class PaxibotClient
    {
        public Socket Socket { get; set; }
        public string Id { get; set; }
    }

    public enum ServerStatus
    {
        Idle,
        WaitingConnections,
        GameStarted
    }
}
