using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Picture
/// </summary>
public class Picture
{
    public Picture()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Picture(int _HatchID, string _PictureDesc, DateTime _DateTaken, string _ImageURL)
    {
        HatchID = _HatchID;
        PictureDescription = _PictureDesc;
        DateTaken = _DateTaken;
        ImageURL = _ImageURL;
    }

    int pictureID;
    public int PictureID
    {
        get { return pictureID; }
        set { pictureID = value; }
    }

    string pictureDescription;
    public string PictureDescription
    {
        get { return pictureDescription; }
        set { pictureDescription = value; }
    }

    DateTime dateTaken;
    public DateTime DateTaken
    {
        get { return dateTaken; }
        set { dateTaken = value; }
    }

    string imageURL;
    public string ImageURL
    {
        get { return imageURL; }
        set { imageURL = value; }
    }

    int hID;
    public int HatchID
    {
        get { return hID; }
        set { hID = value; }
    }

    public int UploadPicture()
    {
        DBservices dbs = new DBservices();
        int RowAffected = dbs.UploadPicture(this);
        return RowAffected;
    }
}