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
