namespace DTGE.GameBoard.SerializationData
{
    public class EdgeDto
    {
        public string SourceObjectId { get; set; }
        public string TargetObjectId { get; set; }
        public int Distance { get; set; }
    }
}