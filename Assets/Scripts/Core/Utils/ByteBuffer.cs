using UnityEngine;
using System;
using System.Text;
using System.Collections;
/// <summary>
/// buffer工具类
/// </summary>
public class ByteBuffer
{
    private int _readIndex;
    private int _writeIndex;
    private byte[] _buffer;
    public ByteBuffer()
    {
        _buffer = new byte[1024];
    }
    public ByteBuffer(int l)
    {
        _buffer = new byte[l];
    }
    public ByteBuffer(byte[] buffer)
    {
        _buffer = buffer;
    }
    public void WriteUint32(uint i)
    {
        Write(BitConverter.GetBytes(i));
    }
    public void WriteInt16(short i)
    {
        Write(BitConverter.GetBytes(i));
    }
    public void WriteInt8(int i)
    {
        byte[] b = new byte[1];
        b[0] = Convert.ToByte(i);
        Write(b);
    }
    public void WriteByte(byte by)
    {
        byte[] b = new byte[1];
        b[0] = by;
        Write(b);
    }
    public void WriteInt32(int i)
    {
        Write(BitConverter.GetBytes(i));
    }
    public void WriteInt64(long l)
    {
        Write(BitConverter.GetBytes(l));
    }
    public void WriteString(string s)
    {
        Write(Encoding.UTF8.GetBytes(s));
    }
    public void WriteDouble(double d)
    {
        Write(BitConverter.GetBytes(d));
    }
    public void WriteFloat(float f)
    {
        Write(BitConverter.GetBytes(f));
    }
    public int ReadInt8()
    {
        byte va = ReadBytes(1)[0];
        return (int)va;
    }
    public short ReadInt16()
    {
        short value = BitConverter.ToInt16(ReadBytes(2), 0);
        return value;
    }
    public UInt32 ReadUInt32()
    {
        UInt32 value = BitConverter.ToUInt32(ReadBytes(4), 0);
        return value;
    }
    public int ReadInt32()
    {
        int value = BitConverter.ToInt32(ReadBytes(4), 0);
        return value;
    }
    public string ReadString(int l)
    {
        return Encoding.UTF8.GetString(ReadBytes(l));
    }
    public string ReadLastString()
    {
        int lastL = _buffer.Length - _readIndex;
        return Encoding.UTF8.GetString(ReadBytes(lastL));
    }
    public byte[] toBytes()
    {
        byte[] returnByte = new byte[_writeIndex];
        Array.Copy(_buffer, returnByte, _writeIndex);
        return returnByte;
    }
    public byte[] ReadBytes(int l)
    {
        if ((_readIndex + l) > _buffer.Length)//没有足够的数据读取
        {
            throw new Exception("没有足够的数据");
        }
        byte[] bufferCoby = new byte[l];
        Array.Copy(_buffer, _readIndex , bufferCoby, 0 , l);
        _readIndex += l;
        return bufferCoby;
    }
    void Write(byte[] b)
    {
        int l = b.Length;
        resetBufferSizeAdaptData(l);
        Array.Copy(b, 0, _buffer, _writeIndex, l);
        _writeIndex += l;
    }
    void resetBufferSizeAdaptData(int addL)
    {
        if ((addL + _writeIndex) > _buffer.Length)
        {
            byte[] bufferCoby = new byte[addL + _writeIndex];
            Array.Copy(_buffer, bufferCoby, _buffer.Length);
            _buffer = bufferCoby;
        }
    }
}
