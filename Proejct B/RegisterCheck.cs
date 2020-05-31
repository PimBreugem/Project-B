using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.XPath;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.IO;

namespace RegisterCheck
{
    class RegisterCheck
    {
        public static string Encrypt(string message)
        {
            char[] chararray = message.ToCharArray();
            string EncryptedString = "";
            int offset = (int)'a';
            for (int i = 0; i < chararray.Length; i++)
            {
                if (chararray[i] >= 'a' && chararray[i] <= 'z')
                {
                    EncryptedString += (char)(((((int)chararray[i]) - offset + 26) % 26) + offset);
                }
                else
                {
                    EncryptedString += chararray[i];
                }
            }
            return EncryptedString;
        }
        public static string Decrypt(string message)
        {
            char[] chararray = message.ToCharArray();
            string DecryptedString = "";
            int offset = (int)'a';
            for (int i = 0; i < chararray.Length; i++)
            {
                if (chararray[i] >= 'a' && chararray[i] <= 'z')
                {
                    DecryptedString += (char)(((((int)chararray[i]) - offset - 26 + 26) % 26) + offset);
                }
                else
                {
                    DecryptedString += chararray[i];
                }
            }
            return DecryptedString;
        }
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
        public static bool IsValidUsername(string username) {
            if (string.IsNullOrWhiteSpace(username))
                return false;
            try
            {
                return Regex.IsMatch(username,
                    @"^(?=[a-zA-Z])[-\w.]{0,23}([a-zA-Z\d]|(?<![-.])_)$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        //public static bool RegisterAccount(string Username, string Password, string Email) {

            //JsonConverter.NewUser()
        //}
    }
}
