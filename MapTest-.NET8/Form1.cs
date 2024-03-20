using System.Drawing.Drawing2D;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Drawing;
using System.Drawing.Text;

namespace MapTest_.NET8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            PrivateFontCollection pfc = new();
            pfc.AddFontFile("Koruri-Regular.ttf");
            font = pfc.Families[0];
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Draw_Japan();
            Draw_World();
        }

        /// <summary>
        /// �z�F�ݒ�
        /// </summary>
        public static Config_Color color = new();
        public static Config_Map config = new();
        public static FontFamily font;

#pragma warning disable CS8602 // null �Q�Ƃ̉\����������̂̋t�Q�Ƃł��B
#pragma warning disable CS8604 // Null �Q�ƈ����̉\��������܂��B
        public void Draw_World()
        {
            var mapImg = new Bitmap(config.MapSize * 16 / 9, config.MapSize);
            var zoomW = config.MapSize / (config.LonEnd - config.LonSta);
            var zoomH = config.MapSize / (config.LatEnd - config.LatSta);
            var mapjson = JsonNode.Parse(File.ReadAllText("map-world.geojson"));
            var g = Graphics.FromImage(mapImg);
            g.Clear(color.Map.Sea);
            var gPath = new GraphicsPath();
            gPath.StartFigure();
            foreach (var mapjson_feature in mapjson["features"].AsArray())
            {
                if (mapjson_feature["geometry"] == null)
                    continue;
                var points = mapjson_feature["geometry"]["coordinates"][0].AsArray().Select(mapjson_coordinates => new Point((int)(((double)mapjson_coordinates[0] - config.LonSta) * zoomW), (int)((config.LatEnd - (double)mapjson_coordinates[1]) * zoomH))).ToArray();
                if (points.Length > 2)
                    gPath.AddPolygon(points);
            }
            g.FillPath(new SolidBrush(color.Map.World), gPath);

            mapjson = JsonNode.Parse(File.ReadAllText("map-jp.geojson"));
            gPath.Reset();
            gPath.StartFigure();
            foreach (var mapjson_features in mapjson["features"].AsArray())
            {
                if ((string?)mapjson_features["geometry"]["type"] == "Polygon")
                {
                    var points = mapjson_features["geometry"]["coordinates"][0].AsArray().Select(mapjson_coordinates => new Point((int)(((double)mapjson_coordinates[0] - config.LonSta) * zoomW), (int)((config.LatEnd - (double)mapjson_coordinates[1]) * zoomH))).ToArray();
                    if (points.Length > 2)
                        gPath.AddPolygon(points);
                }
                else
                {
                    foreach (var mapjson_coordinateses in mapjson_features["geometry"]["coordinates"].AsArray())
                    {
                        var points = mapjson_coordinateses[0].AsArray().Select(mapjson_coordinates => new Point((int)(((double)mapjson_coordinates[0] - config.LonSta) * zoomW), (int)((config.LatEnd - (double)mapjson_coordinates[1]) * zoomH))).ToArray();
                        if (points.Length > 2)
                            gPath.AddPolygon(points);
                    }
                }
            }
            g.FillPath(new SolidBrush(color.Map.Japan), gPath);
            g.DrawPath(new Pen(color.Map.Japan_Border, config.MapSize / 1080f), gPath);
            var mdsize = g.MeasureString("�n�}�f�[�^:�C�ے�, Natural Earth", new Font(font, config.MapSize / 28, GraphicsUnit.Pixel));
            g.DrawString("�n�}�f�[�^:�C�ے�, Natural Earth", new Font(font, config.MapSize / 28, GraphicsUnit.Pixel), new SolidBrush(color.Text), config.MapSize - mdsize.Width, config.MapSize - mdsize.Height);
            g.Dispose();
            BackgroundImage = mapImg;
        }
#pragma warning restore CS8602 // null �Q�Ƃ̉\����������̂̋t�Q�Ƃł��B
#pragma warning restore CS8604 // Null �Q�ƈ����̉\��������܂��B


