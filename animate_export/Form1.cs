using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using animate_export.Properties;
using Newtonsoft.Json;
using Image = System.Drawing.Image;
using ListViewItem = System.Windows.Forms.ListViewItem;
using Timer = System.Timers.Timer;

namespace animate_export
{
    public partial class Form1 : Form
    {

        private int page;
        private string openDirectory;
        private string directory, file;
        private RGSSReader rgssReader;
        private List<Animation> animations;
        private int id, index;

        private Bitmap bitmap;
        private List<Bitmap> bitmaps, bitmap_revs;

        private Timer timer;
        private Bitmap canvasMap;
        private Graphics graphics;

        public Form1()
        {
            InitializeComponent();
            rgssReader = new RGSSReader();
            canvasMap = new Bitmap(canvas.Width, canvas.Height);
            graphics = Graphics.FromImage(canvasMap);

            timer = new Timer();
            timer.AutoReset = true;
            timer.Interval = 50;
            timer.Elapsed += drawAnimate;

            showStep1();
        }

        private void resetStepPanel()
        {
            step1.Visible = false;
            step2.Visible = false;
            step3.Visible = false;
            step4.Visible = false;
            step1Label.ForeColor = Color.Black;
            step2Label.ForeColor = Color.Black;
            step3Label.ForeColor = Color.Black;
            step4Label.ForeColor = Color.Black;

            prevButton.Visible = true;
            nextButton.Visible = true;

            index = 0;
        }

        public void showStep1()
        {
            page = 1;
            resetStepPanel();
            step1.Visible = true;
            step1Label.ForeColor = Color.Red;
            prevButton.Visible = false;

            if (file == null)
            {
                selectFileLabel.Text = "请选择一个Animations.rxdata文件";
                nextButton.Enabled = false;
            }
            else
            {
                selectFileLabel.Text = file;
                nextButton.Enabled = true;
            }

        }

        public void showStep2()
        {

            page = 2;
            resetStepPanel();
            step2.Visible = true;
            step2Label.ForeColor = Color.Red;

            // Draw List
            listView.BeginUpdate();
            listView.Items.Clear();
            foreach (var animation in animations)
            {
                var listViewItem = new ListViewItem();
                listViewItem.Text = Convert.ToString(animation.id);
                listViewItem.SubItems.Add(animation.name);
                listViewItem.SubItems.Add(animation.animation_name);
                listViewItem.SubItems.Add(Convert.ToString(animation.frame_max));
                listView.Items.Add(listViewItem);
            }
            listView.SelectedIndices.Clear();
            listView.SelectedIndices.Add(id);
            listView.EndUpdate();

            listView.EnsureVisible(id);

        }

        public void showStep3()
        {
            page = 3;
            resetStepPanel();
            step3.Visible = true;
            step3Label.ForeColor = Color.Red;
            
            timer.Start();
        }

        public void showStep4()
        {
            page = 4;
            resetStepPanel();
            step4.Visible = true;
            step4Label.ForeColor = Color.Red;
            nextButton.Visible = false;

        }

        private void selectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog=new OpenFileDialog();
            dialog.Title = "读取动画";
            dialog.Filter = "RMXP数据文件(*.rxdata)|*.rxdata";

            dialog.AddExtension = true;
            dialog.DefaultExt = ".rxdata";

