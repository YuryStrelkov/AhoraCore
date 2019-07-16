using System.Collections.Generic;

namespace AhoraCore.Core.Utils
{
    public class IndexedList<KeyType>
    {
        public struct ListIteam
        {
            public KeyType ID { get;  set; }

            public KeyType ParentID { get;  set; }

            public KeyType ChildID { get;  set; }
            
            public int StartPosition { get;  set; }

            public int Length { get;  set; }


            public void shiftLeft(int v_shift)
            {
                StartPosition -= v_shift;
            }

            public void shiftRight(int v_shift)
            {
                StartPosition += v_shift;
            }

            public ListIteam(KeyType ID_, KeyType ParentID_, KeyType ChildID_, int p, int l )
            {
                StartPosition = p;
                Length = l;
                ID = ID_;
                ParentID = ParentID_;
                ChildID = ChildID_;
            }
           
        }

        private int initialOffset = 0;

        public Dictionary<KeyType,ListIteam> Indexes { get; }

        private KeyType LastKeyAdded;

        public void Add(KeyType key,int Length)
        {

            

            if (Indexes.Count == 0)
            {
                Indexes.Add(key, new ListIteam(key, key, key, initialOffset, Length));

                LastKeyAdded = key;

                initialOffset += Length;
            }
            else
            {
                Indexes.Add(key, new ListIteam(key, LastKeyAdded, key, initialOffset, Length));

                ListIteam L = Indexes[LastKeyAdded];

                L.ChildID = key;

                Indexes[LastKeyAdded] = L;

                LastKeyAdded = key;

                initialOffset += Length;
            }
        }

        public int GetLength(KeyType key)
        {
            return Indexes[key].Length;
        }
        public int GetOffset(KeyType key)
        {
            return Indexes[key].StartPosition;
        }

        public void Remove(KeyType key)
        {
            if (Indexes.ContainsKey(key))
            {
               KeyType t_key = Indexes[key].ChildID;

                do
                {
                    Indexes[t_key].shiftLeft(Indexes[key].Length);
                    t_key = Indexes[t_key].ChildID;
                } while (!Indexes[t_key].ID.Equals(Indexes[t_key].ChildID));


                ///Pearent reassign
                ListIteam tmp = Indexes[Indexes[key].ParentID];

                tmp.ChildID = Indexes[key].ChildID;

                Indexes[Indexes[key].ParentID] = tmp;

                ///Child reassign
                tmp = Indexes[Indexes[key].ChildID];

                tmp.ParentID = Indexes[key].ParentID;

                Indexes[Indexes[key].ChildID] = tmp;

                Indexes.Remove(key);
            }

        }

        public void TryToGet(KeyType key, out int offset, out int length)
        {
            offset=Indexes[key].StartPosition;
            length = Indexes[key].Length;
        }

        public IndexedList()
        {
            Indexes = new Dictionary<KeyType, ListIteam>();
        }

    }
}
