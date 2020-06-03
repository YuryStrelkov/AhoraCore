using AhoraCore.Core.Buffers.DataStorraging.StorrageTemplate;
using AhoraCore.Core.CES.Components;
using AhoraCore.Core.CES.ICES;

using System.Collections.Generic;

namespace AhoraCore.Core.CES
{
    public class GameEntityStorrage : TemplateStorrage<string, IGameEntity>
    {
        private static GameEntityStorrage entities;

        private static Stack<string> RenderQueIDs;

        public void AddItem2RederQue(string id)
        {
            RenderQueIDs.Push(id);
        }

        public string GetNextRenderID()
        {
            return RenderQueIDs.Pop();
        }

        public bool HasElementsInRenderQue()
        {
           return RenderQueIDs.Count != 0;
        }

        public static void Initialize()
        {
            RenderQueIDs = new Stack<string>();

            entities = new GameEntityStorrage();

            GameEntity root = new GameEntity("root");

            Entities.AddItem(root.EntityID, root);

            root.AddComponent(new TransformComponent());

            root.SetLocalScale(1, 1f, 1);

            root.SetWorldScale(1, 1f, 1);

        }

        public static GameEntityStorrage Entities
        {
        get {
                return entities;
            }
        }

        public void Add2RenderQue(string NodeID)
        {
            RenderQueIDs.Push(NodeID);
        }

        public void Input(string NodeID)
        {
            DoAction(NodeID, (gameEntity) => { gameEntity.Input(); });
        }

        public void Update(string NodeID)
        {
            DoAction(NodeID, (gameEntity) => { gameEntity.Update();});
        }

        public void Render(string NodeID)
        {
            DoAction(NodeID, (gameEntity) => { gameEntity.Render();});
        }

        public void Disable(string NodeID)
        {
            DoAction(NodeID, (gameEntity) => { gameEntity.Disable(); });
        }

        public void Delete(string NodeID)
        {
            DoAction(NodeID, (gameEntity) => { gameEntity.Delete(); });
        }

        public override void ClearIteamData(string ID)
        {
            Iteams[ID].Data.Clear();
        }

        public override void DeleteIteamData(string ID)
        {
            Iteams[ID].Data.Delete();
        }

        private GameEntityStorrage() : base()
        {

        }
    }
}
