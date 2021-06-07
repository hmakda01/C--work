using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;

/// <summary>
/// Summary description for clsSecurity
/// </summary>
public class clsSecurity
{
    public clsSecurity()
    {
        //
        // TODO: Add constructor logic here
        //
    }

   public string SignUp(string Email, string Password, string PasswordConfirm)
    {
        //var to store any errors
        string ErrorMsg="";
        //if the passwords match
        if (Password == PasswordConfirm)
        {
            //get the hash of the plain text password
            string HashPassword = GetHashString(Password + Email);
            //add the record to the databse
            clsDataConnection DB = new clsDataConnection("select * from Users");
            DB.NewRecord["Email"] = Email;
            DB.NewRecord["UserPassword"] = HashPassword;
            DB.AddNewRecord();
            DB.SaveChanges();


        }
        //if the passwords do not match
        else
        {
            //generate an error messgae
            ErrorMsg = "The passwords do not match.";
        }
        //return th eeror messgae
        return ErrorMsg;
    }

    private string GetHashString(string SomeText)
    {
        if(SomeText != "") //if ther eis text to process
        {
            //create an instance for the hash generataor
            SHA256Managed HashGenerator = new SHA256Managed();
            //var to sore the final hash
            string HashString;
            //array to store the bytes of the original text
            byte[] TextBytes;
            //array to store the hash
            byte[] HashBytes;
            //converrt the text in the string to the array list
            TextBytes = System.Text.Encoding.UTF8.GetBytes(SomeText);
            //GENERATE the hash based on th earray list
            HashBytes = HashGenerator.ComputeHash(TextBytes);
            //generate the hash string repalcing blnak characters with -
            HashString = BitConverter.ToString(HashBytes).Replace("-", "");
            return HashString;

        }
        else
        {
            //RETRUN A BLANK STRING
            return "";
        }
    }
    
    public Boolean Login(string Email, string Password)
    {
        //convert the plain password to hash code
        Password = GetHashString(Password + Email);
        //find the record matching the users password
        clsDataConnectionOLDB UserAccount = new clsDataConnectionOLDB();
        //add the parameteres
        UserAccount.AddParameter("@Email", Email);
        UserAccount.AddParameter("@Password", Password);
        //execute the query
        UserAccount.Execute("qry_Users_FilterByEmailAndPassword");
        //if there is one record found retun true
        if (UserAccount.Count >=1 )
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Boolean ValidateInput(string SomeText, string Text)
    {
        //checks for illegal content in the input
        //boolean flag t ndicate any problems
        Boolean OK = true;
        //convert all text in input to lower case
        SomeText = SomeText.ToLower();
        //IF the text contains the same contents
        if (SomeText.Contains("<script>"))
        {
            //flag a problem
            OK = false;
        }
        if (Text.Contains("<script>"))
        {
            // flag a problem
            OK = false;
        }
        //return the state of the above
        return OK;
    }   
}