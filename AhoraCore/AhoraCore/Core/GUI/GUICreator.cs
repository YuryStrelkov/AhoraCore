using AhoraCore.Core.Buffers.DataStorraging;
using AhoraCore.Core.CES;
using AhoraCore.Core.CES.Components;
using AhoraCore.Core.GUI.UIElemets;

namespace AhoraCore.Core.GUI
{
    public static class GUICreator
    {
        public static void CreateUIpannel(string UI_ID)
        {
            GameEntity ui = new GameEntity(UI_ID);

            ui.RemoveComponent(ComponentsTypes.MaterialComponent);

            ui.AddComponent(ComponentsTypes.ShaderComponent, new ShaderComponent("GUIShader"));

            ui.AddComponent(ComponentsTypes.GUIComponent, new UIPannel(ui.GetComponent<ShaderComponent>(ComponentsTypes.ShaderComponent).Shader));

            GameEntityStorrage.Entities.AddItem("root", UI_ID, ui);
        }

        public static void CreateUIpannel(string ROOT_ID, string UI_ID)
        {
            GameEntity ui = new GameEntity(UI_ID);

            ui.RemoveComponent(ComponentsTypes.MaterialComponent);

            ui.AddComponent(ComponentsTypes.ShaderComponent, new ShaderComponent("GUIShader"));

            ui.AddComponent(ComponentsTypes.GUIComponent, new UIPannel(ui.GetComponent<ShaderComponent>(ComponentsTypes.ShaderComponent).Shader));

            GameEntityStorrage.Entities.AddItem(ROOT_ID, UI_ID, ui);
        }

        public static void CreateUIpannel(string ROOT_ID, string UI_ID, string texture1, string texture2)
        {
            GameEntity ui = new GameEntity(UI_ID);

            ui.RemoveComponent(ComponentsTypes.MaterialComponent);

            ui.AddComponent(ComponentsTypes.ShaderComponent, new ShaderComponent("GUIShader"));

            ui.AddComponent(ComponentsTypes.GUIComponent, new UIPannel(ui.GetComponent<ShaderComponent>(ComponentsTypes.ShaderComponent).Shader));

            ui.GetComponent<UIPannel>(ComponentsTypes.GUIComponent).AddTexture2GUI("texture1", texture1);
       
            ui.GetComponent<UIPannel>(ComponentsTypes.GUIComponent).AddTexture2GUI("texture2", texture2);
       
            GameEntityStorrage.Entities.AddItem(ROOT_ID, UI_ID, ui);
        }


        public static void CreateUItext(string ROOT_ID, string UI_ID, int rows, int cols, string text)
        {
            GameEntity ui = new GameEntity(UI_ID);

            ui.RemoveComponent(ComponentsTypes.MaterialComponent);

            ui.AddComponent(ComponentsTypes.ShaderComponent, new ShaderComponent("GUITextShader"));

            ui.AddComponent(ComponentsTypes.GUIComponent, new GUIText(ui.GetComponent<ShaderComponent>(ComponentsTypes.ShaderComponent).Shader, CharSets.FontTypes.Arial, rows, cols));

            ui.GetComponent<GUIText>(ComponentsTypes.GUIComponent).setText(text);

            GameEntityStorrage.Entities.AddItem(ROOT_ID, UI_ID, ui);

        }

    }
}
