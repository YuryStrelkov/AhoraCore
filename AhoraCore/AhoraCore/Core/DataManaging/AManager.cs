using System.Collections.Generic;

namespace AhoraCore.Core.DataManaging
{
   public abstract class AManager<ManagingData,ManagingBy,uniqKeyType>
    {
        protected Dictionary<ManagingBy, ManagingData> managingData;

        protected Dictionary<uniqKeyType, ManagingBy> uniqueKeysList;

        public abstract void AppendData(uniqKeyType dataKey, ManagingBy managingBy);
        
        public abstract void RemoveData(uniqKeyType dataKey);

        public abstract void ClearManager();

        public abstract void DeleteManager();

        public AManager()
        {
            managingData = new Dictionary<ManagingBy, ManagingData>();
            uniqueKeysList = new Dictionary<uniqKeyType, ManagingBy>();
        }
    }
}
