using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructureRepo.Design
{
    /*
     Design a data structure that follows the constraints of a Least Recently Used (LRU) cache.

    Implement the LRUCache class:

        LRUCache(int capacity) Initialize the LRU cache with positive size capacity.
        int get(int key) Return the value of the key if the key exists, otherwise return -1.
        void put(int key, int value) Update the value of the key if the key exists. Otherwise, add the key-value pair to the cache.
        If the number of keys exceeds the capacity from this operation, evict the least recently used key.
    
    The functions get and put must each run in O(1) average time complexity.

    https://leetcode.com/problems/lru-cache/
     */
    class LRU_Cache
    {
        /*
         We're asked to implement the structure which provides the following operations in O(1) time :

            Get the key / Check if the key exists
            Put the key
            Delete the first added key

        The first two operations in \mathcal{O}(1)O(1) time are provided by the standard hashmap, and the last one - by linked list.
         */

        public class LRUCache
        {

            //Lesson learnt : Boxing-Unboxing is very costly operation. LinkedList.Remove("abc") will cast abc in to LinkedListNode
            //so instead of taking a LinkedList of Int and Dictionary of <int, int>, using a Dictionary of <int, LinkedListNode> 


            private class Node
            {
                public int Key { get; set; }
                public int Val { get; set; }
            }

            //For initializing the constructor with capacity
            public int capacity { get; private set; }

            //DLL for maintaining LRU logic - Move/Add the most recently used item at front of the list
            private LinkedList<Node> circularList;

            //Dictionary for fetching the value for a key in O(1) time 
            private Dictionary<int, LinkedListNode<Node>> map;

            //variable to keep track of DLL(Doubly linked list) size since list.Count is O(n) operation
            private int size;

            public LRUCache(int capacity)
            {
                this.capacity = capacity;
                circularList = new LinkedList<Node>();
                map = new Dictionary<int, LinkedListNode<Node>>();
                size = 0;
            }

            public int Get(int key)
            {
                if (!map.ContainsKey(key)) return -1;

                var node = map[key];
                // ??? why need to remove / addFirst Node here
                circularList.Remove(node);
                circularList.AddFirst(node);
                return node.Value.Val;
            }

            public void Put(int key, int value)
            {

                if (map.ContainsKey(key))
                {
                    var node = map[key];                    
                    circularList.Remove(node);
                    circularList.AddFirst(node);
                    node.Value.Val = value;

                    return;
                }

                if (size == capacity)
                {
                    map.Remove(circularList.Last.Value.Key);
                    circularList.RemoveLast();
                    size--;
                }

                //Add the new key, value at the front of the list
                circularList.AddFirst(new Node { Key = key, Val = value });
                //The LinkedListNode will now be added in the dictionary for the given key
                map.Add(key, circularList.First);
                size++;

            }
        }

        /**
         * Your LRUCache object will be instantiated and called as such:
         * LRUCache obj = new LRUCache(capacity);
         * int param_1 = obj.Get(key);
         * obj.Put(key,value);
         */
    }
}
