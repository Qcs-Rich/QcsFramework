using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System;

/// <summary>
/// 信息提取类  json
/// </summary>
public class Information : SingletonBase<Information>
{

    private static JsonExtractor jsonEntityExtractor;
    private static JsonExtractor JsonEntityExtractor
    {
        get
        {
            if (jsonEntityExtractor == null)
            {
                jsonEntityExtractor = new JsonExtractor(new EntityExtraction());
            }
            return jsonEntityExtractor;
        }
    }

    private static JsonExtractor jsonFieldExtractor;
    private static JsonExtractor JsonFieldExtractor
    {
        get
        {
            if (jsonFieldExtractor == null)
            {
                jsonFieldExtractor = new JsonExtractor(new FieldExtraction());
            }
            return jsonFieldExtractor;
        }
    }


    public T GetEntityFromJson<T>(string json)
    {
        return JsonEntityExtractor.ExtractorData<T>(json);
    }

    public T GetFieldFromJson<T>(string json, string key)
    {
        return JsonFieldExtractor.ExtractorData<T>(json, key);
    }

    internal interface IJsonExtraction
    {
        T ExtractData<T>(string json, string filedName = null);
    }

    public class EntityExtraction : IJsonExtraction
    {
        public T ExtractData<T>(string json, string fieldName = null)
        {
            return JsonMapper.ToObject<T>(json);
        }
    }

    internal class FieldExtraction : IJsonExtraction
    {
        public T ExtractData<T>(string json, string fieldName = null)
        {
            JsonData jsonData = JsonMapper.ToObject<JsonData>(json);

            string[] nestedFields = fieldName.Split('|');
            for (int i = 0; i < nestedFields.Length - 1; i++)
            {
                jsonData = jsonData[nestedFields[i]];
            }
            return (T)Convert.ChangeType(jsonData[nestedFields[nestedFields.Length - 1]].ToString(), typeof(T));
        }
    }

    internal class JsonExtractor
    {
        IJsonExtraction extractor;
        /// <summary>
        /// 实体类提取
        /// </summary>
        /// <param name="_extractor"></param>
        internal JsonExtractor(IJsonExtraction _extractor)
        {
            extractor = _extractor;
        }

        /// <summary>
        /// 提取字段
        /// </summary>
        /// <param name="_extractor"></param>
        internal void SetExtractor(IJsonExtraction _extractor)
        {
            extractor = _extractor;
        }


        internal T ExtractorData<T>(string json, string fieldName = null)
        {
            return extractor.ExtractData<T>(json, fieldName);
        }
    }


}