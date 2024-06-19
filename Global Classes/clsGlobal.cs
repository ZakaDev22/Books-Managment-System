using IslamIsLife.Main_And_Login_Forms;
using IslamIsLife_Buisness;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IslamIsLife.Global_Classes
{
    public class clsGlobal
    {
        public static clsUsers CurrentUser;

        public static LoginForm LoginForm;

        public static bool RememberUsernameAndPassword(string Username, string Password)
        {

            try
            {
                //this will get the current project directory folder.
                string currentDirectory = Directory.GetCurrentDirectory();


                // Define the path to the text file where you want to save the data
                string filePath = currentDirectory + "\\data.txt";

                //incase the username is empty, delete the file
                if (Username == "" && File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;

                }

                // concatonate username and passwrod withe seperator.
               if(Username != "" && Password != "")
                {
                    string dataToSave = Username + "#//#" + Password;

                    // Create a StreamWriter to write to the file
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        // Write the data to the file
                        writer.WriteLine(dataToSave);

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }

            return false;
        }

        public static bool GetStoredCredential(ref string Username, ref string Password)
        {
            //this will get the stored username and password and will return true if found and false if not found.
            try
            {
                //gets the current project's directory
                string currentDirectory = Directory.GetCurrentDirectory();

                // Path for the file that contains the credential.
                string filePath = currentDirectory + "\\data.txt";

                // Check if the file exists before attempting to read it
                if (File.Exists(filePath))
                {
                    // Create a StreamReader to read from the file
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        // Read data line by line until the end of the file
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            Console.WriteLine(line); // Output each line of data to the console
                            string[] result = line.Split(new string[] { "#//#" }, StringSplitOptions.None);

                            Username = result[0];
                            Password = result[1];
                        }
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }

        }


        //
        /// <summary>
        /// 
        /// from this part its the chat gbt Code for Saving User Name And Password
        /// 
        /// </summary>
        //


        private static string filePath = Directory.GetCurrentDirectory() + "\\data.txt";

        // Save credentials to file
        public static void SaveCredentials(string username, string password)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    string encryptedPassword = EncryptString(password);
                    writer.WriteLine($"{username}:{encryptedPassword}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving credentials: {ex.Message}");
            }
        }

        // Retrieve credentials from file
        public static bool RetrieveCredentials(ref string username, ref string password)
        {

            try
            {
                if (File.Exists(filePath))
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        string line = reader.ReadLine();
                        if (line != null)
                        {
                            string[] parts = line.Split(':');
                            if (parts.Length == 2)
                            {
                                username = parts[0];
                                string encryptedPassword = parts[1];
                                password = DecryptString(encryptedPassword);
                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving credentials: {ex.Message}");
            }

            return false;
        }

        // Encrypt password
        private static string EncryptString(string input)
        {
            using (Aes aesAlg = Aes.Create())
            {
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(input);
                        }
                        return Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }
        }

        // Decrypt password
        private static string DecryptString(string input)
        {
            byte[] cipherBytes = Convert.FromBase64String(input);

            using (Aes aesAlg = Aes.Create())
            {
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherBytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
