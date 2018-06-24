using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Net_Framework_3._5
{
    class Class4
    {
        static void Main22()
        {
            //首先启动文件加载器
            LoadManager lm = new LoadManager();
            //添加要处理的文件
            lm.LoadFiles(new WORDFile());
            //lm.LoadFiles(new PDFFile());
            //lm.LoadFiles(new JPGFile());
            //lm.LoadFiles(new AVIFile());
            foreach (Files file in lm.Files)
            {
                //if (file is FileType) //伪代码
                //{
                //    lm.OpenFile(file);
                //}
            }
            Console.ReadKey();
        }
    }
    interface IFileOpen
    {
        void Open();
    }
    abstract class Files : IFileOpen
    {
        private FileType fileType = FileType.doc;
        public FileType FileType
        {
            get { return fileType; }
        }
        public abstract void Open();
    }
    abstract class DocFile : Files
    {
        public int GetPageCount()
        {
            //计算文档页数
            return 1;
        }
    }
    abstract class ImageFile : Files
    {
        public void ZoomIn()
        {
            //放大比例
        }
        public void ZoomOut()
        {
            //缩小比例
        }
    }
    class WORDFile : DocFile
    {
        public override void Open()
        {
            Console.WriteLine("Open the WORD file.");
        }
    }
    class LoadManager
    {
        private IList<Files> files = new List<Files>();
        public IList<Files> Files
        {
            get { return files; }
        }
        public void LoadFiles(Files file)
        {
            files.Add(file);
        }
        //打开所有资料
        public void OpenAllFiles()
        {
            foreach (IFileOpen file in files)
            {
                file.Open();
            }
        }
        //打开单个资料
        public void OpenFile(IFileOpen file)
        {
            file.Open();
        }
        //获取文件类型
        public FileType GetFileType(string fileName)
        {
            //根据指定路径文件返回文件类型
            FileInfo fi = new FileInfo(fileName);
            return (FileType)Enum.Parse(typeof(FileType), fi.Extension);
        }
    }
    #region FileType
    enum FileType
    {
        doc, //Word 文档
        pdf, //PDF 文档
        txt, //文本文档
        ppt, //Powerpoint 文档
        jpg, //jpg 格式图片
        gif, //gif 格式图片
        mp3, //mp3 音频文件
        avi //avi 视频文件
    } 
    #endregion
}
