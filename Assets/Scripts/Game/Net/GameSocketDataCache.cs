using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameSocketDataCache : ISocketDataCache
{
    private Queue<byte[]> bytesQueue = new Queue<byte[]>();
    private object _queueLock = new object();
    public void PushData(byte[] dataArr)
    {
        lock (_queueLock)
        {
            bytesQueue.Enqueue(dataArr);
        }
    }
    public byte[] GetCurrentSendData()
    {
        return MergeAllBytes();
    }
    public bool HasData()
    {
        if (bytesQueue.Count > 0)
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// 组合所有数据包
    /// </summary>
    /// <returns></returns>
    byte[] MergeAllBytes()
    {
        lock (_queueLock)
        {
            byte[] allBytes = new byte[0];
            while (bytesQueue.Count > 0)
            {
                byte[] itemArr = bytesQueue.Dequeue();
                int allL = allBytes.Length;
                int itemL = itemArr.Length;
                byte[] destinationArr = new byte[allL + itemL];
                Array.Copy(allBytes, destinationArr, allL);
                Array.Copy(itemArr, 0, destinationArr, allL, itemL);
                allBytes = destinationArr;
            }
            return allBytes;
        }
    }
}
