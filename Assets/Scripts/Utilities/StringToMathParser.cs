namespace Utilities
{
    public static class StringToMathParser
    {
        public static int StringToMath(string mathString, int xValue)
        {
            int returnValue = xValue;
            mathString = mathString.Replace("x", xValue.ToString());

            if (mathString.Contains("/"))
            {
                string[] tmpMath = mathString.Split('/');
                returnValue = int.Parse(tmpMath[0]) / int.Parse(tmpMath[1]);
            }
            else if (mathString.Contains("*"))
            {
                string[] tmpMath = mathString.Split('*');
                returnValue = int.Parse(tmpMath[0]) / int.Parse(tmpMath[1]);
            }
            else if (mathString.Contains("_"))
            {
                string[] tmpMath = mathString.Split('_');
                string[] tmpMath2 = tmpMath[1].Split(':');
                returnValue = int.Parse(tmpMath[0]) / int.Parse(tmpMath2[0]);
                returnValue *= int.Parse(tmpMath2[1]);
            }

        
            return returnValue;
        }
    }
}
