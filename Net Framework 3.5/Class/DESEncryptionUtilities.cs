using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Net_Framework_3._5
{
    public class DESEncryptionUtilities
    {
        public const string sKey = "EeiixxASDIFRNOYV1.0LockH";
        private const string carKey = "aisinoaisino";
        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        ///   <summary> 
        ///   DES加密文件 
        ///   </summary> 
        ///   <param   name= "inFilePath "> 待加密文件 </param> 
        ///   <param   name= "outFilePath "> 加密后的文件 </param> 
        ///   <param   name= "encryptKey "> 加密密钥 </param> 
        ///   <returns> </returns> 
        public static bool EncryptDES(string inFilePath, string outFilePath, string encryptKey)
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
        ///   <summary> 
        ///   DES解密文件 
        ///   </summary> 
        ///   <param   name= "inFilePath "> 待解密文件 </param> 
        ///   <param   name= "outFilePath "> 待加密文件 </param> 
        ///   <param   name= "decryptKey "> 解密密钥 </param> 
        ///   <returns> </returns> 
        public static bool DecryptDES(string inFilePath, string outFilePath, string decryptKey)
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

        /// <summary>
        /// 3DES解密
        /// </summary>
        /// <param name="a_strString"></param>
        /// <param name="a_strKey"></param>
        /// <returns></returns>
        public static string Decrypt3DES(string a_strString, string a_strKey)
        {
            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();

            //  DES.Key = ASCIIEncoding.ASCII.GetBytes(a_strKey);
            DES.Key = Encoding.UTF8.GetBytes(a_strKey);
            DES.Mode = CipherMode.ECB;
            DES.Padding = PaddingMode.PKCS7;

            ICryptoTransform DESDecrypt = DES.CreateDecryptor();
            string result = "";
            try
            {
                byte[] Buffer = Convert.FromBase64String(a_strString);
                //result = ASCIIEncoding.ASCII.GetString(DESDecrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
                result = Encoding.UTF8.GetString(DESDecrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
            }
            catch (Exception)
            {

            }
            return result;
        }

        /// <summary>
        /// 3DES加密
        /// </summary>
        /// <param name="a_strString"></param>
        /// <param name="a_strKey"></param>
        /// <returns></returns>
        public static string Encrypt3DES(string a_strString, string a_strKey)
        {
            //16、24位加密钥匙,
            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();

            DES.Key = Encoding.ASCII.GetBytes(a_strKey);

            DES.Mode = CipherMode.ECB;

            ICryptoTransform DESEncrypt = DES.CreateEncryptor();

            byte[] Buffer = Encoding.UTF8.GetBytes(a_strString);
            return Convert.ToBase64String(DESEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
        }

        /// <summary>
        /// MD5加密实现
        /// </summary>
        /// <param name="strToEncrypt"></param>
        /// <returns></returns>
        public static string MD5Encrypt(string strToEncrypt)
        {
            byte[] data = ASCIIEncoding.ASCII.GetBytes(strToEncrypt);

            // This is one implementation of the abstract class MD5.
            MD5 md5 = new MD5CryptoServiceProvider();

            byte[] result = md5.ComputeHash(data);

            StringBuilder ret = new StringBuilder();
            foreach (byte b in result)
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }
    }
}
