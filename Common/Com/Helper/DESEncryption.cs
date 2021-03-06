﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Common
{
    /// <summary> 3DES 加密 </summary>
    public class DESEncryption
    {
        public const string sKey = "G+g@G9THz+y).xkI2KBfl*{u";
        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        #region 3DES加密
        /// <summary> 3DES加密 </summary>
        public static string Encrypt3DES(string a_strString, string a_strKey = sKey)
        {
            using (TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider())
            {
                DES.Key = Encoding.UTF8.GetBytes(a_strKey);
                DES.Mode = CipherMode.ECB;

                ICryptoTransform DESEncrypt = DES.CreateEncryptor();

                byte[] Buffer = Encoding.UTF8.GetBytes(a_strString);

                return Convert.ToBase64String(DESEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
            }
        }
        #endregion

        #region 3DES解密
        /// <summary> 3DES解密 </summary>
        public static string Decrypt3DES(string a_strString, string a_strKey = sKey)
        {
            using (TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider())
            {
                DES.Key = Encoding.UTF8.GetBytes(a_strKey);
                DES.Mode = CipherMode.ECB;
                DES.Padding = PaddingMode.PKCS7;

                ICryptoTransform DESDecrypt = DES.CreateDecryptor();

                byte[] Buffer = Convert.FromBase64String(a_strString);
                return Encoding.UTF8.GetString(DESDecrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
            }
        }
        #endregion

        #region DES加密文件
        /// <summary> DES加密文件 </summary>
        public static bool EncryptDES(string inFilePath, string outFilePath, string encryptKey = sKey)
        {
            byte[] rgbIV = Keys;
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                //读入的流 
                FileStream inFs = new FileStream(inFilePath, FileMode.Open, FileAccess.Read);
                //待写的流 
                FileStream outFs = new FileStream(outFilePath, FileMode.OpenOrCreate, FileAccess.Write);
                outFs.SetLength(0);
                //创建一个变量来帮助读写 
                byte[] byteIn = new byte[100];   //临时存放读入的流 
                long readLen = 0;                //读入流的长度 
                long totalLen = inFs.Length;     //总共读入流的长度 
                int everyLen;                    //每次读入流动长度 
                //读入InFs，加密后写入OutFs 
                DES des = new DESCryptoServiceProvider();
                CryptoStream encStream = new CryptoStream(outFs, des.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                while (readLen < totalLen)
                {
                    everyLen = inFs.Read(byteIn, 0, 100);
                    encStream.Write(byteIn, 0, everyLen);
                    readLen = readLen + everyLen;
                }
                encStream.Close();
                outFs.Close();
                inFs.Close();
                return true;//加密成功 
            }
            catch
            {
                return false;//加密失败   
            }
        }
        #endregion

        #region DES解密文件
        /// <summary> DES解密文件 </summary>
        public static bool DecryptDES(string inFilePath, string outFilePath, string decryptKey = sKey)
        {
            byte[] rgbIV = Keys;
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey.Substring(0, 8));
                //读入的流 
                FileStream inFs = new FileStream(inFilePath, FileMode.Open, FileAccess.Read);
                //待写的流 
                FileStream outFs = new FileStream(outFilePath, FileMode.OpenOrCreate, FileAccess.Write);
                outFs.SetLength(0);
                //创建一个变量来帮助读写 
                byte[] byteIn = new byte[100];   //临时存放读入的流 
                long readLen = 0;                //读入流的长度 
                long totalLen = inFs.Length;     //总共读入流的长度 
                int everyLen;                    //每次读入流动长度 
                //读入InFs，解密后写入OutFs 
                DES des = new DESCryptoServiceProvider();
                CryptoStream encStream = new CryptoStream(outFs, des.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                while (readLen < totalLen)
                {
                    everyLen = inFs.Read(byteIn, 0, 100);
                    encStream.Write(byteIn, 0, everyLen);
                    readLen = readLen + everyLen;
                }
                encStream.Close();
                outFs.Close();
                inFs.Close();
                return true;//解密成功 
            }
            catch
            {
                return false;//解密失败   
            }

        }
        #endregion

        #region 3DES密钥生成
        /// <summary> 3DES密钥生成 </summary>
        private static string Create3DESKey(int length = 24)
        {
            string str = "0123456789AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz~!@#$%^&*()_+-=<>{}[]/*.?";
            var sb = new System.Text.StringBuilder();
            Random random = new Random();

            for (int i = 0; i < length; i++)
                sb.Append(str[random.Next(0, str.Length)]);

            return sb.ToString();
        }
        #endregion
    }
}