            if (openDirectory != null)
                dialog.InitialDirectory = openDirectory;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (!"Animations.rxdata".Equals(dialog.SafeFileName))
                {
                    MessageBox.Show("请选择一个Animations.rxdata文件！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                // 检测是否合法
                rgssReader.setPath(dialog.FileName);
                if (rgssReader.GetAnimations() == null)
                {
                    MessageBox.Show("不是一个有效的Animations.rxdata文件！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

                // 检测是否在Data目录下
                openDirectory = Path.GetDirectoryName(dialog.FileName);
                string dir = Path.GetFullPath(openDirectory + @"\..\Graphics\Animations");

                if (!Directory.Exists(dir))
                {
                    MessageBox.Show("Graphics/Animations文件夹不存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    file = null;
                    directory = null;
                    animations = null;
                    return;
                }

                file = dialog.FileName;
                directory = dir;
                animations = rgssReader.GetAnimations();

                showStep1();
            }
        }

        private bool checkValidAnimate()
        {
            Animation animation = animations[id];

            // File exists
            if ("".Equals(animation.name) || "".Equals(animation.animation_name))
                return false;
            string pngPath = Path.Combine(directory, animation.animation_name + ".png");
            if (!File.Exists(pngPath))
            {
                MessageBox.Show(pngPath + "不存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }

            // Check position
            if (animation.position == 3)
            {
                MessageBox.Show("“位置：画面”不被支持！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }

            rgssReader.selectId(id);

            if (animation.frames == null)
            {
                MessageBox.Show("无效的动画", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }

            // 检查是否存在“旋转角度”、“左右翻转”、“合成方式”
            /*
            string reason = null;
            foreach (var animationFrame in animation.frames)
            {
                foreach (var frameInfo in animationFrame.cell_data)
                {
                    if (reason != null) break;
                    // if (frameInfo.angle != 0) reason = "不支持带旋转角度的帧！";
                    // if (frameInfo.mirror) reason = "不支持带翻转的帧";
                    // if (frameInfo.blend != 1) reason = "只支持合成方式：加法！";
                }
            }
            if (reason != null)
            {
                MessageBox.Show(reason, "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
             * */

            // bitmap = new Bitmap(pngPath);
            Bitmap _bitmap = (Bitmap)Image.FromFile(pngPath);

            if (_bitmap.Width % 192 != 0 || _bitmap.Height % 192 != 0)
            {
                MessageBox.Show("目标图片的宽或高不是192的倍数！", "加载失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _bitmap.Dispose();
                return false;
            }

            if (bitmap != null) bitmap.Dispose();
            bitmap = new Bitmap(_bitmap.Width, _bitmap.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.DrawImage(_bitmap, new Rectangle(0, 0, _bitmap.Width, _bitmap.Height), 0, 0, _bitmap.Width, _bitmap.Height, GraphicsUnit.Pixel);
            graphics.Dispose();
            _bitmap.Dispose();

            // 清空原有的
            if (bitmaps == null) bitmaps = new List<Bitmap>();
            else
            {
                bitmaps.ForEach(x => x.Dispose());
                bitmaps.Clear();
            }
            if (bitmap_revs == null) bitmap_revs = new List<Bitmap>();
            else
            {
                bitmap_revs.ForEach(x => x.Dispose());
                bitmap_revs.Clear();
            }

            int hue = animation.animation_hue;

            BitmapWrapper bitmapWrapper = new BitmapWrapper(bitmap);

            for (int y = 0; y < bitmap.Height / 192; y++)
            {
                for (int x = 0; x < bitmap.Width / 192; x++)
                {

                    Bitmap map = new Bitmap(192, 192);
                    BitmapWrapper mapWrapper = new BitmapWrapper(map);

                    Bitmap map_rev = new Bitmap(192, 192);
                    BitmapWrapper mapWrapper_rev = new BitmapWrapper(map_rev);

                    for (int u = 0; u < 192; u++)
                    {
                        for (int v = 0; v < 192; v++)
                        {
                            Color color = Util.addHue(bitmapWrapper.GetPixel(192 * x + u, 192 * y + v), hue);
                            mapWrapper.SetPixel(u,v,color);
                            mapWrapper_rev.SetPixel(191-u,v,color);
                            //mapWrapper.SetPixel(new Point(u,v), Util.addHue(bitmapWrapper.GetPixel(new Point(192*x+u, 192*y+v)), hue));
                            // map.SetPixel(u, v, Util.addHue(bitmap.GetPixel(192 * x + u, 192 * y + v), hue));

                        }
                    }
                    mapWrapper.UnWrapper();
                    mapWrapper_rev.UnWrapper();

                    bitmaps.Add(map);
                    bitmap_revs.Add(map_rev);
                }
            }

            bitmapWrapper.UnWrapper();

            return true;
        }

        private void drawAnimate(object sender, EventArgs e)
        {
            if (page != 3)
            {
                graphics.Clear(Color.Transparent);
                timer.Stop();
                return;
            }

            int standardX = canvas.Width/2, standardY = canvas.Height/2;

            // 清空graphics
            graphics.Clear(Color.Transparent);
            // 绘制enemy
            graphics.DrawImage(Resources.enemy, standardX-16, standardY-16);

            // 进行绘制
            Animation animation = animations[id];

            if (index >= animation.frame_max) index = 0;

            Animation.Frame frame = animation.frames[index];
            ColorMatrix matrix = new ColorMatrix();
            ImageAttributes attributes = new ImageAttributes();

            foreach (var frameInfo in frame.cell_data)
            {
                if (frameInfo.type<0||frameInfo.type>=bitmaps.Count) continue;
                int centerX = standardX + frameInfo.xpos, centerY = standardY + frameInfo.ypos;

                if (animation.position == 0) centerY -= 16;
                if (animation.position == 2) centerY += 16;

                int width = 192 * frameInfo.zoom / 100;

                matrix.Matrix33 = frameInfo.opacity / 255f;
                attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                var image = frameInfo.mirror ? bitmap_revs[frameInfo.type] : bitmaps[frameInfo.type];

                var angle = frameInfo.angle;
                if (angle > 0)
                {
                    graphics.TranslateTransform(centerX, centerY);
                    graphics.RotateTransform(360-angle);
                    graphics.TranslateTransform(-centerX,-centerY);
                }
                graphics.DrawImage(image, new Rectangle(centerX - width / 2, centerY - width / 2, width, width), 0, 0, 192, 192, GraphicsUnit.Pixel, attributes);
                graphics.ResetTransform();

            }

            canvas.Image = canvasMap;

            index++;

        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            if (page == 1)
            {
                if (file == null || animations == null)
                {
                    MessageBox.Show("尚未选择Animations.rxdata文件！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    showStep1();
                    return;
                }
                id = 0;
                showStep2();
            }
            else if (page == 2)
            {
                if (listView.SelectedIndices.Count == 0)
                    return;

                id = listView.SelectedIndices[0];
                if (!checkValidAnimate()) return;

                showStep3();

                //if (bitmaps!=null)
                //    Console.WriteLine(bitmaps.Count);

            }
            else if (page == 3)
            {
                Animation animation = animations[id];
                string se = animation.se;
                if (se.Length == 0)
                {
                    checkBox1.Checked = false;
                    textBox1.Text = "";
                    textBox1.Enabled = false;
                }
                else
                {
                    checkBox1.Checked = true;
                    if (!se.Contains("."))
                        se = se + ".ogg";
                    textBox1.Text = se;
                    textBox1.Enabled = true;
                }
                showStep4();
            }
        }

        private void prevButton_Click(object sender, EventArgs e)
        {
            if (page == 2)
            {
                showStep1();
            }
            else if (page == 3)
            {
                showStep2();
            }
            else if (page == 4)
            {
                showStep3();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            File.Delete("Animations.rxdata");
            file = null;
            directory = null;
            rgssReader.setPath(null);
        }

        private string export(int ratio)
        {
            List<string> list=new List<string>();
            for (int i=0;i<bitmaps.Count;i++)
                list.Add("");

            Animation animation = animations[id];

            AnimationExport animationExport=new AnimationExport();
            animationExport.ratio = ratio;
            animationExport.frame_max = animation.frame_max;
            animationExport.frames=new List<List<List<int>>>();
            animationExport.se = checkBox1.Checked ? textBox1.Text : "";

            foreach (var animationFrame in animation.frames)
            {
                List<List<int>> frame=new List<List<int>>();
                foreach (var frameInfo in animationFrame.cell_data)
                {
                    int type = frameInfo.type;
                    if (type < 0) continue;
                    if ("".Equals(list[type])) // compress png
                    {
                        list[type] = Util.compressPng(bitmaps[type], ratio);
                    }
                    List<int> info=new List<int>();
                    info.Add(type); // 0-index
                    info.Add(frameInfo.xpos); // 1-xpos
                    info.Add(frameInfo.ypos+16*(animation.position-1)); // 2-ypos
                    info.Add(frameInfo.zoom); // 3-zoom
                    info.Add(frameInfo.opacity); // 4-opacity
                    info.Add(frameInfo.mirror?1:0); // 5-mirror
                    info.Add(frameInfo.angle); // 6-angle
                    frame.Add(info);
                }
                animationExport.frames.Add(frame);
            }

            animationExport.bitmaps = list;

            return JsonConvert.SerializeObject(animationExport);

        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog=new SaveFileDialog();
            saveFileDialog.Title = "导出动画到文件";
            saveFileDialog.Filter = "动画文件(*.animate)|*.animate";
            saveFileDialog.AddExtension = true;
            saveFileDialog.DefaultExt = ".animate";
            // saveFileDialog.InitialDirectory = Directory.GetCurrentDirectory();

            string currentDirectory = Directory.GetCurrentDirectory();

            string[] possibleDir = {".", "..", "project", "..\\project"};

            saveFileDialog.InitialDirectory = currentDirectory;
            foreach (string s in possibleDir)
            {
                if (Directory.Exists(Path.GetFullPath(currentDirectory + "\\" + s + "\\animates")))
                {
                    saveFileDialog.InitialDirectory = Path.GetFullPath(currentDirectory + "\\" + s + "\\animates");
                    break;
                }
            }

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {

                    string content = export(2);
                    if (content.Length > 250 * 1024)
                    {
                        content = export(4);
                    }
                    else if (content.Length > 120 * 1024)
                    {
                        content = export(3);
                    }
                    else if (content.Length < 40 * 1024)
                    {
                        content = export(1);
                    }

                    File.WriteAllText(saveFileDialog.FileName, content);
                    MessageBox.Show("已成功导出动画至" + saveFileDialog.FileName, "导出成功", MessageBoxButtons.OK,
                        MessageBoxIcon.Asterisk);
                }
                catch (Exception ee)
                {
                    Console.WriteLine(ee);
                    MessageBox.Show("保存失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = checkBox1.Checked;
        }

    }
}
