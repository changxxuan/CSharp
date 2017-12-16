//存储YML每一个节点及对应的值
//每个节点的层次不同
using YamlDotNet.RepresentationModel;

[DllImport("kernel32.dll")]
public static extern IntPtr _lopen(string lpPathNme, int iReadWriter);
[DllImport("kernel32.dll")]
public static extern bool CloseHandle(IntPtr hObject);

public const int OF_READWRITE = 2;
public const int OF_SHARE_DENY_NONE = 0x40;
public static readonly IntPtr HFILE_ERROR = new IntPtr(-1);

void SaveCalibrationResultCSV()
{
	try
	{
		string fileName = "\\results.yml";
		string destFile = @"D:\Calibration_result.csv";
		string strKey = "Date"+","+"Time"+","+"SN";
		string strDate = DateTime.Now.ToString("u").Replace("-","").Replace(" ","").Replace("Z","");
		string strValue = strDate + context.SN;
		
		#region 解析yml方式
		using(StringReader reader = new StringReader(File.ReadAllText(fileName)))
		{
			YamlStream yaml = new YamlStream();
			yaml.load(reader);
			var mapping = (YamlMappingNode)yaml.Documents[0].RootNode;
			AnalysizeYML(mapping, ref strKey, ref strValue);
		}		
		#endregion
		if(!File.Exists(destFile))
			File.WriteAllText(destFile, strKey+"\r\n"+strValue);
		else
		{
			IntPtr vHandle = _lopen(destFile,OF_READWRITE | OF_SHARE_DENY_NONE);
			if(vHandle == HFILE_ERROR)
				MessageBox.Show("Calibration_result.csv 已打开， 将其关闭后再点击确定");
			CloseHandle(vHandle);
			File.AppendAllText(destFile, "\r\n" + strValue);
		}
	}
	catch(Exception ex)
	{
		context.logger.Error(ex.message);
	}
}

static void AnalysizeYML(YamlMappingNode node, ref string strKey, ref string strValue)
{
	foreach(var entry in node.Children)
	{
		if(entry.Value is YamlScalarNode)
		{
			strKey += ","+ entry.Key;
			strValue += ","+ entry.Value;
			continue;
		}
		strKey += ","+ entry.Key;
		strValue += ",";
		AnalysizeYML((YamlMappingNode)node.Children[new YamlScalarNode(((YamlScalarNode)entry.Key).Value)], ref strKey, ref strValue);
	}
}

//匹配正则表达式
void MatchRegex(string result)
{
	string LedData = context.SN;
	using (StringReader sr = new StringReader(result))
	{
		string line = sr.ReadLine().Trim();
		while(!string.isNullOrEmpty(line))
		{
			if(line.StartsWith("LED"))
			{
				line = line.Split('=')[1].Trim().Split('>')[0];
			LedData += "," + Regex.Match(line, @"(\d+\.?\d*\s*%?)(\s*\[|\]+)").Groups[1];
			//LED30 [high angle amplitue = 129] : ....
			//LED00 [LED relative intensity = 1.05 [> 0.60] : ...
			//LED15 [outlier =     0 %] : ...
			//LED21 [LED angular coverage = 100% [>  70%] :...
			}
			line = sr.Readline();
		}
	}
}