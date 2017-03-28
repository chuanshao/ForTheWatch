using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;
public class PackageBytes
{
    protected ByteBuffer Buffer;
    private short _type;
    private short _cmd;
    public PackageBytes(short type, short cmd )
    {
        this.Buffer = new ByteBuffer();
        this._cmd = cmd;
        this._type = type;
        this.Buffer.WriteInt16(type);
        this.Buffer.WriteInt16(cmd);
    }
    public string GetKey()
    {
        return string.Format("{0}_{1}", this._type, this._cmd);
    }
    public byte[] ToBytes()
    {
        return this.Buffer.toBytes();
    }
}

public class JsonPackageBytes : PackageBytes
{
    public JsonPackageBytes(short type, short cmd , JsonData jdata) : base(type, cmd)
    {
        ByteBuffer b = new ByteBuffer();
        b.WriteString(JsonMapper.ToJson(jdata));
        byte[] stringData = b.toBytes();
        this.Buffer.WriteInt32(stringData.Length);
        this.Buffer.WriteString(JsonMapper.ToJson(jdata));
    }
}
