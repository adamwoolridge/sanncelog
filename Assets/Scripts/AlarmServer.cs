using UnityEngine;
using System;
using System.Collections;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Networking;
using System.IO;

public class AlarmServer : MonoBehaviour
{
    bool mRunning = false;
    string msg = "";
    Thread mThread;
    TcpListener tcp_Listener = null;

    void Start()
    {
        mRunning = true;
        ThreadStart ts = new ThreadStart(SayHello);
        mThread = new Thread(ts);
        mThread.Start();
        print("Thread done...");
    }

    public void stopListening()
    {
        mRunning = false;
    }

    void SayHello()
    {
        try
        {
            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse("192.168.1.84"), 15002);
            tcp_Listener = new TcpListener(ipe);
            tcp_Listener.Start();
            print("Server Start");
            while (mRunning)
            {
                // check if new connections are pending, if not, be nice and sleep 100ms
                if (!tcp_Listener.Pending())
                {
                    Thread.Sleep(100);
                }
                else
                {
                    
                    TcpClient client = tcp_Listener.AcceptTcpClient();
                    byte[] readBuffer = new byte[client.ReceiveBufferSize];
                    NetworkStream ns = client.GetStream();

                    while (ns.DataAvailable)
                    {
                        int numberOfBytesRead = ns.Read(readBuffer, 0, readBuffer.Length);
                        if (numberOfBytesRead <= 0)
                        {
                            break;
                        }


                        string data = Encoding
              .GetEncoding("DETECTED ENCODING OR ISO-8859-1")
              .GetString(readBuffer);

                        Debug.Log(data);
                        //Debug.Log(System.Text.Encoding.UTF32.GetString(readBuffer,0,numberOfBytesRead));
                    }
                    //reader.Close();
                    //client.Close();
                }
            }
        }
        catch (ThreadAbortException)
        {
            print("exception");
        }
        finally
        {
            mRunning = false;
            tcp_Listener.Stop();
        }
    }
    //void OnApplicationQuit()
    //{
    //    // stop listening thread
    //    stopListening();
    //    // wait fpr listening thread to terminate (max. 500ms)
    //    mThread.Join(500);
    //}
}