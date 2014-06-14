using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Pin
/// </summary>
public class Pin
{
    public Pin()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Pin(int _PinID, double _CoordinateX, double _CoordinateY, string _Comment, string _AudioURL, string _VideoURL, int _PictureID)
    {
        PinID = _PinID;
        CoordinateX = _CoordinateX;
        CoordinateY = _CoordinateY;
        Comment = _Comment;
        AudioURL = _AudioURL;
        VideoURL = _VideoURL;
        PictureID = _PictureID;
    }

    int pinID;
    public int PinID
    {
        get { return pinID; }
        set { pinID = value; }
    }

    double coordinateX;
    public double CoordinateX
    {
        get { return coordinateX; }
        set { coordinateX = value; }
    }

    double coordinateY;
    public double CoordinateY
    {
        get { return coordinateY; }
        set { coordinateY = value; }
    }

    string comment;
    public string Comment
    {
        get { return comment; }
        set { comment = value; }
    }

    string audioURL;
    public string AudioURL
    {
        get { return audioURL; }
        set { audioURL = value; }
    }

    string videoURL;
    public string VideoURL
    {
        get { return videoURL; }
        set { videoURL = value; }
    }

    int pictureID;
    public int PictureID
    {
        get { return pictureID; }
        set { pictureID = value; }
    }

    public int InsertNewPin()
    {
        DBservices dbs = new DBservices();
        int RowAffected = dbs.InsertNewPin(this);
        return RowAffected;
    }

    public int DeletePin(int pinID)
    {
        DBservices dbs = new DBservices();
        int RowAffected = dbs.DeletePin(pinID);
        return RowAffected;
    }
}