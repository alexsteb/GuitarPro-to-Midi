using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;


public class open_gp_file  {
    public string Title = "";
    public string FileName = "";
    public string Directory = "";
    public string Extension = "";


    GPFile gpfile;

    public open_gp_file(string filename,string directory){
      Title = System.IO.Path.GetFileNameWithoutExtension(filename);
      FileName = filename;
      Directory = directory;
      Extension = System.IO.Path.GetExtension(filename);
    }

    public void OutputRoutine()
    {
        var loader = File.ReadAllBytes(FileName);;
        //Detect Version by Filename
        int version = 7;
        string fileEnding = Extension;
        if (fileEnding.Equals(".gp3")) version = 3;
        if (fileEnding.Equals(".gp4")) version = 4;
        if (fileEnding.Equals(".gp5")) version = 5;
        if (fileEnding.Equals(".gpx")) version = 6;
        if (fileEnding.Equals(".gp")) version = 7;


        switch (version)
        {
            case 3:
                gpfile = new GP3File(loader);
                gpfile.readSong();
                break;
            case 4:
                gpfile = new GP4File(loader);
                gpfile.readSong();
                break;
            case 5:
                gpfile = new GP5File(loader);
                gpfile.readSong();

                break;
            case 6:
                gpfile = new GP6File(loader);
                gpfile.readSong();
                gpfile = gpfile.self;
                break;
            default:
                Debug.Log("Unknown File Format");
                break;
        }
        var song = new Native.NativeFormat(gpfile);
        var midi = song.toMidi();
        List<byte> data = midi.createBytes();
        var dataArray = data.ToArray();
        using (var fs = new FileStream("./" +Title+ ".mid", FileMode.OpenOrCreate, FileAccess.ReadWrite))
        {
            fs.Write(dataArray, 0, dataArray.Length);

        }

    }

}
