namespace SwfDec.Types
{
    public struct Rect
    {
        public int XMin, XMax, YMin, YMax;

        public Rect(int xMin, int xMax, int yMin, int yMax)
        {
            XMin = xMin;
            XMax = xMax;
            YMin = yMin;
            YMax = yMax;
        }
    }
}
