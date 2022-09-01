using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCP_Chatting_Server
{
     public partial class Server : Form
    {        
        private Thread receiveThread;      
        private Socket clientSocket;
        
        public Server()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread serverThread = new Thread(serverFunc);
            serverThread.Start();
            serverThread.IsBackground = true;                 
        }
        private void serverFunc(object obj)
        {
            try
            {
                Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint serverEP = new IPEndPoint(IPAddress.Any, 12300);
                server.Bind(serverEP);
                server.Listen(10);                
                clientSocket = server.Accept();
                                

                receiveThread = new Thread(new ThreadStart(Receive));
                receiveThread.IsBackground = true;
                receiveThread.Start();               
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void Receive()
        {
            while (true)
            {
                byte[] Recv = new byte[1024];
                int nRecv = clientSocket.Receive(Recv);

                string txt = Encoding.UTF8.GetString(Recv, 0, nRecv);
                showMsg("You : " + txt);
            }
        }  

        private void showMsg(string msg)
        {
            richTextBox1.AppendText(msg);
            richTextBox1.AppendText("\r\n");

            textBox2.Focus();
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] sendBuffer = Encoding.UTF8.GetBytes(textBox2.Text.Trim());
            clientSocket.Send(sendBuffer);
            showMsg("Me : " + textBox2.Text);
            textBox2.Text = "";           
        }

    }

}
