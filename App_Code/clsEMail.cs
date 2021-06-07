using System;

public class clsEMail
{
    public clsEMail()
    {
    }

    public bool SendEMail(string SenderEMail, string RecipientEMail, string SubjectLine, string Message)
    {
        clsDataConnection DB = new clsDataConnection("select * from tblEmail");        
        DB.NewRecord["SenderEMail"] = SenderEMail;
        DB.NewRecord["RecipientEMail"] = RecipientEMail;
        DB.NewRecord["SubjectLine"] = SubjectLine;
        DB.NewRecord["Message"] = Message;
        DB.AddNewRecord();
        DB.SaveChanges();
        return true;
    }
}