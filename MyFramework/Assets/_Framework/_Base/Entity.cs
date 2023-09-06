using System;
using System.Collections.Generic;




/// <summary>
/// 公共接口试试实时数据
/// </summary>
[Serializable]
public class CommonData
{
    public int code { get; set; }
    public Data data { get; set; }
    public string msg { get; set; }

    [Serializable]
    public class NodelistItem
    {
        public string nodeName { get; set; }
        public string nodeTime { get; set; }
        public int duration { get; set; }
    }
    [Serializable]
    public class Data
    {
        public double tiltAngle { get; set; }
        public string makingNo { get; set; }
        public string currentNodeTime { get; set; }
        public string currentNodeName { get; set; }
        public string startTime { get; set; }
        public string steelCode { get; set; }
        public List<NodelistItem> nodeData { get; set; }
    }
}

/// <summary>
/// 折线图曲线历史数据接口
/// </summary>
[Serializable]
public class ConverterHisChartData
{
    public int code { get; set; }
    public Data data { get; set; }


    [Serializable]
    public class Data
    {
        public List<int> oxygenHeightHistoricalList { get; set; }
        public List<int> oxygenFlowHistoricalList { get; set; }
        public List<double> oxygenPressHistoricalList { get; set; }
        public List<int> coHistoricalList { get; set; }
        public List<int> oxygenHeightRecommendList { get; set; }
        public List<int> oxygenFlowRecommendList { get; set; }

    }
}




/// <summary>
/// 折线图曲线实时数据接口
/// </summary>
[Serializable]
public class ConverterRealData
{

    public int code { get; set; }
    public Data data { get; set; }

    [Serializable]
    public class Data
    {
        public double oxygenLanceHeight { get; set; }
        public int limitTime { get; set; }
        public double oxygenConsumption { get; set; }
        public double liquidLevelHeight { get; set; }
        public double oxygenFlowRate { get; set; }
        public double oxygenPress { get; set; }
        public double co { get; set; }
    }
}



/// <summary>
/// 废钢节点数据
/// </summary>
[Serializable]
public class SteelScrapData
{

    public int code { get; set; }
    public Data data { get; set; }

    [Serializable]
    public class Data
    {
        public object scrapWeightReal { get; set; }
        public List<Category> scrapWeightExpectCategory { get; set; }
    }
    [Serializable]
    public class Category
    {
        public string name { get; set; }
        public object value { get; set; }

    }
}



/// <summary>
/// 铁水节点数据
/// </summary>
[Serializable]
public class MoltenIronData
{

    public int code { get; set; }
    public Data data { get; set; }


    [Serializable]
    public class Data
    {
        public object ironWeightReal { get; set; }
        public object ironWeightExpect { get; set; }
        public List<info> ironInfo { get; set; }
    }


    [Serializable]
    public class info
    {
        public string name { get; set; }
        public object value { get; set; }
    }
}



/// <summary>
/// 取样数据
/// </summary>
[Serializable]
public class SamplingData
{
    public int code { get; set; }
    public Data data { get; set; }

    [Serializable]
    public class Data
    {
        public List<Info> steelGradeElement { get; set; }

    }
    [Serializable]
    public class Info
    {
        public string name { get; set; }
        public double armValue { get; set; }
        public double test { get; set; }
    }
}


/// <summary>
/// 出钢数据
/// </summary>
[Serializable]
public class TappingData
{

    public int code { get; set; }
    public Data data { get; set; }
    public string msg { get; set; }
    [Serializable]
    public class AlloyDataListItem
    {
        public string name { get; set; }
        public object expectValue { get; set; }
        public object realValue { get; set; }
    }
    [Serializable]
    public class SteelGradeElementItem
    {
        public string name { get; set; }
        public object expectValue { get; set; }
        public object realValue { get; set; }
    }
    [Serializable]
    public class Data
    {
        public List<AlloyDataListItem> alloyDataList { get; set; }
        public string steelOutputExpect { get; set; }
        public string steelOutputReal { get; set; }
        public List<SteelGradeElementItem> steelGradeElement { get; set; }
    }


}



/// <summary>
/// 溅渣实时数据
/// </summary>
[Serializable]
public class SlagSplashingRealData
{

    public int code { get; set; }

    public Data data { get; set; }

    [Serializable]
    public class Data
    {
        public int limitTime { get; set; }
        public double nitroConsumption { get; set; }
        public double nitrogenFlow { get; set; }
        public double nitrogenPressure { get; set; }
        public double oxygenHeight { get; set; }
    }
}


/// <summary>
/// 溅渣历史数据
/// </summary>
[Serializable]
public class SlagSplashingHisData
{
    public int code { get; set; }
    public Data data { get; set; }

    [Serializable]
    public class Data
    {
        public List<double> nitroFlowRate { get; set; }
        public List<double> oxygenLanceHeight { get; set; }
        public List<double> nitrogenPressure { get; set; }
    }
}




/// <summary>
/// 辅料
/// </summary>
[Serializable]
public class IngredientsData
{

    public int code { get; set; }
    public Data data { get; set; }

    [Serializable]
    public class Data
    {
        public int isBatches { get; set; }
        public List<NoBatch> bulkNoBatchDetail { get; set; }

        /// <summary>
        /// 有批次列表    长度代表种类
        /// </summary>
        public List<Batch> bulkBatchDetail { get; set; }
    }

    [Serializable]
    public class NoBatch
    {
        public string name { get; set; }
        public double expectValue { get; set; }
        public double realValue { get; set; }
    }

    [Serializable]
    public class Batch
    {
        public string name { get; set; }
        public List<double> expectValue { get; set; }
        public List<double> realValue { get; set; }
    }
}

[Serializable]
public class AppData
{
    public double converterHeight { get; set; }
    public int liquiedHeight { get; set; }
    public string BaseUrl { get; set; }
    public string CommonDataUrl { get; set; }
    public string SteelScrapDataUrl { get; set; }
    public string MoltenIronDataUrl { get; set; }
    public string ConverterHisDataUrl { get; set; }
    public string ConverterRealDataUrl { get; set; }
    public string IngredientsDataUrl { get; set; }
    public string SamplingDataUrl { get; set; }
    public string TappingDataUrl { get; set; }
    public string SlagSplashNodeHistoricalDataUrl { get; set; }
    public string SlagSplashNodeRealDataUrl { get; set; }
    public List<Token> token { get; set; }
    public NodeName nodeName { get; set; }

    [Serializable]
    public class Token
    {
        public string key { get; set; }
        public string value { get; set; }
    }
    [Serializable]
    public class NodeName
    {
        public string steelScrap;
        public string moltenIron;
        public string converting;
        public string sampling { get; set; }
        public string tapping { get; set; }
        public string slagSplashing { get; set; }
        public string slagPouring { get; set; }
    }
}



/// <summary>
/// Scoket信息存储
/// </summary>
public struct SocketInfo
{
    public E_SocketType type;
    public string host;
    public int port;
}