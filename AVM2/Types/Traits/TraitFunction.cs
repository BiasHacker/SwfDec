namespace SwfDec.AVM2.Types.Traits
{
    public class TraitFunction : TraitBase
    {
        public uint SlotId { get; set; }
        public MethodInfo Function { get; set; }
    }
}
