using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class FileReadStatus : MonoBehaviour
{
    List<CharaManeuver> partsList = new List<CharaManeuver>();

    public void FileReadParts(string name)
    {
        // �ǂݍ��݂���CSV�t�@�C���̃p�X���w�肵�ĊJ��
        StreamReader sr = new StreamReader(name);
        {
            // �����܂ŌJ��Ԃ�
            while (!sr.EndOfStream)
            {
                // CSV�t�@�C���̈�s��ǂݍ���
                string line = sr.ReadLine();
                // �ǂݍ��񂾈�s���J���}���ɕ����Ĕz��Ɋi�[����
                string[] values = line.Split(',');

                // �z�񂩂烊�X�g�Ɋi�[����
                List<string> lists = new List<string>();
                lists.AddRange(values);

                
            }
        }
    }
}
