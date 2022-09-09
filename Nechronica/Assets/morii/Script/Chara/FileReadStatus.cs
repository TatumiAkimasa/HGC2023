using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class FileReadStatus : MonoBehaviour
{
    List<CharaManeuver> partsList = new List<CharaManeuver>();

    public void FileReadParts(string name)
    {
        // 読み込みたいCSVファイルのパスを指定して開く
        StreamReader sr = new StreamReader(name);
        {
            // 末尾まで繰り返す
            while (!sr.EndOfStream)
            {
                // CSVファイルの一行を読み込む
                string line = sr.ReadLine();
                // 読み込んだ一行をカンマ毎に分けて配列に格納する
                string[] values = line.Split(',');

                // 配列からリストに格納する
                List<string> lists = new List<string>();
                lists.AddRange(values);

                
            }
        }
    }
}
