using WorkMate.Views;

namespace WorkMate.Models
{
    public class Unit
    {
        //public ushort UnitNo { get; set; }
        public ushort ProcessCode { get; set; }
        public string Process { get {  return MainView.DicKeyValues[ProcessCode].iValue; } }
        public ushort SideType { get; set; }
        public string Side { get { return MainView.DicKeyValues[SideType].iValue; } }
        public string UnitName { get; set; }
    }
}