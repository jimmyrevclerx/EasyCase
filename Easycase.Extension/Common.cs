using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Easycase.Extension
{
    public static class Common
    {
        static Random random = new Random();
        public static List<string> GetTimeIntervals()
        {
            List<string> timeIntervals = new List<string>();
            TimeSpan startTime = new TimeSpan(0, 0, 0);
            DateTime startDate = new DateTime(DateTime.MinValue.Ticks); // Date to be used to get shortTime format.
            for (int i = 0; i < 96; i++)
            {
                int minutesToBeAdded = 15 * i;      // Increasing minutes by 15 minutes interval
                TimeSpan timeToBeAdded = new TimeSpan(0, minutesToBeAdded, 0);
                TimeSpan t = startTime.Add(timeToBeAdded);
                DateTime result = startDate + t;
                string hours = result.ToString("HH:mm");
                if (hours == "00.00")
                    hours = "12.00";
                else if (hours == "00.15")
                    hours = "12.15";
                else if (hours == "00.30")
                    hours = "12.30";
                else if (hours == "00.45")
                    hours = "12.45";
                string timetype = result.ToString("tt");
                hours = hours.Replace(".", ":");
                timeIntervals.Add(hours + " " + timetype);// Use Date.ToShortTimeString() method to get the desired format                
            }
            return timeIntervals;
        }
        public static List<BusinessHoursModal> GetConsultinghours()
        {
            List<BusinessHoursModal> timeIntervals = new List<BusinessHoursModal>();
            int max = 2;
            int totallength = ((max * 60) / 5) + 1;
            int number = 1;
            for (int i = 1; i <= totallength; i++)
            {
                string timestring = string.Empty;
                var timeSpan = TimeSpan.FromMinutes(number);
                int hh = timeSpan.Hours;
                int mm = timeSpan.Minutes;
                if (hh == 0)
                    timestring = mm + " min";
                else if (mm == 0)
                    timestring = hh + " hour";
                else
                    timestring = string.Format("{0} hour : {1} min", hh, mm);
                BusinessHoursModal bLBusinessHoursModal = new BusinessHoursModal();
                bLBusinessHoursModal.Text = timestring;
                bLBusinessHoursModal.Value = number;
                timeIntervals.Add(bLBusinessHoursModal);
                number = i * 5;
            }
            return timeIntervals;
        }
        public static List<BusinessHoursModal> GetWaitingHours()
        {
            List<BusinessHoursModal> timeIntervals = new List<BusinessHoursModal>();
            int max = 2;
            int totallength = ((max * 60) / 5);
            int number = 1;
            for (int i = 1; i <= totallength; i++)
            {
                number = i * 5;
                string timestring = string.Empty;
                var timeSpan = TimeSpan.FromMinutes(number);
                int hh = timeSpan.Hours;
                int mm = timeSpan.Minutes;
                if (hh == 0)
                    timestring = mm + " min";
                else if (mm == 0)
                    timestring = hh + " hour";
                else
                    timestring = string.Format("{0} hour : {1} min", hh, mm);
                BusinessHoursModal bLBusinessHoursModal = new BusinessHoursModal();
                bLBusinessHoursModal.Text = timestring;
                bLBusinessHoursModal.Value = number;
                timeIntervals.Add(bLBusinessHoursModal);
            }
            return timeIntervals;
        }

        public static List<long> GetLeadHours()
        {
            List<long> timeIntervals = new List<long>();
            int days = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            for (int i = 1; i <= days; i++)
            {
                timeIntervals.Add(i);
            }
            return timeIntervals;
        }
        public static string encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        public static string GenerateRandom(int count)
        {
            Random generator = new Random();
            String r = generator.Next(0, 999999).ToString("D"+ count);
            return r;
        }
        public static int CalculateAge(DateTime? dateOfBirth)
        {
            if (!dateOfBirth.HasValue)
                dateOfBirth = DateTime.Now;
            int age = 0;
            age = DateTime.Now.Year - dateOfBirth.Value.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.Value.DayOfYear)
                age = age - 1;

            return age;
        }
    }
}

public class BusinessHoursModal
{
    public int Value { get; set; }
    public string Text { get; set; }
}
