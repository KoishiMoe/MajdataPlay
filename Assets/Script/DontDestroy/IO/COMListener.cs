﻿using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace MajdataPlay.IO
{
    public partial class IOManager : MonoBehaviour
    {
        async void COMReceiveAsync(CancellationToken token)
        {
            var serial = new SerialPort("COM3", 9600);
            try
            {
                recvTask = Task.Run(() =>
                {
                    while (true)
                    {
                        if (token.IsCancellationRequested)
                            token.ThrowIfCancellationRequested();
                        if (serial.IsOpen)
                        {
                            int count = serial.BytesToRead;
                            var buf = new byte[count];
                            serial.Read(buf, 0, count);
                            if (buf.Length > 0)
                            {

                                if (buf[0] == '(')
                                {
                                    int k = 0;
                                    for (int i = 1; i < 8; i++)
                                    {
                                        print(buf[i].ToString("X2"));
                                        for (int j = 0; j < 5; j++)
                                        {
                                            COMReport[k] = (buf[i] & 0x01 << j) > 0;
                                            k++;
                                        }
                                    }
                                }

                            }
                        }
                        else
                        {
                            serial.Open();
                            serial.Write("{STAT}");
                        }
                    }
                });
                await recvTask;
            }
            finally
            {
                serial.Close();
            }
        }
    }
}
