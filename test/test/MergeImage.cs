using System;
using System.IO;

namespace test
{
    public class MergeImage
    {
        public void GenerateImage()
        {
            //多张图片合成
            var rootPath = AppContext.BaseDirectory;
            //获取底图
            var imagePath = Path.Combine(rootPath, "images", "底图1.jpg");
            System.Drawing.Image image1 = System.Drawing.Image.FromFile(imagePath);
            //获取合并的图
            var imagePath1 = Path.Combine(rootPath, "images", "无城市logo.png");
            System.Drawing.Image image2 = System.Drawing.Image.FromFile(imagePath1);
            //创建画笔
            var graphics = System.Drawing.Graphics.FromImage(image1);
            //合并图片跟背景
            graphics.DrawImage(image2, new System.Drawing.Point(150, 520));
            //写字 w:854 h:881
            // var font = new System.Drawing.Font("微软雅黑", 45.66f, System.Drawing.FontStyle.Bold);
            // graphics.DrawString("上海", font, System.Drawing.Brushes.Black, 150 + 186, 520 + 212);
            var newImagePath = Path.Combine(rootPath, "images", DateTime.Now.ToString("yyyyMMddHHmmss") + ".png");

            using (System.IO.Stream file = new System.IO.FileStream(newImagePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                image1.Save(file, System.Drawing.Imaging.ImageFormat.Png);
            }
        }
    }
}