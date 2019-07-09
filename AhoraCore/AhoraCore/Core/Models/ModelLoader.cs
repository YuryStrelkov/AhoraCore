using Assimp;
using Assimp.Configs;


namespace AhoraCore.Core.Models
{
    public static class ModelLoader
    {
        public static void LoadModel(string filename)
        {
            AssimpContext importer = new AssimpContext();
            importer.SetConfig(new NormalSmoothingAngleConfig(66.6f));
            Scene m_model = importer.ImportFile(filename, PostProcessSteps.Triangulate |
                                                               PostProcessSteps.CalculateTangentSpace |
                                                               PostProcessSteps.FlipUVs |
                                                               PostProcessSteps.LimitBoneWeights);
             ////TODO
        }
    }

}
