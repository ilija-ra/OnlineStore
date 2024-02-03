using System.Runtime.Serialization;

namespace OnlineStore.Communication.PubSub
{
    [DataContract]
    public class PublishedMessageOne
    {
        [DataMember]
        public string? Content { get; set; }
    }

    [DataContract]
    public class PublishedMessageTwo
    {
        [DataMember]
        public string? Content { get; set; }
    }
}
