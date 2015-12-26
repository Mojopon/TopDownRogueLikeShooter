using UnityEngine;
using System.Collections;
using NUnit.Framework;
using System;

[TestFixture]
public class HeapTest
{
	public void ShouldReturnLowestCost()
    {
        Heap<HeapItemInt> heap = new Heap<HeapItemInt>(20);
    }

    [Test]
    public void ShouldCompareHeapItems()
    {
        var heapItemOne = new HeapItemInt(5);
        var heapItemTwo = new HeapItemInt(5);
        Assert.IsTrue(heapItemOne.CompareTo(heapItemTwo) == 0);
        var heapItemThree = new HeapItemInt(4);
        Assert.IsTrue(heapItemOne.CompareTo(heapItemThree) == -1);
        Assert.IsTrue(heapItemThree.CompareTo(heapItemOne) == 1);
    }

    [Test]
    public void RemovedItemShouldAlwaysBeMinimumNumberInTheArray()
    {
        var heapItems = new HeapItemInt[10]
        {
            new HeapItemInt(6),
            new HeapItemInt(2),
            new HeapItemInt(3),
            new HeapItemInt(7),
            new HeapItemInt(9),
            new HeapItemInt(8),
            new HeapItemInt(5),
            new HeapItemInt(4),
            new HeapItemInt(12),
            new HeapItemInt(4),
        };

        var heap = new Heap<HeapItemInt>(10);
        
        foreach(HeapItemInt item in heapItems)
        {
            heap.Add(item);
        }

        for(int i = 0; i < heapItems.Length;i++)
        {
            heapItems[i] = heap.RemoveFirst();
        }

        for(int i = 0; i < heapItems.Length-1;i++)
        {
            Assert.IsTrue(heapItems[i].number <= heapItems[i + 1].number);
        }
    }

    public class HeapItemInt : IHeapItem<HeapItemInt>
    {
        public int number;
        public HeapItemInt(int num)
        {
            number = num;
        }

        private int heapIndex;

        public int HeapIndex
        {
            get
            {
                return heapIndex;
            }

            set
            {
                heapIndex = value;
            }
        }

        public int CompareTo(HeapItemInt other)
        {
            int compare = number.CompareTo(other.number);

            return -compare;
        }
    }
}