#pragma warning disable CS8602 // null �Q�Ƃ̉\����������̂̋t�Q�Ƃł��B
#pragma warning disable CS8604 // Null �Q�ƈ����̉\��������܂��B
        public void Draw_Japan()
        {
            var mapImg = new Bitmap(config.MapSize * 16 / 9, config.MapSize);
            var zoomW = config.MapSize / (config.LonEnd - config.LonSta);
            var zoomH = config.MapSize / (config.LatEnd - config.LatSta);
            var mapjson = JsonNode.Parse(File.ReadAllText("map-jp.geojson"));
            var g = Graphics.FromImage(mapImg);
            g.Clear(color.Map.Sea);
            var gPath = new GraphicsPath();
            gPath.StartFigure();
            foreach (var mapjson_features in mapjson["features"].AsArray())
            {
                if ((string?)mapjson_features["geometry"]["type"] == "Polygon")
                {
                    var points = mapjson_features["geometry"]["coordinates"][0].AsArray().Select(mapjson_coordinates => new Point((int)(((double)mapjson_coordinates[0] - config.LonSta) * zoomW), (int)((config.LatEnd - (double)mapjson_coordinates[1]) * zoomH))).ToArray();
                    if (points.Length > 2)
                        gPath.AddPolygon(points);
                }
                else
                {
                    foreach (var mapjson_coordinateses in mapjson_features["geometry"]["coordinates"].AsArray())
                    {
                        var points = mapjson_coordinateses[0].AsArray().Select(mapjson_coordinates => new Point((int)(((double)mapjson_coordinates[0] - config.LonSta) * zoomW), (int)((config.LatEnd - (double)mapjson_coordinates[1]) * zoomH))).ToArray();
                        if (points.Length > 2)
                            gPath.AddPolygon(points);
                    }
                }
            }
            g.FillPath(new SolidBrush(color.Map.Japan), gPath);
            g.DrawPath(new Pen(color.Map.Japan_Border, config.MapSize / 1080f), gPath);
            var mdsize = g.MeasureString("�n�}�f�[�^:�C�ے�", new Font(font, config.MapSize / 28, GraphicsUnit.Pixel));
            g.DrawString("�n�}�f�[�^:�C�ے�", new Font(font, config.MapSize / 28, GraphicsUnit.Pixel), new SolidBrush(color.Text), config.MapSize - mdsize.Width, config.MapSize - mdsize.Height);
            g.Dispose();
            BackgroundImage = mapImg;
        }
#pragma warning restore CS8602 // null �Q�Ƃ̉\����������̂̋t�Q�Ƃł��B
#pragma warning restore CS8604 // Null �Q�ƈ����̉\��������܂��B
    }

    public class Config_Map
    {
        /// <summary>
        /// �摜�̍���
        /// </summary>
        public int MapSize { get; set; } = 1080;

        /// <summary>
        /// �ܓx�̎n�_
        /// </summary>
        public double LatSta { get; set; } = 20;

        /// <summary>
        /// �ܓx�̏I�_
        /// </summary>
        public double LatEnd { get; set; } = 50;

        /// <summary>
        /// �o�x�̎n�_
        /// </summary>
        public double LonSta { get; set; } = 120;

        /// <summary>
        /// �o�x�̏I�_
        /// </summary>
        public double LonEnd { get; set; } = 150;
    }

    /// <summary>
    /// �`��F�̐ݒ�
    /// </summary>
    public class Config_Color
    {
        /// <summary>
        /// �n�}�̐F
        /// </summary>
        public MapColor Map { get; set; } = new MapColor();

        /// <summary>
        /// �n�}�̐F
        /// </summary>
        public class MapColor
        {
            /// <summary>
            /// �C�m�̓h��Ԃ��F
            /// </summary>
            public Color Sea { get; set; } = Color.FromArgb(30, 30, 60);

            /// <summary>
            /// ���E(���{����)�̓h��Ԃ��F
            /// </summary>
            public Color World { get; set; } = Color.FromArgb(100, 100, 150);
            /*
            /// <summary>
            /// ���E(���{����)�̋��E���F
            /// </summary>
            public Color World_Border { get; set; }
            */
            /// <summary>
            /// ���{�̓h��Ԃ��F
            /// </summary>
            public Color Japan { get; set; } = Color.FromArgb(90, 90, 120);

            /// <summary>
            /// ���{�̋��E���F
            /// </summary>
            public Color Japan_Border { get; set; } = Color.FromArgb(127, 255, 255, 255);
        }

        /// <summary>
        /// �E�������w�i�F
        /// </summary>
        public Color InfoBack { get; set; } = Color.FromArgb(30, 60, 90);

        /// <summary>
        /// �E���������e�L�X�g�F
        /// </summary>
        public Color Text { get; set; } = Color.FromArgb(255, 255, 255);

        /// <summary>
        /// �k���~�̓����x
        /// </summary>
        public int Hypo_Alpha { get; set; } = 204;
    }
}
