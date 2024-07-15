using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TNSDC_FinishingSchool.Bussiness.Common
{
    public class CommonMethods
    {
        private static readonly char[] PasswordCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_+-=[]{}|;:,.<>?".ToCharArray();

        public static DataTable ToDataTable<T>(List<T> items)
        {
            try
            {
                DataTable dataTable = new DataTable(typeof(T).Name);

                //Get all the properties
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    //Defining type of data column gives proper data table 
                    var type = prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType;
                    //Setting column names as Property names
                    dataTable.Columns.Add(prop.Name, type);
                }
                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {
                        //inserting property values to datatable rows
                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }
                //put a breakpoint here and check datatable
                return dataTable;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static string GenerateOtpText()
        {
            var random = new Random();
            int randomNumber = random.Next(0, 9999);
        nextcheck:
            if (randomNumber.ToString().Length != 4)
            {
                randomNumber = random.Next(0, 9999);
                goto nextcheck;
            }

            return randomNumber.ToString();
        }


        public static string GenerateRandomPassword(int length)
        {
            if (length <= 0)
            {
                throw new ArgumentException("Password length must be greater than zero.");
            }

            StringBuilder password = new StringBuilder();
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] data = new byte[4];
                for (int i = 0; i < length; i++)
                {
                    rng.GetBytes(data);
                    uint value = BitConverter.ToUInt32(data, 0);
                    password.Append(PasswordCharacters[value % PasswordCharacters.Length]);
                }
            }

            return password.ToString();
        }

    }
}
