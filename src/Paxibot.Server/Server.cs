using Paxibot.Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

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
            switch (status)
            {
                case ServerStatus.Idle:
                    break;
                case ServerStatus.WaitingConnections:
                    throw new Exception("Server already initiated.");
                case ServerStatus.GameStarted:
                    throw new Exception("Game already started.");
                default:
                    throw new ArgumentOutOfRangeException("status");
            }

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



        public void StartGame()
        {
            switch (status)
            {
                case ServerStatus.Idle:
                    throw new Exception("Server not initiated");
                case ServerStatus.WaitingConnections:
                    break;
                case ServerStatus.GameStarted:
                    throw new Exception("Game already started.");
                default:
                    throw new ArgumentOutOfRangeException("status");
            }


            status = ServerStatus.GameStarted;
            //TODO start game process
        }

        #region Private Functions

        private void AcceptClients()
        {
            do
            {
                var newConnection = serverSocket.Accept();
                RegisterClient(newConnection);
            } while (true);
        }

        private void RegisterClient(Socket socket)
        {
           
        }

        #endregion Private Functions


    }

}
