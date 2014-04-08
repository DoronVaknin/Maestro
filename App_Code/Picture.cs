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
}