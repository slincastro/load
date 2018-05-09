using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGLoader.Reports
{
    public class ChartLines
    {

        public string CreateMultilineChart(string data, string processName)
        {
            var lineBuilder = new StringBuilder();

            lineBuilder.Append("{");
            lineBuilder.Append("type: \"line\",");
            lineBuilder.Append("axisYType: \"secondary\",");
            lineBuilder.Append(string.Format("name: \"{0}\",", processName));
            lineBuilder.Append("showInLegend: true,");
            lineBuilder.Append("markerSize: 0,");
            lineBuilder.Append("yValueFormatString: \"$#,###k\",");
            lineBuilder.Append("dataPoints: [");
            lineBuilder.Append(data);
            lineBuilder.Append("]}");

            return lineBuilder.ToString();
        }
    }
}
