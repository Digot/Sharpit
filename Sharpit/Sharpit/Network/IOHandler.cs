using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Sharpit.Network
{
    public class IOHandler
    {

        public NetworkStream _stream;
        public List<byte> _buffer;
        public int _offset;
        public TcpClient clientSocket;

        public int State { get; set; }

        public IOHandler(NetworkStream stream,TcpClient clientSocket)
        {
            _stream = stream;
            this.clientSocket = clientSocket;
        }

        public byte ReadByte(byte[] buffer)
        {
            var b = buffer[_offset];
            _offset += 1;
            return b;
        }

        public byte[] Read(byte[] buffer, int length)
        {
            var data = new byte[length];
            Array.Copy(buffer, _offset, data, 0, length);
            _offset += length;
            return data;
        }

        public int ReadVarInt(byte[] buffer)
        {
            var value = 0;
            var size = 0;
            int b;
            while (((b = ReadByte(buffer)) & 0x80) == 0x80)
            {
                value |= (b & 0x7F) << (size++*7);
                if (size > 5)
                {
                    throw new IOException("This VarInt is an imposter!");
                }
            }
            return value | ((b & 0x7F) << (size*7));
        }

        public string ReadString(byte[] buffer, int length)
        {
            var data = Read(buffer, length);
            return Encoding.UTF8.GetString(data);
        }

        public short ReadShort(byte[] buffer)
        {
            //Size is 2 as short has 16bytes
            var data = Read(buffer, 2);
            return BitConverter.ToInt16(data, 0);
        }

        public void WriteVarInt(int value)
        {
            while ((value & 128) != 0)
            {
                _buffer.Add((byte) (value & 127 | 128));
                value = (int) ((uint) value) >> 7;
            }
            _buffer.Add((byte) value);
        }

        public void WriteShort(short value)
        {
            _buffer.AddRange(BitConverter.GetBytes(value));
        }

        public void WriteString(string data)
        {
            var buffer = Encoding.UTF8.GetBytes(data);
            WriteVarInt(buffer.Length);
            _buffer.AddRange(buffer);
        }

        public void Write(byte b)
        {
            _stream.WriteByte(b);
        }

        public void Flush(int id = -1)
        {
            var buffer = _buffer.ToArray();
            _buffer.Clear();

            var add = 0;
            var packetData = new[] {(byte) 0x00};
            if (id >= 0)
            {
                WriteVarInt(id);
                packetData = _buffer.ToArray();
                add = packetData.Length;
                _buffer.Clear();
            }

            WriteVarInt(buffer.Length + add);
            var bufferLength = _buffer.ToArray();
            _buffer.Clear();

            _stream.Write(bufferLength, 0, bufferLength.Length);
            _stream.Write(packetData, 0, packetData.Length);
            _stream.Write(buffer, 0, buffer.Length);
        }
    }
}