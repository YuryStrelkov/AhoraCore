using AhoraCore.Core.CES.ICES;
using AhoraCore.Core.Materials;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AhoraCore.Core.GUI.CharSets
{


    public enum FontTypes
    {
        Arial = 0,

        GOST = 1,

        ISO = 2,

        Mesquite = 3,

        Magneto = 4,

        OCR = 5
    }

    public class CharactesSet : UniformBufferedObject
    {
        private static Dictionary<FontTypes, CharactesSet> CharacterSets;

        public static Dictionary<FontTypes, CharactesSet> Fonts
        {
            get
            {
                if (CharacterSets == null)
                {
                    initFonts();
                }
                return CharacterSets;
            }
        }

        private static void initFonts()
        {
            CharacterSets = new Dictionary<FontTypes, CharactesSet>();

            CharacterSets.Add(FontTypes.Arial, new CharactesSet(@"Resources/GUI/Fonts/Arial.fnt",
                                                                @"Resources/GUI/Fonts/Arial.png"));

            CharacterSets.Add(FontTypes.GOST, new CharactesSet(@"Resources/GUI/Fonts/GOST.fnt",
                                                               @"Resources/GUI/Fonts/GOST.png"));

            CharacterSets.Add(FontTypes.ISO, new CharactesSet(@"Resources/GUI/Fonts/ISOCTEUR.fnt",
                                                              @"Resources/GUI/Fonts/ISOCTEUR.png"));

            CharacterSets.Add(FontTypes.Mesquite, new CharactesSet(@"Resources/GUI/Fonts/Mesquite_Std.fnt",
                                                                   @"Resources/GUI/Fonts/Mesquite_Std.png"));

            CharacterSets.Add(FontTypes.Magneto, new CharactesSet(@"Resources/GUI/Fonts/Magneto.fnt",
                                                                  @"Resources/GUI/Fonts/Magneto.png"));

            CharacterSets.Add(FontTypes.OCR, new CharactesSet(@"Resources/GUI/Fonts/OCR_A_BT.fnt",
                                                              @"Resources/GUI/Fonts/OCR_A_BT.png"));
        }

        public InfoFont Info = new InfoFont();

        public CommonFont Common = new CommonFont();

        static public int PageId;

        static public string PageFile;

        private int charsCount;

        private int kerningsCount;

        private int kerningsCountIter = 0;

        private Texture charactersMap;

        private Dictionary<int, Character> characters = new Dictionary<int, Character>();

        private Dictionary<int, Kerning> kernings = new Dictionary<int, Kerning>();

        public int KerningsCount { get { return kerningsCount; } }

        public int CharsCount { get { return charsCount; } }

        public Texture CharactersMap
        {
            get
            {
                return charactersMap;
            }
        }

        internal Dictionary<int, Character> Characters
        {
            get
            {
                return characters;
            }

        }

        public Dictionary<int, Kerning> Kernings
        {
            get
            {
                return kernings;
            }
        }

        public int KerningsCountIter
        {
            get
            {
                return kerningsCountIter;
            }
        }

        private CharactesSet(string file, string mapfile)
        {

            StreamReader reader = File.OpenText(file);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] items = System.Text.RegularExpressions.Regex.Replace(line, @"\s+", " ").Split(' ');
                switch (items[0])
                {
                    case ("info"):
                        infoParse(ref items);
                        break;
                    case ("common"):
                        commonParse(ref items);
                        break;
                    case ("page"):
                        PageId = int.Parse(items[1].Split('=')[1]);
                        PageFile = items[2].Split('=')[1];
                        break;
                    case ("chars"):
                        charsCount = int.Parse(items[1].Split('=')[1]);
                        break;
                    case ("char"):
                        charParse(ref items);
                        break;
                    case ("kernings"):
                        kerningsCount = int.Parse(items[1].Split('=')[1]);
                        break;
                    case ("kerning"):
                        kerningParse(ref items);
                        break;
                    default:
                        throw new System.ArgumentException("Parameter cannot be readed", items[0]);
                }
            }
            if (kerningsCountIter != kerningsCount) throw new System.ArgumentException("Not right file format", kerningsCount + "");
            //mapfile
            charactersMap = new Texture(mapfile);
            
            EnableBuffering("CharactersData");

            SetBindigLocation(UniformBindingsLocations.TextProperties);
            ///Symbol preset capcity -100
            MarkBufferItem("Advance",2 * 50);
            MarkBufferItem("Offset", 2 * 50);
            MarkBufferItem("UVSize", 2 * 50);
            MarkBufferItem("UVPosition", 2 * 50);
            ConfirmBuffer();

            float[] Advance = new float[2*Characters.Count];
            float[] Offset = new float[2 * Characters.Count];
            float[] UVSize = new float[2 * Characters.Count];
            float[] UVPosition = new float[2 * Characters.Count];

            int i = 0;

            ///??????????????????????????????????????????
            ///?????????????????????????????????????????
            ///?????????????????????????????????????????
            foreach (int id in Characters.Keys)
            {
                Advance[i] = Characters[id].XAdvance;
                Offset[i] = Characters[id].XOffset;
                UVSize[i] = Characters[id].XTextureWidth;
                UVPosition[i] = Characters[id].XTextureCoord;
                i++;
                Advance[i] = 0;
                Offset[i] = Characters[id].YOffset;
                UVSize[i] = Characters[id].YTextureHeight;
                UVPosition[i] = Characters[id].YTextureCoord;
                i++;
            }
            UniformBuffer.Bind();
            UniformBuffer.UpdateBufferIteam("Advance", Advance);
            UniformBuffer.UpdateBufferIteam("Offset", Advance);
            UniformBuffer.UpdateBufferIteam("UVSize", Advance);
            UniformBuffer.UpdateBufferIteam("UVPosition", Advance);


        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void infoParse(ref string[] recording)
        {
            for (int i = 1; i < recording.Length; i++)
            {
                string[] items = recording[i].Split('=');
                switch (items[0])
                {
                    case ("face"):
                        Info.Face = items[1];
                        break;
                    case ("size"):
                        Info.Size = int.Parse(items[1]);
                        break;
                    case ("bold"):
                        Info.Bold = int.Parse(items[1]);
                        break;
                    case ("italic"):
                        Info.Italic = int.Parse(items[1]);
                        break;
                    case ("charset"):
                        Info.Charset = items[1];
                        break;
                    case ("unicode"):
                        Info.Unicode = int.Parse(items[1]);
                        break;
                    case ("stretchH"):
                        Info.StretchH = int.Parse(items[1]);
                        break;
                    case ("smooth"):
                        Info.Smooth = int.Parse(items[1]);
                        break;
                    case ("aa"):
                        Info.Aa = int.Parse(items[1]);
                        break;
                    case ("padding"):
                        {
                            string[] temp = items[1].Split(',');
                            Info.Padding[0] = int.Parse(temp[0]);
                            Info.Padding[1] = int.Parse(temp[1]);
                            Info.Padding[2] = int.Parse(temp[2]);
                            Info.Padding[3] = int.Parse(temp[3]);
                            break;
                        }
                    case ("spacing"):
                        {
                            string[] temp = items[1].Split(',');
                            Info.Spacing[0] = int.Parse(temp[0]);
                            Info.Spacing[1] = int.Parse(temp[1]);
                            break;
                        }
                        //default:
                        //throw new System.ArgumentException("Parameter cannot be readed", items[0]);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void commonParse(ref string[] recording)
        {
            for (int i = 1; i < recording.Length; i++)
            {
                string[] items = recording[i].Split('=');
                switch (items[0])
                {
                    case ("lineHeight"):
                        Common.LineHeight = int.Parse(items[1]);
                        break;
                    case ("base"):
                        Common.BaseVal = int.Parse(items[1]);
                        break;
                    case ("scaleW"):
                        Common.ScaleW = int.Parse(items[1]);
                        break;
                    case ("scaleH"):
                        Common.ScaleH = int.Parse(items[1]);
                        break;
                    case ("pages"):
                        Common.Pages = int.Parse(items[1]);
                        break;
                    case ("packed"):
                        Common.Packed = int.Parse(items[1]);
                        break;
                    default:
                        throw new System.ArgumentException("Parameter cannot be readed", items[0]);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void charParse(ref string[] recording)
        {
            characters.Add(
                int.Parse(recording[1].Split('=')[1]),
                new Character(
                    int.Parse(recording[1].Split('=')[1]),
                    float.Parse(recording[2].Split('=')[1]) / Common.ScaleW,
                    float.Parse(recording[3].Split('=')[1]) / Common.ScaleH,
                    float.Parse(recording[4].Split('=')[1]) / Common.ScaleW,
                    float.Parse(recording[5].Split('=')[1]) / Common.ScaleH,
                    float.Parse(recording[6].Split('=')[1]) / Common.ScaleW,
                    float.Parse(recording[7].Split('=')[1]) / Common.ScaleH,
                    float.Parse(recording[8].Split('=')[1]) / Common.ScaleW,
                    int.Parse(recording[9].Split('=')[1]),
                    int.Parse(recording[10].Split('=')[1])));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void kerningParse(ref string[] recording)
        {
            kernings.Add(
                 kerningsCountIter,
                 new Kerning(
                     int.Parse(recording[1].Split('=')[1]),
                     int.Parse(recording[2].Split('=')[1]),
                     int.Parse(recording[3].Split('=')[1])));
            kerningsCountIter++;
        }
    }

    public class InfoFont
    {
        public string Face;
        public int Size;
        public int Bold;
        public int Italic;
        public string Charset;
        public int Unicode;
        public int StretchH;
        public int Smooth;
        public int Aa;
        public int[] Padding = new int[4];
        public int[] Spacing = new int[2];
    }

    public struct CommonFont
    {
        public int LineHeight;
        public int BaseVal;
        public int ScaleW;
        public int ScaleH;
        public int Pages;
        public int Packed;
    }

    public struct Kerning
    {
        private int first;
        private int second;
        private int amount;

        public int First
        {
            get { return first; }
        }
        public int Second
        {
            get { return second; }
        }
        public int Amount
        {
            get { return amount; }
        }
        public Kerning(int first, int second, int amount)
        {
            this.first = first;
            this.second = second;
            this.amount = amount;
        }
    }
}

