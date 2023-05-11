using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYSharp.SM;
namespace HSSB
{

    public class SM2
    {

        public static string SM2Console(string publicKey, string content)
        {
            //公钥
            // string publickey = "04C7E34D7FB4EECE60C29ED53867F98AA072C0B562787BCA312919EB3E12753BAC462AC485866DC7264CCF03A47C975807674B5684596A96814EC8E59AE17A2974";
            //私钥
            // string privatekey = "";
            //生成公钥和私钥
            // SM2Utils.GenerateKeyPair(out publickey, out privatekey);

            System.Console.Out.WriteLine("加密明文: " + content);
            System.Console.Out.WriteLine("publickey：" + publicKey);
            //开始加密
            var cipherText = SM2Utils.Encrypt_Hex(publicKey, content, Encoding.Default);

            System.Console.Out.WriteLine("密文: " + cipherText);
            return cipherText;
            // System.Console.Out.WriteLine("privatekey：" + privatekey);
            //解密
            // string plainText = SM2Utils.Decrypt(privatekey, cipherText);
            // System.Console.Out.WriteLine("明文: " + plainText);
            // Console.ReadLine();
        }

    }
}

