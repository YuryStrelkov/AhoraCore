using AhoraCore.Core.Buffers.IBuffers;
using System;
using System.Collections.Generic;

namespace AhoraCore.Core.Buffers.DataStorraging.StorrageTemplate
{/// <summary>
/// Иерархически хранит данные 
/// </summary>
/// <typeparam name="KeyType"></typeparam>
/// <typeparam name="ValueType"></typeparam>
    public abstract class TemplateStorrage <KeyType, ValueType> : IDataStorrage<KeyType> 
    {
        public delegate void IteamAtion(ValueType ItemData);

        public struct Cell
        {
         public ValueType Data { get; set; }

         public KeyType ID { get; set; }

         public KeyType Parent { get; set; }

         public List<KeyType> Childrens { get; set; }

         public void AddChild(KeyType child)
         {
                Childrens.Add(child);
         }

         public void RemoveChild(KeyType child)
          {
                Childrens.Remove(child);
          }

         public void SetParent(KeyType parent)
          {
                Parent = parent;
          }

         public Cell(KeyType ID_, KeyType Parent_, ValueType val)
         {
                ID = ID_;
                Data = val;
                Parent = Parent_;
                Childrens = new List<KeyType>();
         }
        }

        protected KeyType lastKey;

        public KeyType RootID { get; protected set; }

        public ValueType Root { get { return Iteams[RootID].Data; } }

        public  Dictionary<KeyType, Cell> Iteams { get; protected set; }

        public ValueType GetItem(KeyType ID)
       {
              return Iteams[ID].Data;
       }

       

        public void AddItems(Dictionary<KeyType, ValueType> Iteams)
        {
            foreach (KeyType k in Iteams.Keys)
            {
                AddItem(k, Iteams[k]);
            }
        }

        public void AddItem(KeyType ID, ValueType Item)
        {
            if (Iteams.Count==0)
            {
                Iteams.Add(ID, new Cell( ID, ID, Item));
                lastKey = ID;
                RootID = ID;
                return;
            }

            if (!Iteams.ContainsKey(ID))
            {
                Iteams.Add(ID, new Cell(ID, lastKey, Item));

                Iteams[lastKey].AddChild(ID);

                lastKey = ID;
            }
            else
            {
                Cell t = Iteams[ID];

                DeleteIteamData(t.ID);

                t.Data = Item;

                Iteams[ID] = t;
            }
        }

        public void AddItem(KeyType ParentID, KeyType ID, ValueType Item)
        {

            if (!Iteams.ContainsKey(ParentID))
            {
                Console.WriteLine("Unnable to find ParentID : "+ ParentID);
            }

            if (!Iteams.ContainsKey(ID))
            {
                Iteams.Add(ID, new Cell(ID, ParentID, Item));

                Iteams[ParentID].AddChild(ID);

                lastKey = ID;
            }
            else
            {
                Cell t = Iteams[ID];

                Iteams[t.Parent].RemoveChild(ID);

                t.Parent = ParentID;

                Iteams[ParentID].AddChild(t.ID);

                DeleteIteamData(t.ID);

                t.Data = Item;

                Iteams[ID] = t;
            }
        }

        public void ClearStorrage()
        {
            foreach (KeyType k in Iteams.Keys)
            {
                ClearIteamData(k);
            }
        }

        public void DeleteStorrage()
        {
            foreach (KeyType k in Iteams.Keys)
            {
                DeleteIteamData(k);
            }
            Iteams.Clear();
        }

        public void RemoveChildrens(KeyType ID)
        {
            if (!Iteams.ContainsKey(ID))
            {
                foreach (KeyType key in Iteams[ID].Childrens)
                {
                    RemoveItem(key);
                }
                Iteams[ID].Childrens.Clear();
            }
        }


        public void RemoveItem(KeyType ID)
        {
            if (!Iteams.ContainsKey(ID))
            {
                DeleteIteamData(ID); ///StorrageIteams[ID].Data.Delete();

                Iteams[Iteams[ID].Parent].RemoveChild(ID);

                foreach (KeyType k in Iteams[ID].Childrens)
                {
                    Iteams[Iteams[ID].Parent].AddChild(k);
                    Iteams[k].SetParent(Iteams[ID].Parent);
                }

                Iteams.Remove(ID);
            }
        }

        public TemplateStorrage()
        {
            Iteams = new Dictionary<KeyType, Cell>();
        }

        public void DoAction(KeyType key, IteamAtion action)
        {
            action(Iteams[key].Data);

            foreach (KeyType k in Iteams[key].Childrens)
            {
                DoAction(k, action);
            }
        }

        public void WholeStorrageAction(IteamAtion action)
        {
            DoAction(RootID, action);
        }

        public abstract void ClearIteamData(KeyType ID);

        public abstract void DeleteIteamData(KeyType ID);


    }
}
