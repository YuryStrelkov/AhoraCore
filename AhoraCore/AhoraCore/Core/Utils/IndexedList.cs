using AhoraCore.Core.Buffers.DataStorraging.StorrageTemplate;
using OpenTK;
using System.Collections.Generic;
using System;

namespace AhoraCore.Core.Utils
{
    public struct ListIteam<KeyType>
    {
        public int StartPosition { get; set; }
     
        public int Length { get; set; }

        public void shiftLeft(int v_shift)
        {
            StartPosition -= v_shift;
        }

        public void shiftRight(int v_shift)
        {
            StartPosition += v_shift;
        }

        public ListIteam( int p, int l)
        {
            StartPosition = p;
            Length = l;
        }

    }

    public class IndexedList<KeyType>: TemplateStorrage<KeyType, ListIteam<KeyType>>
    {
       

        private int initialOffset = 0;

        public void Add(KeyType key, int Length)
        {
             AddItem(key, new ListIteam<KeyType>(initialOffset, Length));
            initialOffset += Length;
        }


        public void Clear()
        {
            base.ClearStorrage();
            initialOffset = 0;
        }

        public int GetLength(KeyType key)
        {
            return Iteams[key].Data.Length;
        }

        public int GetOffset(KeyType key)
        {
            return Iteams[key].Data.StartPosition;
        }

        public void Remove(KeyType key)
        {
           ///работает неправильно !!!
          if (Iteams.ContainsKey(key))
            {

                if (Iteams[key].Childrens.Count==0)
                {
                    RemoveItem(key);
                    return;
                }
                //KeyType t_key = Iteams[key].Childrens[0];

                //do
                //{
                //    Iteams[t_key].Data.shiftLeft(Iteams[t_key].Data.Length);
                //    t_key = Iteams[key].Childrens[0];
                //} while (!Iteams[key].ID.Equals(Iteams[t_key].Childrens[0]));

                //base.RemoveItem(key);
            }
            
        }

        public void TryToGet(KeyType key, out int offset, out int length)
        {
            offset= Iteams[key].Data.StartPosition;
            length = Iteams[key].Data.Length;
        }

        public override void ClearIteamData(KeyType ID)
        {
           // throw new NotImplementedException();
        }

        public override void DeleteIteamData(KeyType ID)
        {
          ///  throw new NotImplementedException();
        }

        public IndexedList():base()
        {
    
        }

    }
}
