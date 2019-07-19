using AhoraCore.Core.Buffers.DataStorraging.StorrageTemplate;

namespace AhoraCore.Core.CES
{
    public class GameEntityStorrage : TemplateStorrage<string, GameEntity>
    {
    

        public void Input(string NodeID)
        {
            DoAction(NodeID, (gameEntity) => { gameEntity.Input(); });
        }

        public void Update(string NodeID)
        {
            DoAction(NodeID, (gameEntity) => { gameEntity.Update(); });
        }

        public void Render(string NodeID)
        {
            DoAction(NodeID, (gameEntity) => { gameEntity.Render(); });
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
          ///  StorrageIteams[ID].Data.Clear();
        }

        public override void DeleteIteamData(string ID)
        {
            Iteams[ID].Data.Delete();
        }
        public GameEntityStorrage() : base()
        {

        }
    }
}
