using AhoraCore.Core.Buffers.IBuffers;
using System.Collections.Generic;

namespace AhoraCore.Core.Buffers.DataStorraging.StorrageTemplate
{
    public class TemplateStorrage <KeyType, ValueType, BindTarget> : IDataStorrage<KeyType> where ValueType:ABindableObject<BindTarget>
    {
        public Dictionary<KeyType, ValueType> StorrageIteams { get; protected set; }



        public ValueType GetItem(KeyType ID)
        {
          return  StorrageIteams[ID];
        }


        public void AddItem(KeyType ID, ValueType Item)
        {
            if (!StorrageIteams.ContainsKey(ID))
            {
                StorrageIteams.Add(ID, Item);
            }
            else
            {
                StorrageIteams[ID].Delete();
                StorrageIteams[ID] = Item;
            }
        }
        
        public void ClearStorrage()
        {
            foreach (KeyType k in StorrageIteams.Keys)
            {
                StorrageIteams[k].Clear();
            }
        }

        public void DeleteStorrage()
        {
            foreach (KeyType k in StorrageIteams.Keys)
            {
                StorrageIteams[k].Delete();
            }
            StorrageIteams.Clear();
        }

        public void RemoveItem(KeyType ID)
        {
            if (!StorrageIteams.ContainsKey(ID))
            {
                StorrageIteams[ID].Delete();
                StorrageIteams.Remove(ID);
            }
        }

    }
}
