using System.Collections.Generic;
using System.Xml;

public class ZombieSpawnRate
{
    public int Min { get; set; }
    public int Max { get; set; }
    public int Value { get; set; }
}

public class ZombieType
{
    public string Type { get; set; }
    public List<ZombieSpawnRate> SpawnRates { get; set; }
}

public class ZombieSpawnData
{
    public List<ZombieType> Types { get; set; }

    public void Load(string xmlPath)
    {
        Types = new List<ZombieType>();

        using (XmlReader reader = XmlReader.Create(xmlPath))
        {
            while (reader.Read())
            {
                if (reader.IsStartElement("zombie"))
                {
                    ZombieType zombieType = new ZombieType();
                    zombieType.Type = reader.GetAttribute("type");
                    zombieType.SpawnRates = new List<ZombieSpawnRate>();

                    reader.ReadToDescendant("spawnRate");

                    while (reader.ReadToFollowing("range"))
                    {
                        ZombieSpawnRate spawnRate = new ZombieSpawnRate();
                        spawnRate.Min = int.Parse(reader.GetAttribute("min"));
                        spawnRate.Max = int.Parse(reader.GetAttribute("max"));
                        spawnRate.Value = int.Parse(reader.ReadInnerXml());
                        zombieType.SpawnRates.Add(spawnRate);
                    }

                    Types.Add(zombieType);
                }
            }
        }
    }
}

/*
using System;
using System.Xml;
using UnityEngine;

public class zombieSpawnData : MonoBehaviour {
    public string xmlFilePath;
    XmlDocument xmlDoc;

    void Start()
    {
        xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlFilePath);

        // XML 파일 내의 모든 roundCondition 요소 가져오기
        XmlNodeList roundConditions = xmlDoc.GetElementsByTagName("roundCondition");

        // 각 roundCondition 요소의 자식 요소인 zombieKill과 zombieSpawnTime 가져와서 출력하기
        foreach (XmlNode roundCondition in roundConditions)
        {
            int zombieKill = int.Parse(roundCondition.SelectSingleNode("zombieKill").InnerText);
            XmlNodeList zombieSpawnTimes = roundCondition.SelectSingleNode("zombieSpawnTime").ChildNodes;

            float[] spawnTimes = new float[zombieSpawnTimes.Count];
            for (int i = 0; i < zombieSpawnTimes.Count; i++)
            {
                spawnTimes[i] = float.Parse(zombieSpawnTimes[i].InnerText);
            }
            Debug.Log("zombieKill : " + zombieKill + "spawn times : " + string.Join(", ", spawnTimes));
        }
    }    
}
*/

/*
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

// 각 column에 해당하는 데이터 작성
public class DialogueData
{
    [XmlAttribute]
    public int zombieKillMin;
    [XmlAttribute]
    public int zombieKillMax;
    [XmlAttribute]
    public float zombieSpawnTimes0;
    [XmlAttribute]
    public float zombieSpawnTimes1;
    [XmlAttribute]
    public float zombieSpawnTimes2;
    [XmlAttribute]
    public float zombieSpawnTimes3;
}

// 데이터에 맞는 key값 설정한 Dictionary 작성
[Serializable, XmlRoot("BasicDialogue")]
public class DialogueDataLoader : ILoader<string, DialogueData>
{
    [XmlElement("DialogueData")]
    public List<DialogueData> dialogueData = new List<DialogueData>();
    
    public Dictionary<string, DialogueData> MakeDict()
    {
        Dictionary<string, DialogueData> dict = new Dictionary<string, DialogueData>();

        foreach (DialogueData dialogue in dialogueData)
        {
            dict.Add(dialogue.zombieSpawn, dialogue);
        }

        return dict;
    }
    
}
*/