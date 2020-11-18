﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using DoAnLTTQ.Backend;
using DoAnLTTQ.Components;

namespace DoAnLTTQ.Backend
{
    class Server
    {
        public void ListenProfile()
        {
            var listener = new TcpListener(IPAddress.Any, 1308);
            listener.Start(10);
            byte[] data = new byte[1024 * 1000];
            while (true)
            {
                var client = listener.AcceptTcpClient();
                var stream = client.GetStream();
                var formatter = new BinaryFormatter();
                var guest = formatter.Deserialize(stream) as GuestProfile;
                guest.saveData();
                
                //this.Dispatcher.Invoke(() =>
                //{
                //    img.Source = Common.LoadImage(student.avatar.buffer);
                //});
                client.Close();
            }
        }
        public void SendProfile(IPAddress ip)
        {
            User user = new User();
            GuestProfile u = new GuestProfile(user);
            var client = new TcpClient();
            client.Connect(ip, 1308);
            var stream = client.GetStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, u);

            client.Close();
        }

        public void ListenRequestMessage()
        {
            var localIp = IPAddress.Any;
            var localPort = 1308;
            var localEndPoint = new IPEndPoint(localIp, localPort);
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Bind(localEndPoint);

            var size = 1024;
            var receiveBuffer = new byte[size];

            while (true)
            {
                EndPoint remoteEndpoint = new IPEndPoint(IPAddress.Any, 0);
                var length = socket.ReceiveFrom(receiveBuffer, ref remoteEndpoint);
                var text = Encoding.ASCII.GetString(receiveBuffer, 0, length);
                SendProfile(IPAddress.Parse(text));
                Array.Clear(receiveBuffer, 0, size);
            }
        }

        public void SendRequestMessage()
        {
            var serverEndpoint = new IPEndPoint(IPAddress.Broadcast, 1308);
            var socket = new Socket(SocketType.Dgram, ProtocolType.Udp);

            var localIp = "";
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIp =  ip.ToString();
                }
            }

            byte[] data = Encoding.ASCII.GetBytes(localIp);
            socket.SendTo(data, serverEndpoint);
        }

        //public void SendProfile();

    }
}

